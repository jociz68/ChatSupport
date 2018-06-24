using Entities;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApp.Models;
using Database;
using Helper;

namespace WebApp.Controllers
{
    [Authorize]
    public class ServiceController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Service service)
        {
            if (ModelState.IsValid)
            {
                var manager = new DataManager();
                if (manager.IsValidLogin(service.Login, PasswordHash.GenerateSHA256Hash(service.Password)))
                {
                    FormsAuthentication.SetAuthCookie(service.Login, true);
                    return RedirectToAction("Index", "Chat");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(service);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Chat");
        }


        public ActionResult Support()
        {

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddAnswer(AnswerModel model)
        {
            var manager = new DataManager();
            var user = User.Identity.Name;
            var userId = manager.GetServiceUserId(user);
            var answer = new Answer()
            {
                Text = model.Text
            };
            if (userId != -1)
            {
                answer.ServiceId = userId;
            }

            var retVal = manager.CreateAnswer(answer, int.Parse(model.QuestionId));
            return Json(retVal, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetQuestion()
        {
            var manager = new DataManager();
            var questions = manager.GetQuestionsNotAnswered().ToList();
            return Json(questions, JsonRequestBehavior.AllowGet);
        }


    }
}