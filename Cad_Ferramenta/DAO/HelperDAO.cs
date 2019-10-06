using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cad_Ferramenta.DAO
{
    public static class HelperDAO
    {
        public static void ExecutaProcedure(string nomeProcedure, SqlParameter[] parametro)
        {
            using (SqlConnection conexao = Conexao.GetConexao())
            {
                using (SqlCommand comando = new SqlCommand(nomeProcedure, conexao))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    if (parametro != null)
                    {
                        comando.Parameters.AddRange(parametro);
                    }

                    comando.ExecuteNonQuery();
                }

                conexao.Close();
            }
        }

        public static DataTable ExecutaProcedureSelect(string nomeProcedure, SqlParameter[] parametro)
        {
            using (SqlConnection conexao = Conexao.GetConexao())
            {
                using (SqlDataAdapter adapter= new SqlDataAdapter(nomeProcedure, conexao))
                {
                    if (parametro != null)
                    {
                        adapter.SelectCommand.Parameters.AddRange(parametro);
                    }

                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);

                    conexao.Close();

                    return tabela;
                }
            }
        }
    }
}
