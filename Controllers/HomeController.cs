using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UhyIntranet.Common;

namespace SimpleSearchMVCApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private FeaturesSearch _featuresSearch = new FeaturesSearch("");

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string q = "", string f = "", string[] channels = null)
        {   
            // If blank search, assume they want to search everything
            if (string.IsNullOrWhiteSpace(q))
                q = "*";



            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                
                Data = _featuresSearch.Search(q, new ODataFilter(new IntranetNewsFilter(f, channels)))
            };
        }

       


    }
}