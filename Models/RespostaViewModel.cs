using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzle.Models
{
    public class RespostaViewModel
    {
        public int Id_Resposta { get; set; }
        public AlternativasViewModel Alternativa { get; set; }
    }
}
