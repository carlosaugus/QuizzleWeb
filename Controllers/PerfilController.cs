using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Quizzle.Models;
using Quizzle.Repositorio;

namespace Quizzle.Controllers
{
    public class PerfilController : Controller
    {
        private readonly IConexao _conexao;

        public PerfilController(IConexao conexao)
        {
            _conexao = conexao;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm]PerfilViewModel model)
        {
            PerfilViewModel perfil = null;

            using (var conn = _conexao.AbrirConexao())
            {
                string queryQuery = $"SELECT * FROM PERFIL WHERE LOGIN = '{model.Login}' AND SENHA = '{model.Senha}'; ";
                perfil = conn.QueryFirst<PerfilViewModel>(queryQuery);
            }
            if (perfil != null)
            {
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimValueTypes.String, perfil.Id_Perfil.ToString(), "Id_Perfil"),
                    new Claim(ClaimValueTypes.String, perfil.Nome, "Nome"),
                    new Claim(ClaimValueTypes.String, perfil.Email, "Email"),
                    new Claim(ClaimValueTypes.String, perfil.Login, "Login")
                };

                var minhaIdentidade = new ClaimsIdentity(userClaims, "Perfil");
                var userPrincipal = new ClaimsPrincipal(new[] { minhaIdentidade });

                await HttpContext.SignInAsync(userPrincipal);
                return RedirectToAction("Index", "Home");

            }
            ViewBag.Mensagem = "Credenciais invalidas!";

            return View(model);
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Cadastrar")]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost("Cadastrar")]
        public IActionResult Cadastrar([FromForm]PerfilViewModel model)
        {
            try
            {
                int retorno = 0;
                string sql = "INSERT INTO PERFIL (NOME, EMAIL, LOGIN, SENHA) VALUES ('{0}', '{1}', '{2}', '{3}')";

                if (null != model)
                {
                    sql = string.Format(sql, model.Nome, model.Email, model.Login, model.Senha);

                    using (var conn = _conexao.AbrirConexao())
                    {
                        retorno = conn.Execute(sql, model);
                    }
                }

                if (retorno == 1)
                    return RedirectToAction("Login", "Perfil");
                else
                    return RedirectToAction("Cadastrar", "Perfil");
            }
            catch
            {
                return RedirectToAction("Cadastrar", "Perfil");
            }
        }
    }
}