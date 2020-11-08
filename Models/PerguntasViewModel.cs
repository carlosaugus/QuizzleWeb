using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Quizzle.Models
{
    public class PerguntasViewModel
    {
        public PerguntasViewModel()
        {

        }

        public PerguntasViewModel(int qtdAlternativasMax)
        {
            Alternativas = new List<AlternativasViewModel>();
            for (int i = 0; i < qtdAlternativasMax; i++)
                Alternativas.Add(new AlternativasViewModel());
        }

        public int Id_Pergunta { get; set; }
        public string Titulo { get; set; }
        public QuizzesViewModel Quiz { get; set; }
        public List<AlternativasViewModel> Alternativas { get; set; }
        public RespostaViewModel Resposta { get; set; }
    }
}
