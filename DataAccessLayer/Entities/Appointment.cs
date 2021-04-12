using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
	public class Appointment
	{
		public int Id { get; set; }

		[Required]
		public double EstimativePrice { get; set; }

		public double? Price { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[Required]
		public int PetId { get; set; }
		public Pet Pet { get; set; }

		[Required]
		public int MedicId { get; set; }
		public User Medic { get; set; }
	}
}
