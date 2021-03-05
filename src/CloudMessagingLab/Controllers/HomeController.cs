using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudMessagingLab
{
    public class HomeController : Controller
    {
        // Methods
        public ActionResult<string> Index()
        {
            return "Hello World!";
        }
    }
}
