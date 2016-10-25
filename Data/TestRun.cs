using System.ComponentModel.DataAnnotations;

namespace Data
{
	public class TestRun
	{
		public int Id { get; set; }
		[Required]
		public string Test { get; set; }
		public string BuildAgent { get; set; }
		public bool Success { get; set; }
	}
}
