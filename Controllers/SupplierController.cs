using MediSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediSync.Controllers
{
    [Authorize(Roles ="Supplier")]
    public class SupplierController : Controller
    {
        // GET: Supplier
        public ActionResult Index()
        {
            MediSyncEntities db = new MediSyncEntities();
            return View(db.PurchaseOrderHeaders.ToList());
        }
    }
}