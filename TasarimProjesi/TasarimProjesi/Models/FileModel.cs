﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasarimProjesi.Models
{
    public class FileModel
    {
        [Key]
        public int FileId { get; set; }
        public string? Name { get; set; }
        public string? FileType { get; set; }
        public string? Extension { get; set; }
        public string? Description { get; set; }
        public string? UploadedBy { get; set; }
        public string? CreatedOn { get; set; }
        public byte[]? Data { get; set; }
        [ForeignKey("Request")]
        public int? RequestId { get; set; }
        public virtual Request? Request { get; set; }

        [ForeignKey("PurchasingItem")]
        public int? PurchasingItemId { get; set; }
        public virtual PurchasingItem? PurchasingItem { get; set; }

    }
}