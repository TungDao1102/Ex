using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Out_Source_Project.Extension;
using Out_Source_Project.Models;
using Out_Source_Project.Models.Authentication;
using Out_Source_Project.Models.ViewModel;
using System.Security.Claims;

namespace Out_Source_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    //  [Authorize(Roles ="Admin")]
    
    public class AdminController : Controller
	{
        private readonly OutSourceContext _context;
        public AdminController(OutSourceContext context)
        {
            _context = context;
        }
           [Authentication]
      //  [Authorize]
        public IActionResult Index()
		{
			return View();
		}
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID != null)
            {
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Account kh = _context.Accounts.Include(x => x.Role).SingleOrDefault(x => x.Email.ToLower() == model.Email.ToLower().Trim());
                    if (kh == null)
                    {
                        ViewBag.Error = "Thông tin đăng nhập chưa chính xác";
                        return View(model);
                    }
                 //  string pass = (model.Password.Trim() + kh.Salt.Trim().ToMD5());
                    string pass = model.Password.Trim();
                    if (kh.Password.Trim() != pass)
                    {
                        ViewBag.Error = "Thông tin đăng nhập chưa chính xác";
                        return View(model);
                    }
                    //kh.LastLogin = DateTime.Now;
                    //_context.Update(kh);
                    //await _context.SaveChangesAsync();

                    var TaiKhoanID = HttpContext.Session.GetString("AccountId");
                    HttpContext.Session.SetString("AccountId", kh.AccountId.ToString());

                    //var UserClaims = new List<Claim>
                    //{
                    //    new Claim(ClaimTypes.Name , kh.FullName),
                    //    new Claim(ClaimTypes.Email, kh.Email),
                    //    new Claim("AccountId", kh.AccountId.ToString()),
                    //     new Claim("RoleId", kh.RoleId.ToString()),
                    //     new Claim(ClaimTypes.Role , kh.Role.RoleName)
                    //};
                    //var GrandmaIdentity = new ClaimsIdentity(UserClaims, "User Identity");
                    //var UserPrincipal = new ClaimsPrincipal(new[] { GrandmaIdentity });
                    //await HttpContext.SignInAsync(UserPrincipal);

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Admin", new { area = "Admin" });
                }
            }
            catch
            {
                return RedirectToAction("Login", "Admin", new { area = "Admin" });
            }
            return RedirectToAction("Login", "Admin", new { area = "Admin" });
        }

        public IActionResult Logout()
        {
                HttpContext.SignOutAsync();
                HttpContext.Session.Remove("AccountId");
                return RedirectToAction("Login", "Admin", new { area = "Admin" });
        }
        //[Authorize, HttpGet]
        // public IActionResult ChangePassWord()
        //{
        //    if(!User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Login", "Admin", new { area = "Admin" });
        //    }
        //    var taikhoanID = HttpContext.Session.GetString("AccountId");
        //    if(taikhoanID ==null)
        //    {
        //        return RedirectToAction("Login", "Admin", new { area = "Admin" });
        //    }
        //    return View();
        //}
        //[Authorize, HttpPost]
        //public IActionResult ChangePassword(ChangePasswordViewModel model) 
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Login", "Admin", new { area = "Admin" });
        //    }
        //    var taikhoanID = HttpContext.Session.GetString("AccountId");
        //    if(taikhoanID ==null)
        //    {
        //        return RedirectToAction("Login", "Admin", new { area = "Admin" });
        //    }
        //    if(ModelState.IsValid)
        //    {
        //        var account = _context.Accounts.AsNoTracking().SingleOrDefault(x => x.AccountId == int.Parse(taikhoanID));
        //        if (account == null)
        //        {
        //            return NotFound();
        //        }
        //        try
        //        {
        //            string passnow = (model.PasswordNow.Trim() + account.Salt.Trim()).ToMD5();
        //            if(passnow == account.Password.Trim())
        //            {
        //                account.Password = (model.Password.Trim() + account.Salt.Trim()).ToMD5();
        //                _context.Update(account);
        //                _context.SaveChanges();
        //                return RedirectToAction("Index", "Admin", new { area = "Admin" });
        //            }
        //            else
        //            {
        //                return View();
        //            }
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }
        //    return View();
        //}
        
        //public IActionResult EditProfile()
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Login", "Admin", new { area = "Admin" });
        //    }
        //    var taikhoanID = HttpContext.Session.GetString("AccountId");
        //    if (taikhoanID == null)
        //    {
        //        return RedirectToAction("Login", "Admin", new { area = "Admin" });
        //    }
        //    var account = _context.Accounts.AsNoTracking().SingleOrDefault(x => x.AccountId == int.Parse(taikhoanID));
        //    if (account == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(account);
        //}

        //[HttpPost]
        //public IActionResult EditProfile(Account model)
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Login", "Admin", new { area = "Admin" });
        //    }
        //    var taikhoanID = HttpContext.Session.GetString("AccountId");
        //    if (taikhoanID == null)
        //    {
        //        return RedirectToAction("Login", "Admin", new { area = "Admin" });
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        var account = _context.Accounts.AsNoTracking().SingleOrDefault(x => x.AccountId == int.Parse(taikhoanID));
        //        if (account == null)
        //        {
        //            return NotFound();
        //        }
        //        try
        //        {
        //            account.FullName = model.FullName;
        //            account.Email = model.Email;
        //            account.Phone = model.Phone;
        //            _context.Update(account);
        //            _context.SaveChanges();
        //            return RedirectToAction("Index", "Admin", new { area = "Admin" });
                    
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }
        //    return View();
        //}
    }
}
