using Aplicatie_web_cabinet_veterinar.Models;
using Aplicatie_web_cabinet_veterinar.Models.ApplicationModels;
using Aplicatie_web_cabinet_veterinar.Models.DatabaseEntities;
using Aplicatie_web_cabinet_veterinar.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using static Aplicatie_web_cabinet_veterinar.Models.ApplicationModels.CommonStuff;

namespace Aplicatie_web_cabinet_veterinar.Repositories
{
	public class AppointmentsRepository
	{
		private readonly ApplicationDbContext _db;
		public AppointmentsRepository(ApplicationDbContext db)
		{
			_db = db;
		}
		public List<AppointmentModel> GetAppointments(int userId, int roleId)
		{
			if (userId == 0 || roleId == 0)
				throw new Exception();
			if(roleId == (int)Roles.Admin)
				return _db.Appointments
				.Select(a => new AppointmentModel()
				{
					Id = a.Id,
					Date = a.Date,
					EstimativePrice = a.EstimativePrice,
					Price = a.EstimativePrice,
					MedicId = a.MedicId,
					PetId = a.PetId,
					MedicName = a.Medic.LastName + a.Medic.FirstName,
					PetName = a.Pet.Name,
					ClientEmail = a.Pet.Owner.Email
				})
				.ToList();

			if(roleId == (int)Roles.Angajat)
				return _db.Appointments
					.Where(a => a.MedicId == userId)
					.Select(a => new AppointmentModel()
					{
						Id = a.Id,
						Date = a.Date,
						EstimativePrice = a.EstimativePrice,
						Price = a.EstimativePrice,
						MedicId = a.MedicId,
						PetId = a.PetId,
						MedicName = a.Medic.LastName + a.Medic.FirstName,
						PetName = a.Pet.Name,
						ClientEmail = a.Pet.Owner.Email 
					})
					.ToList();

			//normal user
			return _db.Appointments
					.Where(a => a.Pet.OwnerId == userId)
					.Select(a => new AppointmentModel()
					{
						Id = a.Id,
						Date = a.Date,
						EstimativePrice = a.EstimativePrice,
						Price = a.EstimativePrice,
						MedicId = a.MedicId,
						PetId = a.PetId,
						MedicName = a.Medic.LastName + a.Medic.FirstName,
						PetName = a.Pet.Name,
						ClientEmail = a.Pet.Owner.Email
					})
					.ToList();
		}

		public AppointmentModel Get(int id)
		{
			return _db.Appointments
			.Where(a => a.Id == id)
			.Select(a => new AppointmentModel()
			{ 
				Id = a.Id,
				ClientEmail = a.Pet.Owner.Email,
				Date = a.Date,
				EstimativePrice = a.EstimativePrice,
				Price = a.Price,
				MedicId = a.MedicId,
				MedicName = a.Medic.LastName + a.Medic.FirstName,
				PetId = a.PetId,
				PetName = a.Pet.Name
				
			})
			.SingleOrDefault();
		}

		public AppointmentModel AddOrUpdate(AppointmentModel appointment)
		{
			if(appointment.Id == 0)
			{
				_db.Appointments.Add(new Appointment() 
				{ 
					Date = appointment.Date,
					EstimativePrice = appointment.EstimativePrice,
					MedicId = appointment.MedicId,
					PetId = appointment.PetId,
					Price = appointment.Price					
				});
			}
			else
			{
				Appointment dbAppointment = _db.Appointments.SingleOrDefault();
				if (dbAppointment == null)
					throw new Exception();

				dbAppointment.Date = appointment.Date;
				dbAppointment.EstimativePrice = appointment.EstimativePrice;
				dbAppointment.MedicId = appointment.MedicId;
				dbAppointment.PetId = appointment.PetId;
				dbAppointment.Price = appointment.Price;
			}

			_db.SaveChanges();
			return appointment;
		}

		public void Delete(int id)
		{
			Appointment appointment = _db.Appointments.Remove(new Appointment() { Id = id });
			if (appointment == null)
				throw new Exception();
		}
	}
}