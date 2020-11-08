using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzle.Repositorio
{
    public interface IConexao
    {
        IDbConnection AbrirConexao();
    }
}
