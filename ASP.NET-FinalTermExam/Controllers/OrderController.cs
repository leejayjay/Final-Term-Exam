using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_FinalTermExam.Controllers
{
    public class OrderController : Controller
    {
        // GET: DropList
        public ActionResult Droplistemp(Models.Order Droplist)
        {
            return View();
        }
        // GET: Order
        public ActionResult Index(Models.Order Order)
        {
            Models.OrderService orderservice = new Models.OrderService();
            Models.OrderService droplistemp = new Models.OrderService();
            Models.OrderService droplistship = new Models.OrderService();
            var order = orderservice.GetOrderByCondition(Order);
            ViewData["Dropemp"] = droplistemp.DropDownListEmp();
            ViewData["Dropship"] = droplistship.DropDownListship();
            ViewBag.order = order;
            return View();
        }
    }
}