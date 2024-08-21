using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projeto_etapa4.Models;
using Microsoft.AspNetCore.Http;

namespace projeto_etapa4.Controllers
{
    public class ContatoController : Controller
    {
        private readonly ILogger<ContatoController> _logger;

        public ContatoController(ILogger<ContatoController> logger)
        {
            _logger = logger;
        }





        public IActionResult FaleConosco()
        {
            if (HttpContext.Session.GetInt32("idCadastro") == null)
            {
                return RedirectToAction ("Login", "Usuario");
            }
            else
            {
                return View();
            }
        }

        public IActionResult FaleConosco2()
        {
            if (HttpContext.Session.GetInt32("idCadastro") == null)
            {
                return RedirectToAction ("Login", "Usuario");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Solicitacoes() // RETORNA A RESPECTIVA VIEW LISTANDO TODAS AS SOLICITACOES CADASTRADAS //
        {
            if (HttpContext.Session.GetInt32("idCadastro") == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                if (HttpContext.Session.GetString("Tipo") == "Admin")
                {
                    SolicitacaoRepository sR = new SolicitacaoRepository();
                    return View(sR.Listar());
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public IActionResult Exclusao(int idSolicitacao) // EXCLUI UMA SOLICITACAO A PARTIR DE SEU ID //
        {
            if (HttpContext.Session.GetInt32("idCadastro") == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                if (HttpContext.Session.GetString("Tipo") == "Admin")
                {
                    SolicitacaoRepository sR = new SolicitacaoRepository();
                    sR.Excluir(idSolicitacao);
                    return RedirectToAction("Solicitacoes");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }                
            }
        }



        [HttpPost]
        public IActionResult CadastrarSol(Solicitacao s) // ADICIONA A SOLICITAÇÃO PREENCHIDA NO FORMULARIO DO SITE NO BANCO DE DADOS //
        {
            if (HttpContext.Session.GetInt32("idCadastro") == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                try 
                {
                    s.idCadastro = (int)HttpContext.Session.GetInt32("idCadastro");

                    SolicitacaoRepository sR = new SolicitacaoRepository();
                    sR.Cadastrar(s);
                    return Json(new{status="OK"});
                }
                catch (Exception)
                {
                    return Json(new{status="ERR", mensagem="Houve um problema ao realizar a sua solicitação, por favor, tente mais tarde."});
                }
                
            }
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}