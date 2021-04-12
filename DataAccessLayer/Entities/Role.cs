using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
	public class Role
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(31)]
		public string Name { get; set; }
	}
}