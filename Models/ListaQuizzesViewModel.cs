using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzle.Models
{
    public class ListaQuizzesViewModel
    {
        public int Id_ListaQuiz { get; set; }
        public List<QuizzesViewModel> Quizzes { get; set; }
    }
}
