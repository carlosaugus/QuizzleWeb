using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzle.Repositorio
{
    public class Conexao : IConexao
    {
        public IDbConnection AbrirConexao()
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost; Port=3306; Database=Quizzle; Uid=sa; Pwd='P@ssw0rd';"))
            {
                conn.Open();
                return conn;
            }
        }
    }
}
