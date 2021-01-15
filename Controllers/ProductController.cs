using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Newtonsoft.Json;
using ShopBanHang.Helper;
using ShopBanHang.Models;
using ShopBanHang.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanHang.Controllers
{
    public class ProductController : BaseController
    {
        private DataShopContext _db;
        private readonly IMapper _mapper;
        private readonly IMailService mailService;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IViewRenderService _viewRenderService;
        public ProductController(DataShopContext dbcontext, IMapper mapper, IMailService mailService, IWebHostEnvironment env,
            IConfiguration conf, IViewRenderService renderService)
        {
            _db = dbcontext;
            _mapper = mapper;
            this.mailService = mailService;
            _env = env;
            _configuration = conf;
            _viewRenderService = renderService;
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


        #region Cart

       
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
        [Route("/checkout")]
        public IActionResult Checkout()
        {
            //Get customer Info tu trong session
            //CustomerInfo.
            var customerinfor = new CustomerInfo();
            var lstCT_Provinces = _db.CT_Provinces.ToList();
            var lstCT_Districts = _db.CT_Districts.ToList();
            var lstWards = _db.CT_Wards.ToList();
            if (this.User.Identity.IsAuthenticated)
            {
                customerinfor = _db.CustomerInfos.Where(x => x.UserName == this.User.Identity.Name).First();

                ViewBag.Provinces = new SelectList(lstCT_Provinces, "ID", "Name", customerinfor.ProvinceID != null ? customerinfor.ProvinceID : 0);

                if (customerinfor.ProvinceID != null)
                {
                    ViewBag.Districts = new SelectList(lstCT_Districts.Where(m => m.ProvinceID == customerinfor.ProvinceID.ToString()).ToList(), "ID", "Name",
                                customerinfor.DistrictID);
                }
                else
                {
                    ViewBag.Districts = new SelectList(lstCT_Districts.ToList(), "ID", "Name", 0);
                }

                if (customerinfor.DistrictID != null)
                {
                    ViewBag.Wards = new SelectList(lstWards.Where(m => m.DistrictID == customerinfor.DistrictID.ToString()).ToList(), "ID", "Name", customerinfor.WardID);
                }
                else
                {
                    ViewBag.Wards = new SelectList(lstWards.Where(m => m.DistrictID == customerinfor.DistrictID.ToString()).ToList(), "ID", "Name", 0);
                }
            }
            else
            {
                ViewBag.Provinces = new SelectList(_db.CT_Provinces.ToList(), "ID", "Name",0);
                ViewBag.Districts = new SelectList(new List<CT_Province>());
                ViewBag.Wards = new SelectList(new List<CT_Ward>());
            }

            var model = _mapper.Map<CustomerInfoModel>(customerinfor);

            return View(model);
        }
        #region Order section
        [HttpPost]
        [Route("Order")]
        public IActionResult ToOrder(CustomerInfoModel model)
        {
            //Get cart from Session
            var cart = HttpContext.Session.GetString("cart");
            if (cart == null)
            {
                return View("ErrorOrder");
            }

            List<CartModel> dataCart = JsonConvert.DeserializeObject<List<CartModel>>(cart);
            if (ModelState.IsValid)
            {
                //Get info of customer
                var customerinfor = _db.CustomerInfos.Where(x => x.UserName == this.User.Identity.Name).FirstOrDefault();
                //CustomerInfoModel contains order infomation
                if (customerinfor != null)
                {
                    customerinfor.FullName = model.FullName;
                    customerinfor.Address = model.Address;
                    customerinfor.PhoneNumber = model.PhoneNumber;
                    customerinfor.ProvinceID = model.ProvinceID;
                    customerinfor.DistrictID = model.DistrictID;
                    customerinfor.WardID = model.WardID;
                    customerinfor.Note = model.Note;
                    //save modified informations to database
                    _db.SaveChanges();
                }
                

                //Create order
                var OrdersModel = new Order();
                if (model.UserName == null)
                { 
                    OrdersModel.UserName = "None-resident guest"; 
                }    
                else
                {
                    OrdersModel.UserName = model.UserName;
                }
                OrdersModel.FullName = model.FullName; 
                OrdersModel.Address = model.Address;
                OrdersModel.PhoneNumber = model.PhoneNumber;
                OrdersModel.ProvinceID = model.ProvinceID;
                OrdersModel.DistrictID = model.DistrictID;
                OrdersModel.WardID = model.WardID;
                OrdersModel.OrderDate = DateTime.Now;
                OrdersModel.Total = model.Total;
                OrdersModel.ShipFee = model.ShipFee;

                _db.Orders.Add(OrdersModel);

                _db.SaveChanges();

                for (int i = 0; i < dataCart.Count; i++)
                {
                    var OrdersDetailModel = new OrderDetail();

                    OrdersDetailModel.OrderId = OrdersModel.OrderId;
                    OrdersDetailModel.ProductId = dataCart[i].Product.ID;
                    //OrdersDetailModel.ProductName = dataCart[i].Product.ProductName;
                    OrdersDetailModel.Quantity = dataCart[i].Quantity;
                    OrdersDetailModel.UnitPrice = dataCart[i].DetailProduct.UnitPriceNew;
                    OrdersDetailModel.ColorSize = dataCart[i].DetailProduct.NameColor;
                    _db.OrderDetails.Add(OrdersDetailModel);

                }

                _db.SaveChanges();

                //Send Email if login
                #region Email

                if (customerinfor != null)
                {
                    if(!string.IsNullOrEmpty(customerinfor.Email))
                    {
                        var mailRequest = new MailRequest();
                        string OrderIDFormat = OrdersModel.OrderId.ToString("#00000000");
                        mailRequest.ToEmail = customerinfor.Email; //get user email
                        mailRequest.Subject = $"Order Confirmation #{OrderIDFormat} - Estore247";
                        mailRequest.Body = "";
                        
                    
                        var orderDetails = _db.OrderDetails.Where(x => x.OrderId == OrdersModel.OrderId).ToList();

                        var bodyorderdetail = CreatOderDetailHTML(orderDetails, OrdersModel.Total.Value, OrdersModel.ShipFee.Value);

                        string ward = _db.CT_Wards.Where(m => m.ID == OrdersModel.WardID)
                            .Select(m => m.Name)
                            .FirstOrDefault();
                        string district = _db.CT_Districts.Where(m => m.ID == OrdersModel.DistrictID)
                            .Select(m => m.Name)
                            .FirstOrDefault();
                        string province = _db.CT_Provinces.Where(m => m.ID == OrdersModel.ProvinceID)
                            .Select(m => m.Name)
                            .FirstOrDefault();

                        string address = OrdersModel.Address + ", " + ward + ", " + district + ", " + province;

                        // Read file template html 
                        var webRoot = _env.WebRootPath;

                        string pathToFile = Path.Combine(this._env.WebRootPath, "Template")
                             + Path.DirectorySeparatorChar.ToString()
                             + "EmailOrder.html";

                        var builder = new BodyBuilder();
                        using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                        {

                            builder.HtmlBody = SourceReader.ReadToEnd();

                        }
                        //Render order ID: 10 --> #0000010
                        mailRequest.Body = string.Format(builder.HtmlBody,
                            OrdersModel.FullName,
                           "#" + (OrdersModel.OrderId.ToString("#00000000")),
                           string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", OrdersModel.OrderDate),
                           address
                           , OrdersModel.PhoneNumber
                           , bodyorderdetail);

                        mailService.SendEmailAsync(mailRequest);
                    }
                }
                #endregion

                //Clear Cart session
                HttpContext.Session.Remove("cart");
                return RedirectToAction("ToOrder");
            }
            return View("ErrorOrder");
        }
        [HttpGet]
        [Route("Order")]
        public IActionResult ToOrder ()
        {
            return View();
        }
        #endregion


        #region Address for responding Ajax request

        public JsonResult AjaxDistrictList(int Id)
        {
            var data = _db.CT_Districts.Where(m => m.ProvinceID == Id.ToString()).ToList();

            return Json(data);
        }

        public JsonResult AjaxWardList(int Id)
        {
            var data = _db.CT_Wards.Where(m => m.DistrictID == Id.ToString()).ToList();

            return Json(data);
        }

        #endregion Address
        #region Search product

        public ActionResult Search(int? CategoryID, string colorId, string sizeId, string giaTu, string giaDen, string ProductName = "", string OrderBy = "ID")
        {

            int BlockSize = 3; //pagesize
            ViewBag.ProductName = ProductName;
            ViewBag.OrderBy = OrderBy;


            //Get data from db
            List<SearchProduct_Result> lstProduct = GetProductSearch(1, BlockSize, CategoryID, ProductName, OrderBy);

            //Tạo data cho combobox search

            DropdownCategory(null, null);

            //xử lý data
            ViewBag.MoreData = false; //Còn data chưa lấy ra hết
            if (lstProduct != null && lstProduct.Count > 0)
            {
                ViewBag.MoreData = lstProduct[0].TotalCount > BlockSize;
            }


            return View(lstProduct);
        }

        public List<Category> GetCategories()
        {
            List<Category> result = new List<Category>();
            try
            {
                result = _db.Categories.AsNoTracking().OrderBy(p => p.Order).Where(m => m.IsDeleted == false || m.IsDeleted == null).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public void DropdownCategory(string title, int? CategoryID = 0)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var category = GetCategories();

            var idCateCurrent = 0;

            if (CategoryID != null)
            {
                if (CategoryID == 0)
                {
                    //foreach (var cate in category)
                    //{
                    //    if (HelperSEO.ToSeoUrl(cate.CategoryName) == title)
                    //    {
                    //        idCateCurrent = cate.CategoryID;
                    //        break;
                    //    }
                    //}
                }
                else
                {
                    idCateCurrent = CategoryID.Value;
                }
            }

            foreach (var c in category)
            {
                if (c.ParentCategoryID == 0)
                {
                    list.Add(new SelectListItem
                    {
                        Selected = c.ID == idCateCurrent ? true : false,
                        Text = c.CategoryName,
                        Value = c.ID.ToString()
                    });
                    foreach (var cc in category)
                    {
                        if (cc.ParentCategoryID == c.ID)
                        {
                            list.Add(new SelectListItem
                            {
                                Selected = cc.ID == idCateCurrent ? true : false,
                                Text = "---" + cc.CategoryName,
                                Value = cc.ID.ToString()
                            });
                            foreach (var c3 in category)
                            {
                                if (c3.ParentCategoryID == cc.ID)
                                {
                                    list.Add(new SelectListItem
                                    {
                                        Selected = c3.ID == idCateCurrent ? true : false,

                                        Text = "------" + c3.CategoryName,
                                        Value = c3.ID.ToString()
                                    });
                                }
                            }
                        }
                    }
                }
            }

            ViewBag.CategoryID = list;
        }

        public List<SearchProduct_Result> GetProductSearch(int BlockNumber, int BlockSize, int? CategoryID = 0, string ProducName = "", string Order = "")
        {
            //var products = ProductManager.GetProduct(1, BlockSize);
            List<SearchProduct_Result> productlist = new List<SearchProduct_Result>();
            Category category = null;
            int startIndex = (BlockNumber - 1) * BlockSize;

            List<int> lstcateID = new List<int>();
            if (CategoryID != 0)
            {
                var categorylist = _db.Categories.Where(m => m.IsDeleted == null || m.IsDeleted == false).ToList();

                var cateRoot = categorylist.Where(x => x.ParentCategoryID == 0).ToList();

                foreach (var cate in categorylist)
                {
                    if (cate.ID == CategoryID)
                    {
                        category = cate;
                        break;
                    }
                }

                //neu ton tai category thi get list danh sach san pham theo category
                if (category != null)
                {
                    lstcateID = GetListChild(category.ID).Select(x => x.ID).ToList();
                }
            }

            string listCategoryStr = string.Join(",", lstcateID);



            List<SqlParameter> lstParas = new List<SqlParameter>();
            SqlParameter para1 = new SqlParameter();
            para1.ParameterName = "CategoryID";
            para1.Value = listCategoryStr;
            lstParas.Add(para1);

            SqlParameter paraBlockNumber = new SqlParameter();
            paraBlockNumber.ParameterName = "PageNum";
            paraBlockNumber.Value = BlockNumber;
            lstParas.Add(paraBlockNumber);

            SqlParameter paraBlockSize = new SqlParameter();
            paraBlockSize.ParameterName = "PageSize";
            paraBlockSize.Value = BlockSize;
            lstParas.Add(paraBlockSize);

            SqlParameter paraOrder = new SqlParameter();
            paraOrder.ParameterName = "OrderBy";
            paraOrder.Value = Order;
            lstParas.Add(paraOrder);

            SqlParameter paraProducName = new SqlParameter();
            paraProducName.ParameterName = "TenSP";
            paraProducName.Value = ProducName;
            lstParas.Add(paraProducName);

            var storeName = "SearchProduct";
            var dt = GetDataTableByStore(storeName, lstParas.ToArray());

            List<SearchProduct_Result> dataResult = new List<SearchProduct_Result>();
            dataResult = MyTool.ConvertDataTable<SearchProduct_Result>(dt);

            return dataResult;
        }

        

        public DataTable GetDataTableByStore(string storeName, SqlParameter[] parameters)
        {
            try
            {
                DataTable dataTable = new DataTable();

                using (SqlConnection connection = new SqlConnection(_db.Database.GetDbConnection().ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(storeName, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dataTable);
                    connection.Close();
                }

                return dataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public List<Category> GetListChild(long rootID)
        {
            var result = new List<Category>();
            try
            {


                var lstAll = _db.Categories.AsNoTracking().Where(m => m.IsDeleted != true).ToList();

                result.Add(lstAll.Where(x => x.ID == rootID).First());

                var lstAll1 = lstAll.Where(p => p.ParentCategoryID == rootID && p.IsDeleted != true).ToList();
                result.AddRange(lstAll1);

                if (lstAll1 != null && lstAll1.Count > 0)
                {
                    foreach (var mp in lstAll1)
                    {
                        result.Add(mp);

                        var temp = RenderMenuItem(lstAll, mp).ToList();
                        if (temp != null && temp.Count > 0)
                        {
                            result.AddRange(temp);
                        }
                    }
                }

            }
            catch (System.Exception ee)
            {
                return new List<Category>();
                throw ee;
            }

            return result;
        }

        public static List<Category> RenderMenuItem(List<Category> menuList, Category mi)
        {
            var result = new List<Category>();

            var list = menuList.Where(p => p.ParentCategoryID == mi.ID).ToList();

            if (list != null && list.Count > 0)
            {
                foreach (var cp in menuList.Where(p => p.ParentCategoryID == mi.ID))
                {
                    result.Add(cp);
                    result.AddRange(RenderMenuItem(menuList, cp));
                }
            }

            return result;
        }


        //InfinateScrollSearch

        public async Task<IActionResult> InfinateScrollSearch(int BlockNumber, int? CategoryID, string ProductName = "", string OrderBy = "ID")
        {
            System.Threading.Thread.Sleep(900);

            var jsonModel = new JsonModel();
            int BlockSize = int.Parse(this._configuration["BlockSize_Product"]);
            List<SearchProduct_Result> lstProduct = GetProductSearch(BlockNumber, BlockSize, CategoryID, ProductName, OrderBy);

            jsonModel.MoreData = false;

            if (lstProduct != null && lstProduct.Count > 0)
            {
                jsonModel.MoreData = lstProduct[0].TotalCount > BlockSize * BlockNumber;
            }

            jsonModel.HTMLString = await _viewRenderService.RenderToStringAsync("Product/ProductList", lstProduct);
            return Json(jsonModel);


        }

        public class JsonModel
        {
            public string HTMLString { get; set; }

            public bool MoreData { get; set; }
        }
        #endregion Search product
        public string CreatOderDetailHTML(List<OrderDetail> lstorderdetail, double? total, double? ship)
        {
            double sumorder = total.Value + ship.Value;


            string body = "<table width='650' cellspacing='0' cellpadding='0' border='0' style='border: 1px solid #eaeaea'>";
            body += "<thead>";
            body += "<tr>";
            body += "<th bgcolor='#261b4f' align='left' style='font-size: 13px; color:#fff; padding: 3px 9px'>Product</th>";
            body += " <th bgcolor='#261b4f' align='left' style='font-size:13px;padding:3px 9px'></th>";
            body += "<th bgcolor='#261b4f' align='center' style='font-size: 13px; color:#fff; padding: 3px 9px'>Quantity</th>";
            body += " <th bgcolor='#261b4f' align='right' style='font-size: 13px; color:#fff; padding: 3px 9px'>Subtotal</th>";
            body += "</tr>";
            body += "</thead>";
            foreach (var item in lstorderdetail)
            {

                var product = _db.Products.Where(x => x.ID == item.ProductId).FirstOrDefault();


                body += "<tr>";
                body += " <td valign='top' align='left' style='font-size: 11px; padding: 3px 9px; border-bottom: 1px dotted #cccccc'>";
                body += "<strong style='font-size: 11px'>" + product.ProductName + "</strong>";
                body += "<dl style='margin: 0; padding: 0'>";
                body += "<dt><strong><em>Color:</em></strong></dt>";

                body += "<dd style='margin: 0; padding: 0 0 0 15px'>" + item.ColorSize + "</dd>";

                body += "</dl>";
                body += "</td>";
                body += "<td valign='top' align='left' style='font-size:11px;padding:3px 9px;border-bottom:1px dotted #cccccc'></td>";
                body += "<td valign='top' align='center' style='font-size: 11px; padding: 3px 9px; border-bottom: 1px dotted #cccccc'>" + item.Quantity + "</td>";
                body += "<td valign='top' align='right' style='font-size: 11px; padding: 3px 9px; border-bottom: 1px dotted #cccccc'>";
                body += "<span>" + string.Format("{0:0,0 vnd}", item.UnitPrice) + "</span>";
                body += "</td>";
                body += "</tr>";
            }
            body += "<tr>";
            body += "<td align='right' style='padding: 3px 9px' colspan='3'>Total Payment</td>";
            body += "<td align='right' style='padding: 3px 9px'>";
            body += "<span>" + string.Format("{0:0,0 vnd}", total) + "</span></td>";
            body += "</tr>";
            body += "<tr>";
            //body += "<td align='right' style='padding: 3px 9px' colspan='3'>Delivery fee</td>";
            //body += "<td align='right' style='padding: 3px 9px'>";
            //body += "<span>" + string.Format("{0:0,0 vnd}", ship) + "</span>                    </td>";
            //body += "</tr>";
            //body += "<tr>";
            //body += "<td align='right' style='padding: 3px 9px' colspan='3'>";
            //body += "<strong>Total Payment</strong>";
            //body += "</td>";
            //body += "<td align='right' style='padding: 3px 9px'>";
            //body += "<strong><span>" + string.Format("{0:0,0 vnd}", sumorder) + "</span></strong>";
            //body += "</td>";
            //body += "</tr>";
            body += " </table>";

            return body;
        }
    }
}