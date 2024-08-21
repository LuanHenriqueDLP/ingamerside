using System.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projeto_etapa4.Models;

namespace projeto_etapa4.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ILogger<ProdutoController> _logger;

        public ProdutoController(ILogger<ProdutoController> logger)
        {
            _logger = logger;
        }





        public IActionResult AddProduto()
        {
            if (HttpContext.Session.GetInt32("idCadastro") == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                if (HttpContext.Session.GetString("Tipo") == "Admin")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }   
        }        
        
        public IActionResult Produtos() // RETORNA A RESPECTIVA VIEW LISTANDO OS PRODUTOS CADASTRADOS COM AS FUNÇÕES DE ADMIN //
        {
            if (HttpContext.Session.GetInt32("idCadastro") == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                if (HttpContext.Session.GetString("Tipo") == "Admin")
                {
                    ProdutoRepository pR = new ProdutoRepository();
                    return View(pR.Listar());
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }  
        }

        public IActionResult BuscarID(int idProduto) // BUSCA O PRODUTO NO BANCO DE DADOS PELO ID RECEBIDO NOS PARAMETROS DA URL (CLICANDO NO LINK) //
        {
            if (HttpContext.Session.GetInt32("idCadastro") == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                if (HttpContext.Session.GetString("Tipo") == "Admin")
                {
                    Cadastro c = new Cadastro();
                    c.idCadastro = (int)HttpContext.Session.GetInt32("idCadastro");
                    ProdutoRepository pR = new ProdutoRepository();
                    return View("Edicao", pR.BuscarID(idProduto));
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public IActionResult Exclusao(int idProduto) // EXCLUI O PRODUTO EM QUESTÃO BASEADO NO SEU ID //
        {
            if (HttpContext.Session.GetInt32("idCadastro") == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                if (HttpContext.Session.GetString("Tipo") == "Admin")
                {
                    ProdutoRepository pR = new ProdutoRepository();
                    pR.Excluir(idProduto);
                    return RedirectToAction("Produtos");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }            
            }
        }




        [HttpPost]
        public IActionResult AddProduto(Produto p) // ADICIONA UM NOVO PRODUTO //
        {
            if (HttpContext.Session.GetInt32("idCadastro") == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                if (HttpContext.Session.GetString("Tipo") == "Admin")
                {
                    try
                    {
                        p.idCadastro = (int)HttpContext.Session.GetInt32("idCadastro");
                        
                        ProdutoRepository pR = new ProdutoRepository();
                        pR.Cadastrar(p);
                        return Json(new{status="OK"});
                    }
                    catch (Exception)
                    {
                        return Json(new{status="ERR", mensagem="Houve um problema ao realizar a sua solicitação, por favor, tente mais tarde."});
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpPost]
        public IActionResult Edicao(Produto p) // ATUALIZA AS INFORMAÇÕES DE UM PRODUTO JÁ CADASTRADO //
        {
            if (HttpContext.Session.GetInt32("idCadastro") == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                if (HttpContext.Session.GetString("Tipo") == "Admin")
                {
                    Cadastro c = new Cadastro();
                    c.idCadastro = (int)HttpContext.Session.GetInt32("idCadastro");
                    p.idCadastro = (int)HttpContext.Session.GetInt32("idCadastro");
                    ProdutoRepository pR = new ProdutoRepository();
                    pR.Editar(p);
                    return RedirectToAction("Produtos");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
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