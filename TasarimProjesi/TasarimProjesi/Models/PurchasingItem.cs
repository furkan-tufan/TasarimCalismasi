using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace TasarimProjesi.Models
{
    public class PurchasingItem
    {
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
        public Currency? Currency { get; set; }
        [Display(Name = "Fiyat")]
        public decimal? Price { get; set; }
    }
    public enum Currency
    {
        TRY, EUR, GBP, USD
    }
}
