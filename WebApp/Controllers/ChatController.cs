using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using Database;

namespace WebApp.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Index()
        {
            var user = HttpContext.User.Identity.Name;
            return View();
        }

        #region Customer chat


        [AllowAnonymous]
        public ActionResult Customer()
        {
            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        [ValidateInput(false)]
        [AllowAnonymous]
        public JsonResult AddChat(Question question)
        {
            var manager = new DataManager();
            if (ModelState.IsValid)
            {
                string sessionID = HttpContext.Session.SessionID;

                manager.CreatQuestion(new Question()
                {
                    Text = question.Text,
                    SessionId = sessionID
                });
            }
            string retVal = "success";
            return Json(retVal, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetQuestion()
        {
            var manager = new DataManager();
            var questions = manager.GetActiveQuestions(HttpContext.Session.SessionID).ToList();
            return Json(questions, JsonRequestBehavior.AllowGet);
        }

        #endregion Customer chat



    }
}