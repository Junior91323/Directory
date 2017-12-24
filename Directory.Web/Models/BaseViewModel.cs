namespace Directory.Web.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class BaseViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid? Uid { get; set; }

        [Display(Name = "Дата создания")]
        [ReadOnly(true)]
        [HiddenInput]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Дата редактирования")]
        [ReadOnly(true)]
        [HiddenInput]
        public DateTime UpdateDate { get; set; }

        [ReadOnly(true)]
        [HiddenInput]
        public bool IsActive { get; set; }
    }
}