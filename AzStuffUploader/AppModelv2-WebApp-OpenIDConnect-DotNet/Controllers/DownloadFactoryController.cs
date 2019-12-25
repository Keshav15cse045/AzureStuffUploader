using AppModelv2_WebApp_OpenIDConnect_DotNet.Controllers.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppModelv2_WebApp_OpenIDConnect_DotNet.Controllers
{
    [Authorize]
    public class DownloadFactoryController : Controller
    {
        // GET: DownloadFactory  DownloadFiles

        public async System.Threading.Tasks.Task<ActionResult> DownloadFiles()
        {
            var downloadlists = await FileDownloader.DownloadList();
            return View(downloadlists);
        }
    }
}