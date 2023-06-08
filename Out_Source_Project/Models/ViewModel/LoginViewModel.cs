using System.ComponentModel.DataAnnotations;

namespace Out_Source_Project.Models.ViewModel
{
    public class LoginViewModel
    {
        [Key]
        [MaxLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MaxLength(30, ErrorMessage = "Mật khẩu không được quá 30 ký tự")]
        public string Password { get; set; }
    }
}
