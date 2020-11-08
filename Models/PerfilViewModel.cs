using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzle.Models
{
    public class PerfilViewModel
    {
        public int Id_Perfil { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public ListaQuizzesViewModel ListaQuizzes { get; set; }
    }
}
