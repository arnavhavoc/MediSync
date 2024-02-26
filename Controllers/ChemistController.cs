using MediSync.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediSync.Controllers
{
    [Authorize(Roles ="Chemist")]
    public class ChemistController : Controller
    {
        // GET: Chemist
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewDrugRequests()
        {
            MediSyncEntities db = new MediSyncEntities();
            return View(db.DrugRequests.ToList());
        }


        public ActionResult MarkRequestCompleted(int? drugRequestID)
        {
            MediSyncEntities db = new MediSyncEntities();
            DrugRequest drugRequest = (from drg in db.DrugRequests
                                      where drg.drugRequestID == drugRequestID
                                      select drg).FirstOrDefault();
            drugRequest.drugRequestStatus = "Placed";
            db.Entry(drugRequest).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ViewDrugRequests", "Chemist");
        }


        public ActionResult PlaceDrugOrder()
        {
            MediSyncEntities db = new MediSyncEntities();
            ViewBag.Supplier = db.Suppliers.ToList();
            ViewBag.Drugs = db.Drugs.ToList();
            var model = new POHeaderModel
            {
                POLines = new List<POLineModel> { new POLineModel() }
            };
            return View(model);
        }

        //[HttpPost]
        //public ActionResult PlaceDrugOrder(POHeaderModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        MediSyncEntities db = new MediSyncEntities();
        //        PurchaseOrderHeader purchaseOrderHeader = new PurchaseOrderHeader();
        //        purchaseOrderHeader.purchaseOrderDate = DateTime.Now.Date;
        //        purchaseOrderHeader.purchaseOrderSupplierID = model.purchaseOrderSupplierID;
        //        db.PurchaseOrderHeaders.Add(purchaseOrderHeader);
        //        db.SaveChanges();

        //        foreach (var item in model.POLines)
        //        {
        //            PurchaseProductLine purchaseProductLine = new PurchaseProductLine();
        //            purchaseProductLine.purchaseOrderSerialNo = purchaseOrderHeader.purchaseOrderID;
        //            purchaseProductLine.purchaseOrderDrugID = item.purchaseOrderDrugID;
        //            purchaseProductLine.purchaseOrderQuantity = item.purchaseOrderQuantity;
        //            purchaseProductLine.purchaseOrderNote = item.purchaseOrderNote;
        //            db.PurchaseProductLines.Add(purchaseProductLine);
        //            db.SaveChanges();

        //        }
        //        return RedirectToAction("Index", "Chemist");

        //    }
        //    return RedirectToAction("Index", "Chemist");

        //}

        [HttpPost]
        public ActionResult PlaceDrugOrder(POHeaderModel model)
        {
            if (ModelState.IsValid)
            {
                MediSyncEntities db = new MediSyncEntities();
                PurchaseOrderHeader purchaseOrderHeader = new PurchaseOrderHeader();
                purchaseOrderHeader.purchaseOrderDate = DateTime.Now.Date;
                purchaseOrderHeader.purchaseOrderSupplierID = model.purchaseOrderSupplierID;
                db.PurchaseOrderHeaders.Add(purchaseOrderHeader);
                db.SaveChanges();

                //foreach (var item in model.POLines)
                //{
                //    PurchaseProductLine purchaseProductLine = new PurchaseProductLine();
                //    purchaseProductLine.purchaseOrderID = purchaseOrderHeader.purchaseOrderID; // Set the correct purchaseOrderID
                //    purchaseProductLine.purchaseOrderDrugID = item.purchaseOrderDrugID;
                //    purchaseProductLine.Drug = db.Drugs.Find(item.purchaseOrderDrugID);
                //    purchaseProductLine.purchaseOrderSerialNo = 0;
                //    purchaseProductLine.purchaseOrderQuantity = item.purchaseOrderQuantity;
                //    purchaseProductLine.purchaseOrderNote = item.purchaseOrderNote;
                //    db.PurchaseProductLines.Add(purchaseProductLine);
                //    db.SaveChanges();
                //}
                return RedirectToAction("Index", "Chemist");
            }
            return RedirectToAction("Index", "Chemist");
        }



    }
}