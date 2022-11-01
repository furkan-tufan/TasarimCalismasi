using System.ComponentModel.DataAnnotations.Schema;

namespace TasarimProjesi.Models
{
    public class Request
    {
        public int RequestId { get; set; }
        public string? RequestTitle { get; set; }
        public string? RequestDetail { get; set; }
        public string? Date { get; set; }
        public string? User { get; set; }
        [NotMapped]
        public virtual List<IFormFile>? Files { get; set; } = new List<IFormFile>();
        public virtual List<FileModel>? FileList { get; set; } = new List<FileModel>();

    }
}
