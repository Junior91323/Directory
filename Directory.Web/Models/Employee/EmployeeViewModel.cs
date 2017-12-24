using System.ComponentModel.DataAnnotations;

namespace Directory.Web.Models.Employee
{
    public class EmployeeViewModel : BaseViewModel
    {
        [Display(Name ="ФИО")]
        [Required(ErrorMessage = "Не указано ФИО")]
        public string FullName { get; set; }

        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Не указан номер телефона")]
        [Phone(ErrorMessage = "Формат номера непривильный")]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Неправильный формат (+7(999) 999-99-99)")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        [EmailAddress(ErrorMessage = "Формат Email непривильный")]
        public string Email { get; set; }
    }
}