using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using MySql.Data.MySqlClient;
using Quizzle.Models;
using Quizzle.Repositorio;

namespace Quizzle.Controllers
{
    public class CriarController : Controller
    {
        private readonly IConexao _conexao;

        public CriarController(IConexao conexao)
        {
            _conexao = conexao;
        }

        [Authorize]
        public IActionResult Index()
        {
            List<CategoriasViewModel> listCategorias;

            using (var conn = _conexao.AbrirConexao())
            {
                var querySQL = @"SELECT * FROM Categorias;";
                listCategorias = conn.Query<CategoriasViewModel>(querySQL).ToList();
            }

            ViewData["Categorias"] = listCategorias;

            return View("Criar", new QuizzesViewModel(10));
        }

        [HttpPost()]
        public IActionResult Criar([FromForm] QuizzesViewModel model)
        {
            try
            {
                PerfilViewModel perfil = new PerfilViewModel();

                if (null != HttpContext.User.Claims.FirstOrDefault(p => p.ValueType == "Id_Perfil").Value)
                {
                    perfil.Id_Perfil = int.Parse(HttpContext.User.Claims.FirstOrDefault(p => p.ValueType == "Id_Perfil").Value);
                }

                ListaQuizzesViewModel listaQuizzes;

                using (var conn = _conexao.AbrirConexao())
                {
                    var querySQLselect = $"SELECT Id_Listaquiz FROM Listaquizzes WHERE Id_Perfil = { perfil.Id_Perfil };";
                    listaQuizzes = conn.QueryFirstOrDefault<ListaQuizzesViewModel>(querySQLselect);
                }

                if (listaQuizzes == null)
                {
                    listaQuizzes = new ListaQuizzesViewModel();

                    var querySQLinsert = $"INSERT INTO Listaquizzes (Id_Perfil) VALUES ({ perfil.Id_Perfil });";
                    using (var conn = _conexao.AbrirConexao())
                    {
                        var reader = conn.ExecuteReader(querySQLinsert);
                        listaQuizzes.Id_ListaQuiz = (int)((MySqlCommand)((IWrappedDataReader)reader).Command).LastInsertedId;
                    }
                }

                QuizzesViewModel quizzes = model;

                try
                {
                    var path = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin"));
                    string uniqueFileName = null;

                    if (quizzes.ImagemFile != null)
                    {
                        string uploadsFolder = Path.Combine(path, "wwwroot\\images\\quiz\\");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + quizzes.ImagemFile.FileName;
                        quizzes.Imagem = "/images/quiz/" + uniqueFileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            quizzes.ImagemFile.CopyTo(fileStream);
                        }
                    }
                }
                catch (Exception e)
                {
                }

                using (var conn = _conexao.AbrirConexao())
                {
                    var querySQLinsert = "INSERT INTO QUIZZES (TITULO, IMAGEM, DESCRICAO, ID_LISTAQUIZ, ID_CATEGORIA)" +
                        "VALUES ('{0}', '{1}', '{2}', {3}, {4})";
                    querySQLinsert = string.Format(querySQLinsert, quizzes.Titulo, quizzes.Imagem ?? "", quizzes.Descricao, listaQuizzes.Id_ListaQuiz, quizzes.Categoria.Id_Categoria);

                    var reader = conn.ExecuteReader(querySQLinsert);
                    quizzes.Id_Quiz = (int)((MySqlCommand)((IWrappedDataReader)reader).Command).LastInsertedId;
                }

                foreach (var pergunta in quizzes.Perguntas)
                {
                    if (pergunta.Titulo != null)
                    {
                        using (var conn = _conexao.AbrirConexao())
                        {
                            var querySQLinsert = "INSERT INTO PERGUNTAS (TITULO, ID_QUIZ) VALUES ('{0}', {1})";
                            querySQLinsert = string.Format(querySQLinsert, pergunta.Titulo, quizzes.Id_Quiz);

                            var reader = conn.ExecuteReader(querySQLinsert);
                            pergunta.Id_Pergunta = (int)((MySqlCommand)((IWrappedDataReader)reader).Command).LastInsertedId;

                            reader.Close();

                            foreach (var alternativa in pergunta.Alternativas)
                            {
                                if (alternativa.Alternativa != null)
                                {
                                    querySQLinsert = "INSERT INTO ALTERNATIVAS (ID_PERGUNTA, ALTERNATIVA) VALUES ({0}, '{1}')";
                                    querySQLinsert = string.Format(querySQLinsert, pergunta.Id_Pergunta, alternativa.Alternativa);

                                    reader = conn.ExecuteReader(querySQLinsert);
                                    alternativa.Id_Alternativa = (int)((MySqlCommand)((IWrappedDataReader)reader).Command).LastInsertedId;

                                    reader.Close();

                                    if (alternativa == pergunta.Alternativas.First())
                                    {
                                        querySQLinsert = "INSERT INTO RESPOSTA (ID_PERGUNTA, ID_ALTERNATIVA) VALUES ({0}, {1})";
                                        querySQLinsert = string.Format(querySQLinsert, pergunta.Id_Pergunta, alternativa.Id_Alternativa);

                                        conn.Execute(querySQLinsert);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return RedirectToAction("Index", "Home");
        }
    }
}