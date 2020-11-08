using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Quizzle.Models;
using Quizzle.Repositorio;

namespace Quizzle.Controllers
{
    public class menuCategoriasController : ViewComponent
    {
        private readonly IConexao _conexao;

        public menuCategoriasController(IConexao conexao)
        {
            _conexao = conexao;
        }

        public IViewComponentResult Invoke()
        {
            List<CategoriasViewModel> listCategorias;

            using (var conn = _conexao.AbrirConexao())
            {
                string sql = "SELECT * FROM CATEGORIAS ORDER BY DESCRICAO ASC";
                listCategorias = conn.Query<CategoriasViewModel>(sql).ToList();
            }

            return View("../menuCategorias", listCategorias);
        }
    }
}