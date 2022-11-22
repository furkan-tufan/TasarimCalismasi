using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasarimProjesi.Models
{
	public class Comment
	{
		[Key]
		public int CommentId { get; set; }
		[ForeignKey("PurchasingItem")]
		public int PurchasingItemId { get; set; }
        public string? Time { get; set; }
		public string? CommentDetail { get; set; }
        public string? User { get; set; }
    }
}
