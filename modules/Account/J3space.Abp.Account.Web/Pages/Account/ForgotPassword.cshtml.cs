using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace J3space.Abp.Account.Web.Pages.Account
{
    public class ForgotPasswordModel : AccountPageModel
    {
        [Required]
        [EmailAddress]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxEmailLength))]
        [BindProperty]
        public string Email { get; set; }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string ReturnUrlHash { get; set; }

        public virtual Task<IActionResult> OnGetAsync()
        {
            return Task.FromResult<IActionResult>(Page());
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await AccountAppService.SendPasswordResetCodeAsync(
                    new SendPasswordResetCodeDto
                    {
                        Email = Email,
                        AppName = "MVC",
                        ReturnUrl = ReturnUrl,
                        ReturnUrlHash = ReturnUrlHash
                    }
                );

                return RedirectToPage(
                    "./PasswordResetLinkSent",
                    new
                    {
                        returnUrl = ReturnUrl,
                        returnUrlHash = ReturnUrlHash
                    });
            }
            // from AccountService.CheckSelfRegistrationAsync
            catch (UserFriendlyException e)
            {
                MyAlerts.Warning(e.Message, L["OperationFailed"]);
                return await OnGetAsync();
            }
            // from AccountService.GetUserByEmail
            catch (BusinessException e)
            {
                var message = L[e.Code, e.Data["Email"]];
                MyAlerts.Warning(message, L["OperationFailed"]);
                return await OnGetAsync();
            }
        }
    }
}