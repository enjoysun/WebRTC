using RedisHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IM.Controllers
{
    public class RedisdemoController : Controller
    {
        // GET: Redisdemo
        RedisHelper redishelp = new RedisHelper(1);
        public ActionResult Index()
        {
            ViewBag.info=redishelp.StringSet("name", "jack");
            return View();
        }
    }
}