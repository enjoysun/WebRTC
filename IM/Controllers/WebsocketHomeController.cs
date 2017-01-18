using IM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IM.Controllers
{
    public class WebsocketHomeController : Controller
    {
        // GET: Websocketdemo
        private websocketcommon con = new websocketcommon();
        public ActionResult Index()
        {
            ViewBag.msg=con.Connect();
            return View();
        }
    }
}