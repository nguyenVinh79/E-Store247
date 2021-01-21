using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ECommerce.Project.Areas.Admin.Models;
using ECommerce.Project.Models;
using System;
using System.Linq;

namespace ECommerce.Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SettingController : Controller
    {
        private readonly IMapper _mapper;
        private DataShopContext db;
        private readonly IWebHostEnvironment _env;
        private readonly int pageSize = 10;
        // requires using Microsoft.Extensions.Configuration;

        private readonly IConfiguration Configuration;

        public SettingController(IMapper mapper, IWebHostEnvironment env, IConfiguration configuration,
            DataShopContext dbcontext)
        {
            this._mapper = mapper;
            _env = env;
            db = dbcontext;
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            var data = db.Settings.ToList();
            return View(data);
        }

        public IActionResult Create(int id = 0)
        {
            var model = new SettingModel();
            if (id > 0)
            {
                var entity = db.Settings.Where(x => x.ID == id).FirstOrDefault();
                model = _mapper.Map<SettingModel>(entity);
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(SettingModel model, IFormFile avata) //setting upload file
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.ID == 0)
                    {
                        #region For Create

                        var entity = _mapper.Map<Setting>(model);
                        db.Settings.Add(entity);
                        db.SaveChanges();

                        #endregion For Create
                        TempData["StatusMessage"] = "Successfully created";
                    }
                    else
                    {
                        #region for edit

                        var entity = db.Settings.Find(model.ID);
                        entity = _mapper.Map(model, entity);

                        db.Update(entity);
                        db.SaveChanges();

                        #endregion for edit
                        TempData["StatusMessage"] = "Successfully updated";
                    }

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["StatusMessage"] = ex.Message;
                return View(model);
            }

            return View(model);
        }

 
        
    }
}