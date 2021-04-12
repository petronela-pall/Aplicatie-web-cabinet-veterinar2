using Aplicatie_web_cabinet_veterinar.Models.ApplicationModels;
using Aplicatie_web_cabinet_veterinar.Repositories;
using Aplicatie_web_cabinet_veterinar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aplicatie_web_cabinet_veterinar.Controllers
{
    public class AppointmentController : Controller
    {
		private readonly AppointmentsRepository _appointmentsRepo = new AppointmentsRepository(new Models.ApplicationDbContext());
        private readonly UsersRepository _usersRepo = new UsersRepository(new Models.ApplicationDbContext());
        private readonly PetsRepository _petsRepo = new PetsRepository(new Models.ApplicationDbContext());
        public AppointmentController()
		{
		}
        public ActionResult Index()
        {
			try
			{
                return View(_appointmentsRepo.GetAppointments((Session["loggedUser"] as LoggedUser).Id, (Session["loggedUser"] as LoggedUser).RoleId));
			}
            catch(Exception e)
			{
                return HttpNotFound();
			}
        }

        public ActionResult Show(int id)
        {
            AppointmentModel appointment = _appointmentsRepo.Get(id);
            if (appointment == null)
                return HttpNotFound();

            return View(appointment);
        }
        [HttpGet]
        public ActionResult AddEdit(int? id)
        {
            var model = new AppointmentViewModel()
            {
                Appointment = new AppointmentModel(),
                Medics = _usersRepo.GetMedicsForAppointment(),
                Pets = _petsRepo.GetPetsByOwner((Session["loggedUser"] as LoggedUser).Id)
            };
            if (id.GetValueOrDefault() != default)
               model.Appointment = _appointmentsRepo.Get(id.GetValueOrDefault());


            return View("AppointmentForm", model);
        }
        [HttpPost]
        public ActionResult AddEdit(AppointmentModel appointment)
        {
            if(!ModelState.IsValid)
			{
                return View("AppointmentForm", new AppointmentViewModel()
                {
                    Appointment = appointment,
                    Medics = _usersRepo.GetMedicsForAppointment(),
                    Pets = _petsRepo.GetPetsByOwner((Session["loggedUser"] as LoggedUser).Id)
                });
			}
			try
			{
                _appointmentsRepo.AddOrUpdate(appointment);

            }
            catch (Exception e)
			{
                return HttpNotFound();
			}


            if (appointment.Id == 0)
                return RedirectToAction("Index", "Service");

            return RedirectToAction("Show", new { id = appointment.Id });
        }
        public ActionResult Delete(int id)
        {
           throw new NotImplementedException();
        }
    }
}