using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cad_Ferramenta.Models;
using Cad_Ferramenta.DAO;

namespace Cad_Ferramenta.Controllers
{
    public class HomeController : Controller
    {
        private void ValidaDados(FerramentaViewModel ferramenta, string operacao)
        {
            FerramentaDAO dao = new FerramentaDAO();

            if (operacao == "A" && dao.ConsultaFerramenta(ferramenta.Id) == null)
            {
                ModelState.AddModelError("Id", "Ferramenta não existe");
            }

            if (string.IsNullOrEmpty(ferramenta.Descricao))
            {
                ModelState.AddModelError("Descricao", "Preencha o campo referente a descrição da ferramenta");
            }
        }

        public IActionResult Index()
        {
            FerramentaDAO dao = new FerramentaDAO();
            List<FerramentaViewModel> listaFerramenta = dao.ListagemFerramenta();

            if (listaFerramenta.Count > 0)
            {
                ViewBag.ErroConsulta = null;
            }
            else
            {
                ViewBag.ErroConsulta = "Nenhuma ferramenta cadastrada";
            }

            return View(listaFerramenta);
        }

        public IActionResult Incluir()
        {
            ViewBag.Operacao = "I";

            FerramentaDAO dao = new FerramentaDAO();
            List<FabricanteViewModel> listaFabricante = dao.ListagemFabricante();

            ViewBag.ListaFabricantes = listaFabricante;

            return View();
        }

        public IActionResult SalvarFerramenta(FerramentaViewModel ferramenta, string operacao)
        {
            try
            {
                ViewBag.ErroIncluir = null;
                ValidaDados(ferramenta, operacao);

                if (ModelState.IsValid == false)
                {
                    FerramentaDAO dao = new FerramentaDAO();
                    List<FabricanteViewModel> listaFabricante = dao.ListagemFabricante();

                    ViewBag.ListaFabricantes = listaFabricante;

                    return View("Incluir", ferramenta);
                }
                else
                {
                    FerramentaDAO dao = new FerramentaDAO();

                    if (operacao == "I")
                    {
                        dao.IncluiFerramenta(ferramenta);
                    }
                    else
                    {
                        dao.AlteraFerramenta(ferramenta);
                    }

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErroIncluir = "Erro ao incluir os dados da ferramenta. Erro: " + ex.Message;

                FerramentaDAO dao = new FerramentaDAO();
                List<FabricanteViewModel> listaFabricante = dao.ListagemFabricante();

                ViewBag.ListaFabricantes = listaFabricante;

                return View("Incluir", ferramenta);
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                FerramentaDAO dao = new FerramentaDAO();
                dao.Excluir(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErroConsulta = "Não foi possível excluir os dados da ferramenta. Erro: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Operacao = "A";

                FerramentaDAO dao = new FerramentaDAO();
                FerramentaViewModel ferramenta = dao.ConsultaFerramenta(id);

                if (ferramenta == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    List<FabricanteViewModel> listaFabricante = dao.ListagemFabricante();

                    ViewBag.ListaFabricantes = listaFabricante;

                    return View("Incluir", ferramenta);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErroConsulta = "Não foi possível alterar os dados da ferramenta. Erro: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
