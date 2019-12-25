using AppModelv2_WebApp_OpenIDConnect_DotNet.Controllers.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppModelv2_WebApp_OpenIDConnect_DotNet.Controllers
{
    [Authorize]
    public class UploadFactoryController : Controller
    {
        // GET: UploadFactory
        public ActionResult Index()
        {
            return View("UploadStuff");
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Upload(HttpPostedFileBase uploadfile)
        {
            if (uploadfile != null)
            {
                var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;

                //You get the user’s first and last name below:
                string username = userClaims?.FindFirst("name")?.Value;
                //  string filepath = Path.Combine(Server.MapPath("./Stuff"), Path.GetFileName(uploadfile.FileName));
                string path = Path.Combine(Server.MapPath("~/Stuff"),
                                             Path.GetFileName(uploadfile.FileName));
                uploadfile.SaveAs(path);
                await FileUploader.UploadFileToBlobAsync(path, username);
                return View("UploadStuff");
            }
            return View("UploadStuff");
        }
    }
}