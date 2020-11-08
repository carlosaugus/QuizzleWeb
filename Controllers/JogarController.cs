using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quizzle.Models;
using Quizzle.Repositorio;

namespace Quizzle.Controllers
{
    public class JogarController : Controller
    {
        private readonly IConexao _conexao;

        public JogarController(IConexao conexao)
        {
            _conexao = conexao;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Jogar/{id}")]
        public IActionResult Index(int id)
        {
            QuizzesViewModel quizzes = new QuizzesViewModel();
            quizzes.Passo = 0;
            quizzes.Pontuacao = 0;

            using (var conn = _conexao.AbrirConexao())
            {
                var querySQL = $"SELECT * FROM QUIZZES WHERE ID_QUIZ = { id };";
                quizzes = conn.QueryFirstOrDefault<QuizzesViewModel>(querySQL);

                quizzes.Perguntas = new List<PerguntasViewModel>();

                querySQL = $"SELECT * FROM PERGUNTAS WHERE ID_QUIZ = { id };";
                quizzes.Perguntas = conn.Query<PerguntasViewModel>(querySQL).ToList();

                for (int i = 0; i < quizzes.Perguntas.Count; i++)
                {
                    querySQL = $"SELECT * FROM ALTERNATIVAS WHERE ID_PERGUNTA = { quizzes.Perguntas[i].Id_Pergunta };";
                    quizzes.Perguntas[i].Alternativas = conn.Query<AlternativasViewModel>(querySQL).ToList();

                    querySQL = $"SELECT * FROM RESPOSTA WHERE ID_PERGUNTA = { quizzes.Perguntas[i].Id_Pergunta };";
                    quizzes.Perguntas[i].Resposta = conn.QueryFirstOrDefault<RespostaViewModel>(querySQL);
                }
            }

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddHours(2);
            string jsonString = JsonSerializer.Serialize(quizzes);

            Response.Cookies.Append("quizz", jsonString, option);

            return View(quizzes);
        }

        [HttpGet("Avancar/{idPergunta}/{idResposta}")]
        public IActionResult Avancar(int idPergunta, int idResposta)
        {
            QuizzesViewModel quizzes = new QuizzesViewModel();

            string cookieValueFromReq = Request.Cookies["quizz"];
            quizzes = JsonSerializer.Deserialize<QuizzesViewModel>(cookieValueFromReq);

            if (quizzes.Perguntas.FirstOrDefault(p => p.Id_Pergunta == idPergunta).Resposta.Id_Resposta == idResposta)
            {
                quizzes.Pontuacao += 1;
            }

            quizzes.Passo += 1;

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddHours(2);
            string jsonString = JsonSerializer.Serialize(quizzes);

            Response.Cookies.Append("quizz", jsonString, option);

            return View("Index", quizzes);
        }
    }
}
