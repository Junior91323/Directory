namespace Directory.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BaseModel
    {
        [Key]
        [Required]
        public Guid Uid { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public bool IsActive { get; set; }
    }
}
