using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VienShops.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập Email")]
        [Display(Name = "Email")]
		[StringLength(255, ErrorMessage = "Email dài tối đat 255 kí tự")]
		public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Lưu trình duyệt ?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập Email")]
        [Display(Name = "Email")]
		[StringLength(255, ErrorMessage = "Email tối đa 255 kí tự ")]
		public string Email { get; set; }
    }

	public class LoginViewModel
	{
		[Required(ErrorMessage = "Bạn Chưa nhập Email")]
		[Display(Name = "Email")]
		[EmailAddress]
		[StringLength(255, ErrorMessage = "Email tối đa 255 kí tự ")]
		public string Email { get; set; }

		[Required (ErrorMessage = "Bạn chưa nhập mật khẩu ")]
		[DataType(DataType.Password)]
		[Display(Name = "Mật khẩu")]
		public string Password { get; set; }

		[Display(Name = "Remember me?")]
		public bool RememberMe { get; set; }

    }

    public class RegisterViewModel
    {
		[Required(ErrorMessage = "Bạn chưa nhập Tên")]
		[Display(Name = "Tên")]
		[StringLength(255)]
		public string FullName { get; set; }
		[Required(ErrorMessage = "Bạn chưa nhập Email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Mật Khẩu phải có tối đa {0} kí tự và tối thiểu {2} kí tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác thực mật khẩu")]
        [Compare("Password", ErrorMessage = "Nhập lại mật khẩu không đúng ")]
        public string ConfirmPassword { get; set; }
		[Required(ErrorMessage = "Bạn chưa nhập địa chỉ")]
		[Display(Name = "Địa chỉ")]
		[StringLength(255)]
		public string Adress { get; set; }
	}

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
		[StringLength(255, ErrorMessage = "Email không được bỏ trống")]
		public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Nhập lại mật khẩu không đúng .")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
		[StringLength(255, ErrorMessage = "Email không được bỏ trống")]
		public string Email { get; set; }
    }
}
