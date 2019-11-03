using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsPublish.Model.Request;
using NewsPublish.Model.Response;
using NewsPublish.Service;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace NewsPublish.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : Controller
    {
        private BannerService _bannerService;
        [Obsolete]
        private IHostingEnvironment _host;

        [Obsolete]
        public BannerController(BannerService bannerService, IHostingEnvironment host)
        {
            _bannerService = bannerService;
            _host = host;
        }
        // GET: Banner
        public ActionResult Index()
        {
            var banner = _bannerService.GetBannerList();
            return View(banner);
        }
        public ActionResult BannerAdd()
        {
            return View();
        }
        [HttpPost]
        [Obsolete]
        public async Task<JsonResult> AddBanner(AddBanner banner, IFormCollection collection)
        {

            var files = collection.Files;
            if (files.Count > 0)
            {
                var webRootPath = _host.WebRootPath;
                string relativeDirPath = "\\BAnnerPic";
                string absolutePath = webRootPath + relativeDirPath;
                string[] fileTypes = new string[] { ".gif", ".jpg", ".jpeg", ".png" };
                string extension = Path.GetExtension(files[0].FileName);
                if (fileTypes.Contains(extension.ToLower()))
                {
                    bool HasDirectory = Directory.Exists(absolutePath);
                    if (!HasDirectory)
                        Directory.CreateDirectory(absolutePath);
                    string fileName = DateTime.Now.ToString("yyyMMddHHmmss") + extension;
                    var filePath = absolutePath + "\\" + fileName;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await files[0].CopyToAsync(stream);
                    }
                    banner.Image = "/BannerPic/" + fileName;
                    return Json(_bannerService.AddBanner(banner));
                }
                return Json(new ResponseModel
                {
                    code = 0,
                    result = "图片格式有误"
                });
            }
            return Json(new ResponseModel
            {
                code = 0,
                result = "请上传图片文件,图片数量：" + files.Count,
                data = collection
            });
        }
        [HttpPost]
        public JsonResult DelBanner(int id)
        {
            if (id < 0)
                return Json(new ResponseModel { code = 0, result = "参数有误" });
            return Json(_bannerService.DeleteBanner(id));
        }
        // GET: Banner/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Banner/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Banner/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Banner/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Banner/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Banner/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Banner/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}