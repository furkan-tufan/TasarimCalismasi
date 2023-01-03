using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasarimProjesi.Models
{
    public class Request
    {
        public Request()
        {
            IsOver = false;
        }
        public int RequestId { get; set; }
        [Display(Name = "Talep Başlığı")]

        public string? RequestTitle { get; set; }
        [Display(Name = "Talep Detayı")]

        public string? RequestDetail { get; set; }
        [Display(Name = "Tarih")]

        public string? Date { get; set; }
        [Display(Name = "Destek Mesajı")]

        public string? HelpContent { get; set; }
        [Display(Name = "Talebi Açan")]
        public bool IsOver { get; set; }
        [Display(Name = "Talebi Açan")]

        public string? User { get; set; }
        [NotMapped]
        public virtual List<IFormFile>? Files { get; set; } = new List<IFormFile>();
        public virtual List<FileModel>? FileList { get; set; } = new List<FileModel>();

    }
}
