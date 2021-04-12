using Aplicatie_web_cabinet_veterinar.Models;
using Aplicatie_web_cabinet_veterinar.Models.ApplicationModels;
using Aplicatie_web_cabinet_veterinar.Models.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using static Aplicatie_web_cabinet_veterinar.Models.ApplicationModels.CommonStuff;

namespace Aplicatie_web_cabinet_veterinar.Repositories
{
	public class UsersRepository
	{
		private readonly ApplicationDbContext _db;
		public UsersRepository(ApplicationDbContext db)
		{
			_db = db;
		}
		public List<MedicModel> GetMedicsForAppointment()
		{
			return _db.Users
				.Where(u => u.Specialization != null && u.Specialization != "" && u.RoleId == (int)Roles.Angajat)
				.Select(m => new MedicModel
				{
					Id = m.Id,
					FirstName = m.FirstName,
					LastName = m.LastName,
					Specialization = m.Specialization
				})
				.ToList();
		}
		public bool DeleteUser(int id)
		{
			if (id != 0)
			{
				var user = _db.Users.Where(u => u.Id == id).FirstOrDefault();
				user.IsDeleted = true;
				_db.SaveChanges();
				return true;
			}
			return false;
			
		}
		public bool Authenticate(UserAuthenticationModel model)
		{
			if (model != null)
			{
				var md5 = MD5.Create();
				var userInDb = _db.Users.Include(u => u.Role).Where(u => u.Email == model.Email).FirstOrDefault();
				if (userInDb != null)
				{
					var pas = Convert.ToBase64String(md5.ComputeHash(System.Text.UTF8Encoding.Default.GetBytes(model.Password)));
					if (pas == userInDb.Password)
					{
						var loggedUser = new LoggedUser()
						{
							Id = userInDb.Id,
							Email = userInDb.Email,
							Name = string.Concat(userInDb.LastName, " ", userInDb.FirstName),
							RoleId = userInDb.RoleId
						};
						HttpContext.Current.Session.Add("LoggedUser",loggedUser);
						return true;
					}
					
					else return false;
				}
				else
				{
					return false;
				}
			}
			return false;
		}
		public void SaveSignUpUser(UserSignUpModel model)
		{
			var md5 = MD5.Create();
			var pas = Convert.ToBase64String(md5.ComputeHash(System.Text.UTF8Encoding.Default.GetBytes(model.Password)));
			_db.Users.Add(new User
			{
				IsDeleted = false,
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Password = pas,
				PhoneNumber = model.PhoneNumber,
				RoleId = (int)Roles.Utilizator
			});
			_db.SaveChanges();
		}
		
	}
}