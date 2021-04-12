using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
	public class ApplicationDbContext: DbContext
	{
		public ApplicationDbContext() :base()
		{

		}

		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<AppointmentService> AppointmentServices { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Pet> Pets { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Service> Services { get; set; }
		public DbSet<User> Users { get; set; }
	}
}
