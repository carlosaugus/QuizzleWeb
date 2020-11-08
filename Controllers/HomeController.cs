using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quizzle.Models;
using Quizzle.Repositorio;

namespace Quizzle.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly IConexao _conexao;
        private List<CategoriasViewModel> listCategorias;
        private List<QuizzesViewModel> listQuizzes;

        /*public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        public HomeController(IConexao conexao)
        {
            _conexao = conexao;
        }

        public IActionResult Index()
        {
            listQuizzes = new List<QuizzesViewModel>();
            using (var conn = _conexao.AbrirConexao())
            {
                var querySQL = @"SELECT * FROM Categorias;";
                listCategorias = conn.Query<CategoriasViewModel>(querySQL).ToList();

                querySQL = @"SELECT ID_QUIZ, TITULO, DESCRICAO, IMAGEM FROM QUIZZES ORDER BY DATACADASTRO DESC;";
                listQuizzes = conn.Query<QuizzesViewModel>(querySQL).Take(30).ToList();
            }

            Random rnd = new Random();
            ViewData["Categorias"] = listCategorias.OrderBy(p => rnd.Next()).Take(5).ToList();

            return View(listQuizzes);
        }

        [HttpGet("Categoria/{id}")]
        public IActionResult Index(int id)
        {
            listQuizzes = new List<QuizzesViewModel>();
            using (var conn = _conexao.AbrirConexao())
            {
                var querySQL = @"SELECT * FROM Categorias;";
                listCategorias = conn.Query<CategoriasViewModel>(querySQL).ToList();

                querySQL = $"SELECT ID_QUIZ, TITULO, DESCRICAO, IMAGEM FROM QUIZZES WHERE ID_CATEGORIA = { id } ORDER BY DATACADASTRO DESC;";
                listQuizzes = conn.Query<QuizzesViewModel>(querySQL).Take(30).ToList();
            }

            Random rnd = new Random();
            ViewData["Categorias"] = listCategorias.OrderBy(p => rnd.Next()).Take(5).ToList();

            return View(listQuizzes);
        }

        [HttpGet("Perfil/{id}")]
        public IActionResult Perfil(int id)
        {
            listQuizzes = new List<QuizzesViewModel>();
            using (var conn = _conexao.AbrirConexao())
            {
                var querySQL = @"SELECT * FROM Categorias;";
                listCategorias = conn.Query<CategoriasViewModel>(querySQL).ToList();

                querySQL = $"SELECT ID_QUIZ, TITULO, DESCRICAO, IMAGEM FROM QUIZZES AS Q INNER JOIN LISTAQUIZZES AS L ON L.ID_LISTAQUIZ = Q.ID_LISTAQUIZ WHERE L.ID_PERFIL = { id } ORDER BY DATACADASTRO DESC;";
                listQuizzes = conn.Query<QuizzesViewModel>(querySQL).Take(30).ToList();
            }

            Random rnd = new Random();
            ViewData["Categorias"] = listCategorias.OrderBy(p => rnd.Next()).Take(5).ToList();

            return View(listQuizzes);
        }

        [HttpGet("Pesquisar/{texto}")]
        public IActionResult Index(string texto)
        {
            listQuizzes = new List<QuizzesViewModel>();
            using (var conn = _conexao.AbrirConexao())
            {
                var querySQL = @"SELECT * FROM Categorias;";
                listCategorias = conn.Query<CategoriasViewModel>(querySQL).ToList();

                querySQL = $"SELECT ID_QUIZ, TITULO, DESCRICAO, IMAGEM FROM QUIZZES WHERE TITULO LIKE '%{ texto }%' OR DESCRICAO LIKE '%{ texto }%' ORDER BY DATACADASTRO DESC;";
                listQuizzes = conn.Query<QuizzesViewModel>(querySQL).Take(30).ToList();
            }

            Random rnd = new Random();
            ViewData["Categorias"] = listCategorias.OrderBy(p => rnd.Next()).Take(5).ToList();

            return View(listQuizzes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
