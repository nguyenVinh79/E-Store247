using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopBanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopBanHang.Controllers
{
    public class ProductController : BaseController
    {
        private DataShopContext _db;

        public ProductController(DataShopContext dbcontext)
        {
            _db = dbcontext;
        }

        public IActionResult Index()
        {
            var dataProducts = _db.Products.Where(x => x.IsDeleted != true).ToList();
            return View(dataProducts);
        }

        public IActionResult Detail(int id)
        {
            var dataProduct = getDetailProduct(id);
            if (dataProduct != null)
            {
                var lstProductSize = _db.ProductColorSizes.Where(x => x.ProductID == dataProduct.ID).ToList();
                var lstImage = _db.Images.Where(x => x.ReferenceId == dataProduct.ID && x.IsShow == true).ToList();
                var lstRelative = _db.Relatives.Where(x => x.ID == dataProduct.ID).ToList();

                var model = new ProductViewModel
                {
                    ProductInfo = dataProduct,
                    ImageList = lstImage,
                    RelateList = lstRelative,
                    PropertyList = lstProductSize
                };

                return View(model);
            }
            //gán vào 1 model --> ProductViewModel

            return View();
        }

        public IActionResult Search()
        {
            return View();
        }

        #region Cart

        //Danh sách giỏ hàng
        public IActionResult ListCart()
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<CartModel> dataCart = JsonConvert.DeserializeObject<List<CartModel>>(cart);
                if (dataCart.Count > 0)
                {
                    ViewBag.carts = dataCart;
                    return View();
                }
            }
            return View();
            //return RedirectToAction(nameof(Index)); // trả về index
        }

        //Khi thêm vào check sp có tồn tại chưa, có thì tăng số lượng, thêm mới
        public IActionResult addCart(int id, int quantity) //id product
        {
            var cart = HttpContext.Session.GetString("cart"); //get key cart
            if (cart == null)
            {
                //giỏ hàng chưa có sp
                var DetailProduct = _db.ProductColorSizes.Find(Convert.ToInt64(id));
                var MainProduct = _db.Products.SingleOrDefault(x => x.ID == DetailProduct.ProductID);
                List<CartModel> listCart = new List<CartModel>()
                {
                    new CartModel
                    {
                        Product = MainProduct,
                        DetailProduct = DetailProduct,
                        Quantity = quantity
                    }
                };
                //chuyển object sang json
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(listCart));
            }
            else
            {
                List<CartModel> dataCart = JsonConvert.DeserializeObject<List<CartModel>>(cart);
                bool checkBit = true;
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].DetailProduct.Id == id)
                    {
                        dataCart[i].Quantity += quantity;
                        checkBit = false;
                        //phát hiện sp vừa add tăng 1 cho product đó rồi break out of loop
                        break;
                    }
                }
                //product is'nt exist in cart
                if (checkBit == true)
                {
                    var DetailProduct = _db.ProductColorSizes.Find(Convert.ToInt64(id));
                    var MainProduct = _db.Products.SingleOrDefault(x => x.ID == DetailProduct.ProductID);
                    dataCart.Add(new CartModel
                    {
                        Product = MainProduct,
                        DetailProduct = DetailProduct,
                        Quantity = quantity
                    });
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
            }
            return RedirectToAction(nameof(ListCart));
        }

        private Product getDetailProduct(int id)
        {
            //get product by id Find(id) method in entities
            var product = _db.Products.Find(id);
            return product;
        }

        //update sản phẩm đã đưa vào giỏ hàng (Ajax request)
        [HttpPost]
        public IActionResult updateCart(int Id, int Quantity)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<CartModel> dataCart = JsonConvert.DeserializeObject<List<CartModel>>(cart);
                if (Quantity > 0)
                {
                    for (int i = 0; i < dataCart.Count; i++)
                    {
                        if (dataCart[i].DetailProduct.Id == Id)
                        {
                            dataCart[i].Quantity = Quantity;
                        }
                    }
                    //set lại session
                    HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                }
                //dùng ajax trả về status 200
                return Ok(Quantity);
            }

            return BadRequest();
        }

        //xóa sp trong giỏ hàng đi(dùng link)
        public IActionResult deleteCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<CartModel> dataCart = JsonConvert.DeserializeObject<List<CartModel>>(cart);
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].DetailProduct.Id == id)
                    {
                        dataCart.RemoveAt(i);
                    }
                }

                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
            }

            return RedirectToAction(nameof(ListCart));
        }

        #endregion Cart

        #region Address

        //public JsonResult AjaxDistrictList(int Id)
        //{
        //    var data = _db.CT_Districts.Where(m => m.ProvinceID == Id.ToString()).ToList();

        //    return Json(data);
        //}

        //public JsonResult AjaxWardList(int Id)
        //{
        //    var data = _db.CT_Wards.Where(m => m.DistrictID == Id.ToString()).ToList();

        //    return Json(data);
        //}

        #endregion Address
    }
}