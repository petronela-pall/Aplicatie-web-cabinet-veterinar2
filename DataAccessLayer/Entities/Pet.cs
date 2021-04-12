using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
	public class Pet
	{
		public int Id  { get; set; }
		
		[Required]
		[MaxLength(63)]
		public string Name { get; set; }

		[Required]
		[MaxLength(63)]
		public string Species { get; set; }

		[MaxLength(63)]
		public string Breed { get; set; }

		[Required]
		public DateTime BirthDate { get; set; }

		[Required]
		public int OwnerId { get; set; }
		public User Owner { get; set; }
	}
}
