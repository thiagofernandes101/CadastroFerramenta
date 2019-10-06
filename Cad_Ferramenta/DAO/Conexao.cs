using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cad_Ferramenta.DAO
{
    public static class Conexao
    {
        public static SqlConnection GetConexao()
        {
            string strConexao = "Data Source = LAPTOP-P8C39P0A\\SQLEXPRESSEX; Initial Catalog = ExercicioDB; user id = sa; password = 123456";
            SqlConnection conexao = new SqlConnection(strConexao);

            conexao.Open();

            return conexao;
        }
    }
}
