using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
	public class AppointmentService
	{
		public int Id { get; set; }

		[Required]
		public int AppointmentId { get; set; }
		public Appointment Appointment { get; set; }

		[Required]
		public int ServiceId { get; set; }
		public Service Service { get; set; }
	}
}
