using Cad_Ferramenta.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cad_Ferramenta.DAO
{
    public class FerramentaDAO
    {
        private FerramentaViewModel MontarFerramenta(DataRow row)
        {
            FerramentaViewModel ferramenta = new FerramentaViewModel
            {
                Id = Convert.ToInt32(row["Id"]),
                Descricao = row["DescricaoFerramenta"] != null ? row["DescricaoFerramenta"].ToString() : null,
                FabricanteId = row["FabricanteId"] != null ? Convert.ToInt32(row["FabricanteId"]) : 0,
                FabricanteDescricao = row["DescricaoFabricante"] != null ? row["DescricaoFabricante"].ToString() : null
            };

            return ferramenta;
        }

        private FabricanteViewModel MontarFabricante(DataRow row)
        {
            FabricanteViewModel fabricante = new FabricanteViewModel
            {
                Id = Convert.ToInt32(row["id"]),
                Descricao = row["descricao"].ToString()
            };

            return fabricante;
        }

        private SqlParameter[] CriaParametro(FerramentaViewModel ferramenta)
        {
            SqlParameter[] parametro = new SqlParameter[3];
            parametro[0] = new SqlParameter("id", ferramenta.Id);
            parametro[1] = new SqlParameter("descricao", ferramenta.Descricao);
            parametro[2] = new SqlParameter("codigoFabricante", ferramenta.FabricanteId);

            return parametro;
        }

        public List<FerramentaViewModel> ListagemFerramenta()
        {
            try
            {
                List<FerramentaViewModel> lista = new List<FerramentaViewModel>();
                DataTable tabela = HelperDAO.ExecutaProcedureSelect("spListagemFerramentas", null);

                foreach (DataRow row in tabela.Rows)
                {
                    lista.Add(MontarFerramenta(row));
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FabricanteViewModel> ListagemFabricante()
        {
            try
            {
                List<FabricanteViewModel> lista = new List<FabricanteViewModel>();
                DataTable tabela = HelperDAO.ExecutaProcedureSelect("spListagemFabricantes", null);

                foreach (DataRow row in tabela.Rows)
                {
                    lista.Add(MontarFabricante(row));
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FerramentaViewModel ConsultaFerramenta(int id)
        {
            var parametro = new SqlParameter[]
            {
                new SqlParameter("id", id)
            };

            DataTable tabela = HelperDAO.ExecutaProcedureSelect("spConsultaFerramenta", parametro);

            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return MontarFerramenta(tabela.Rows[0]);
            }
        }

        public void IncluiFerramenta(FerramentaViewModel ferramenta)
        {
            try
            {
                HelperDAO.ExecutaProcedure("spIncluiFerramenta", CriaParametro(ferramenta));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AlteraFerramenta(FerramentaViewModel ferramenta)
        {
            try
            {
                HelperDAO.ExecutaProcedure("spAlteraFerramenta", CriaParametro(ferramenta));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Excluir(int id)
        {
            try
            {
                var parametro = new SqlParameter[]
                {
                    new SqlParameter("id", id)
                };

                HelperDAO.ExecutaProcedure("spExcluiFerramenta", parametro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
