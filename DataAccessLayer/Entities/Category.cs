using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
	public class Category
	{
		public int Id { get; set; }
		
		[Required]
		[MaxLength(127)]
		public string Name { get; set; }
	}
}