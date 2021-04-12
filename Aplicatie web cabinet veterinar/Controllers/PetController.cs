using Aplicatie_web_cabinet_veterinar.Models.ApplicationModels;
using Aplicatie_web_cabinet_veterinar.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aplicatie_web_cabinet_veterinar.Controllers
{
    public class PetController : Controller
    {
        private readonly PetsRepository _petsRepo = new PetsRepository(new Models.ApplicationDbContext());
        public PetController()
        {
        }
        public ActionResult Index()
        {
            return View(_petsRepo.GetPetsByOwner(2));
        }

        public ActionResult Show(int id)
		{
            var pet = _petsRepo.Get(id);
            if (pet == null)
                return HttpNotFound();

            return View(pet);
        }

        [HttpGet]
        public ActionResult AddEdit(int? id)
        {
            PetModel pet = new PetModel() { OwnerId = 2};
            
            if (id.GetValueOrDefault() != default)
                pet = _petsRepo.Get(id.GetValueOrDefault());

            if (pet == null && id.GetValueOrDefault() != default)
                return HttpNotFound();
            else
                return View("PetForm", pet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(PetModel pet)
        {
            if (!ModelState.IsValid)
                return View("PetForm", pet);
			try
			{
                _petsRepo.AddOrUpdate(pet);
            }
            catch(Exception e)
			{
                return HttpNotFound();
			}


            if (pet.Id == 0)
                return RedirectToAction("Index", "Pet");

            return RedirectToAction("Show", new { id = pet.Id });
        }
    }
}