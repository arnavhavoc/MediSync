using MediSync.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace MediSync.Controllers
{
    //[HandleError(ExceptionType = typeof(System.Exception))]
    public class UserCredController : Controller
    {
        // GET: UserCred
        //public ActionResult Login()
        //{
           
        //    return View();
        //}

        [HttpPost]
        public ActionResult Login(UserLogin usr)
        {
         //   int i = int.Parse("");
            if (ModelState.IsValid)
            {
                MediSyncEntities _db = new MediSyncEntities();
                UserCred loggedUser = (from itr in _db.UserCreds
                                      where itr.userName == usr.userName && itr.userPassword == usr.userPassword
                                      select itr).FirstOrDefault();
                if(loggedUser != null)
                {
                    FormsAuthentication.SetAuthCookie(loggedUser.userName, false);

                    //Storing Current User Information
                    CurrentUserModel currentUserModel = new CurrentUserModel();
                    currentUserModel.currentUserID = loggedUser.userID;
                    currentUserModel.currentUserName = loggedUser.userName;
                    currentUserModel.currentUserReferenceToId = loggedUser.userReferenceToID;
                    currentUserModel.currentUserRole=loggedUser.userRole;
                    if(currentUserModel.currentUserRole == "Patient")
                    {
                        currentUserModel.currentUserFirstName = _db.Patients.Find(loggedUser.userReferenceToID).patientFirstName;
                        currentUserModel.currentUserLastName = _db.Patients.Find(loggedUser.userReferenceToID).patientLastName;
                    }
                    else if (currentUserModel.currentUserRole == "Physician")
                    {
                        currentUserModel.currentUserFirstName = _db.Physicians.Find(loggedUser.userReferenceToID).physicianFirstName;
                        currentUserModel.currentUserLastName = _db.Physicians.Find(loggedUser.userReferenceToID).physicianLastName;
                    }
                    else if (currentUserModel.currentUserRole == "Chemist")
                    {
                        currentUserModel.currentUserFirstName = _db.Chemists.Find(loggedUser.userReferenceToID).chemistName;
                        currentUserModel.currentUserLastName = "";
                    }
                    else if (currentUserModel.currentUserRole == "Supplier")
                    {
                        currentUserModel.currentUserFirstName = _db.Suppliers.Find(loggedUser.userReferenceToID).supplierName;
                        currentUserModel.currentUserLastName = "";
                    }

                    Session["CurrentUser"] = currentUserModel;
                    return RedirectToAction("Index", loggedUser.userRole);
                }
                else
                {
                    ModelState.AddModelError("", "Wrong Credentials!");
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        

        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    filterContext.ExceptionHandled = true;
        //    var vr = new ViewResult();
        //    vr.ViewName = @"~/Views/Shared/Error.cshtml";
        //    vr.ViewBag.errorInfo = filterContext.Exception.ToString();
        //    filterContext.Result = vr;

        //}
    }

}
