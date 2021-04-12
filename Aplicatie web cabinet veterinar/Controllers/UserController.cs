using Aplicatie_web_cabinet_veterinar.Models.ApplicationModels;
using Aplicatie_web_cabinet_veterinar.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aplicatie_web_cabinet_veterinar.Controllers
{
    public class UserController : Controller
    {
        private readonly UsersRepository _usersRepo = new UsersRepository(new Models.ApplicationDbContext());
		public UserController()
		{
		}
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
		public ActionResult ShowMedics()
		{
			var medicsList = _usersRepo.GetMedicsForAppointment();
			return View(medicsList);
		}
        public ActionResult Delete(int id)
        {
            var result = _usersRepo.DeleteUser(id);
            if (result)
            {
                return View();
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Authenticate()
        {
            var model = new UserAuthenticationModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Authenticate(UserAuthenticationModel model)
        {
            var result = _usersRepo.Authenticate(model);
            if (result)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Introduceti datele corecte de autentificare.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            var model = new UserSignUpModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult SignUp(UserSignUpModel model)
        {
            if(!ModelState.IsValid)
			{
                return View(model);
            }
			else
			{
                _usersRepo.SaveSignUpUser(model);
                //autentifica userul
                this.Authenticate(new UserAuthenticationModel { Email = model.Email, Password = model.Password });
                return RedirectToAction("Index", "Home");
            }
            
        }
        public ActionResult LogOut()
        {
            Session["LoggedUser"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}