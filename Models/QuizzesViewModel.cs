using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzle.Models
{
    public class QuizzesViewModel
    {
        public QuizzesViewModel()
        {

        }

        public QuizzesViewModel(int qtdPerguntasMax)
        {
            Perguntas = new List<PerguntasViewModel>();
            for (int i = 0; i < qtdPerguntasMax; i++)
                Perguntas.Add(new PerguntasViewModel(5));
            Categoria = new CategoriasViewModel();
        }

        public int Id_Quiz { get; set; }
        public string Titulo { get; set; }
        public string Imagem { get; set; }
        public IFormFile ImagemFile { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public CategoriasViewModel Categoria { get; set; }
        public List<PerguntasViewModel> Perguntas { get; set; }
        public int Passo { get; set; }
        public int AltMarcada { get; set; }
        public int Pontuacao { get; set; }
    }
}
