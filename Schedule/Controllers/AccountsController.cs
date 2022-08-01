using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schedule.Entities;
using Schedule.Helpers;
using Schedule.Models.Accounts;
using Schedule.Services;
using System;
using System.Collections.Generic;

namespace Schedule.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountsController(
            IAccountService accountService,
            IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost("authenticate")]
        public ActionResult<AccountAuthenticateResponse> Authenticate(AccountAuthenticateRequest model)
        {
            var response = _accountService.Authenticate(model, ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public ActionResult<AccountAuthenticateResponse> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = _accountService.RefreshToken(refreshToken, ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("revoke-token")]
        public IActionResult RevokeToken(AccountRevokeTokenRequest model)
        {
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            if (!Account.OwnsToken(token) && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            _accountService.RevokeToken(token, ipAddress());
            return Ok(new { message = "Token revogado" });
        }

        [HttpPost("register")]
        public IActionResult Register(AccountRegisterRequest model)
        {
            _accountService.Register(model, Request.Headers["origin"]);
            return Ok(new { message = "Registro bem-sucedido, verifique seu e-mail para obter instruções de verificação" });
        }


        [HttpPost("verify-email")]
        public IActionResult VerifyEmail(AccountVerifyEmailRequest model)
        {
            _accountService.VerifyEmail(model.Token);
            return Ok(new { message = "Verificação bem-sucedida, agora você pode fazer login" });
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(AccountForgotPasswordRequest model)
        {
            _accountService.ForgotPassword(model, Request.Headers["origin"]);
            return Ok(new { message = "Verifique seu e-mail para obter instruções de redefinição de senha" });
        }

        [HttpPost("validate-reset-token")]
        public IActionResult ValidateResetToken(AccountValidateResetTokenRequest model)
        {
            _accountService.ValidateResetToken(model);
            return Ok(new { message = "Token válido" });
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword(AccountResetPasswordRequest model)
        {
            _accountService.ResetPassword(model);
            return Ok(new { message = "Redefinição de senha bem-sucedida, agora você pode fazer login" });
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public ActionResult<IEnumerable<AccountResponse>> GetAll()
        {
            var accounts = _accountService.GetAll();
            return Ok(accounts);
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public ActionResult<AccountResponse> GetById(int id)
        {
            //Admins acessam qualquer conta. Usuários só podem acessar sua própria conta
            if (id != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Não Autorizado" });

            var account = _accountService.GetById(id);
            return Ok(account);
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        public ActionResult<AccountResponse> Create(AccountCreateRequest model)
        {
            var account = _accountService.Create(model);
            return Ok(account);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public ActionResult<AccountResponse> Update(int id, AccountUpdateRequest model)
        {
            //Admins conseguem alterar qualquer conta
            if (id != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Não Autorizado" });

            if (Account.Role != Role.Admin)
                model.Role = null;

            var account = _accountService.Update(id, model);
            return Ok(account);
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            //Admins conseguem deletar qualquer conta
            if (id != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Não Autorizado" });

            _accountService.Delete(id);
            return Ok(new { message = "Conta excluída com sucesso" });
        }


        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
