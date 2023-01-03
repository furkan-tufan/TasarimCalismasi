using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TasarimProjesi.Models
{
    public class PurchasingItem
    {
        public PurchasingItem()
        {
            Uploaded = false;
        }

        [Key]
        public int PurchasingItemId { get; set; }
        [ForeignKey("Purchasing")]
        [Display(Name = "Sipariş No")]
        public int PurchasingId { get; set; }
        public Purchasing? Purchasing { get; set; }
        [Display(Name = "Ürün")]
        [Required(ErrorMessage = "Ürün Giriniz")]
        public string? Product { get; set; }
        [Required(ErrorMessage = "Birim Giriniz")]
        [Display(Name = "Birim")]
        public string? Unit { get; set; }
        [Required(ErrorMessage = "Miktar Giriniz")]
        [Display(Name = "Miktar")]
        public decimal? Amount { get; set; }
        [Display(Name = "Marka")]
        [Required(ErrorMessage = "Marka Giriniz")]
        public string? Brand { get; set; }
        [EnumDataType(typeof(Currency))]
        [Display(Name = "Para Birimi")]
        public Currency? Currency { get; set; }
        [Display(Name = "Fiyat")]
        public decimal? Price { get; set; }
        public bool Uploaded { get; set; }

        [NotMapped]
        public virtual List<IFormFile>? Invoices { get; set; } = new List<IFormFile>();
        public virtual List<FileModel>? FileList { get; set; } = new List<FileModel>();
    }
    public enum Currency
    {
        TRY, EUR, GBP, USD
    }
}
