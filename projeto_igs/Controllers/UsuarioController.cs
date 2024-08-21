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
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }





        public IActionResult CadastroUser() // CADASTRA UM USUÁRIO QUALQUER //
        {
            return View();
        }

        public IActionResult CadastroAdmin() // CADASTRA UM ADMINISTRADOR - SOMENTE ACESSADA PELA VIEW "ADMIN" QUANDO JÁ LOGADO COMO ADMIN //
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

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Admin() // ACESSADA SOMENTE SE ESTIVER LOGADO COMO ADMIN //
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

        public IActionResult Logout() // FAZ O LOGOUT DO USUARIO //
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Usuarios() // LISTA TODOS OS CADASTROS NA PÁGINA //
        {
            if (HttpContext.Session.GetInt32("idCadastro") == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                if (HttpContext.Session.GetString("Tipo") == "Admin")
                {
                    ViewBag.virgula = ", ";
                    ViewBag.hifen = " - ";
                    CadastroRepository cR = new CadastroRepository();
                    return View(cR.Listar());
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public IActionResult UsuariosE1() // MENSAGEM DE ERRO AO TENTAR EXCLUIR O PRÓPRIO CADASTRO //
        {
            if (HttpContext.Session.GetInt32("idCadastro") == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                if (HttpContext.Session.GetString("Tipo") == "Admin")
                {
                    ViewBag.DeleteFail = "* Você não pode excluir seu próprio cadastro. Para tal, faça login com outra conta admin.";
                    ViewBag.virgula = ", ";
                    ViewBag.hifen = " - ";
                    CadastroRepository cR = new CadastroRepository();
                    return View("Usuarios", cR.Listar());
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public IActionResult UsuariosE2() // MENSAGEM DE ERRO AO TENTAR EXCLUIR CADASTRO COM PENDÊNCIAS //
        {
            if (HttpContext.Session.GetInt32("idCadastro") == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                if (HttpContext.Session.GetString("Tipo") == "Admin")
                {
                    ViewBag.DeleteFail_1 = "* O cadastro não pôde ser excluído porque o ID do cadastro em questão possui uma ou mais solicitações pendentes";
                    ViewBag.virgula = ", ";
                    ViewBag.hifen = " - ";
                    CadastroRepository cR = new CadastroRepository();
                    return View("Usuarios", cR.Listar());
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public IActionResult ConfirmarSenha()
        {
            if (HttpContext.Session.GetInt32("idCadastro") == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                return View();
            }
        }

        public IActionResult MinhaConta() // EXIBE OS DADOS COM BASE NO idCadastro DO USUARIO LOGADO //
        {
            if (HttpContext.Session.GetInt32("idCadastro") == null)
            {
                return RedirectToAction ("Login", "Usuario");
            }
            else
            {
                Cadastro c = new Cadastro();
                c.idCadastro = (int)HttpContext.Session.GetInt32("idCadastro");
                CadastroRepository cR = new CadastroRepository();
                return View(cR.ListarAtual(c.idCadastro));
            }
        }

        public IActionResult Edicao(int idCadastro) // RETORNA NA VIEW OS DADOS DO CADASTRO REFERENTE AO ID RECEBIDO //
        {
            if (HttpContext.Session.GetInt32("idCadastro") == null)
            {
                return RedirectToAction ("Login", "Usuario");
            }
            else
            {
                if (HttpContext.Session.GetString("Tipo") == "Admin")
                {
                    CadastroRepository cR = new CadastroRepository();
                    return View("EdicaoAdmin", cR.BuscarID(idCadastro));
                }
                else
                {
                    CadastroRepository cR = new CadastroRepository();
                    return View(cR.BuscarID(idCadastro));
                }
            }
        }

        public IActionResult Exclusao(int idCadastro) // EXCLUI O CADASTRO DO SISTEMA //
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

                    if (c.idCadastro == idCadastro)
                    {
                        ViewBag.DeleteFail = "* Você não pode excluir seu próprio cadastro. Para tal, faça login com outra conta admin.";
                        return RedirectToAction("UsuariosE1");
                    }
                    else
                    {
                        CadastroRepository cR = new CadastroRepository();
                        string verifyID = cR.Del_Solicit_ID(idCadastro);
                        
                        if (verifyID == "y")
                        {
                            cR.Excluir(idCadastro);
                            return RedirectToAction("Usuarios");
                        }
                        else
                        {
                            ViewBag.DeleteFail_1 = "* O cadastro não pôde ser excluído porque o ID do cadastro em questão possui uma ou mais solicitações pendentes";
                            return RedirectToAction("UsuariosE2");
                        }
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }



        [HttpPost]
        public IActionResult Login(Cadastro c) // ACTION COM SCRIPTS DE LOGIN //
        {
            LoginRepository cR = new LoginRepository();
            Cadastro cadLog = cR.Login(c);

            if (cadLog == null) // CONDIÇÃO CRIADA PARA VALIDAR SE O USUÁRIO INFORMADO NO FORMULÁRIO REALMENTE EXISTE NO BANCO DE DADOS //
            {
                ViewBag.MensagemFail = "Email e/ou senha incorreto. Favor, tente novamente.";
                return View();
            }
            else
            {
                HttpContext.Session.SetInt32("idCadastro", cadLog.idCadastro);
                HttpContext.Session.SetString("Nome", cadLog.Nome);
                HttpContext.Session.SetString("Tipo", cadLog.Tipo);
                HttpContext.Session.SetString("Senha", cadLog.Senha);

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult CadastrarU(Cadastro c) // CRIA UM CADASTRO DO TIPO USUARIO //
        {
            CadastroRepository cR = new CadastroRepository();
            string novoC = cR.ChecarEmail(c);

            if (novoC == null)
            {
                ViewBag.emailDup = "* Email já está cadastrado em outra conta.";
                return View("CadastroUser");
            }
            else
            {
                cR.CadastrarUsuario(c);
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public IActionResult CadastrarA(Cadastro c) // CRIA UM CADASTRO DO TIPO ADMIN - SÓ PODE SER ACESSADO SE JÁ ESTIVER LOGADO COMO ADMIN //
        {
            CadastroRepository cR = new CadastroRepository();
            string novoC = cR.ChecarEmail(c);

            if (HttpContext.Session.GetInt32("idCadastro") == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                if (HttpContext.Session.GetString("Tipo") == "Admin")
                {
                    if (novoC == null)
                    {
                        ViewBag.emailDup = "* Email já está cadastrado em outra conta.";
                        return View("CadastroAdmin");
                    }
                    else
                    {
                        cR.CadastrarAdmin(c);
                        return RedirectToAction("Usuarios");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpPost]
        public IActionResult ConfirmarSenha(Cadastro c)
        {
            if (c.Senha == (string)HttpContext.Session.GetString("Senha"))
            {
                return RedirectToAction("MinhaConta");
            }
            else
            {
                ViewBag.Mensagem = "Senha incorreta, tente novamente.";
                return View();
            } 
        }

        [HttpPost]
        public IActionResult Edicao(Cadastro c) // EDITA OS DADOS DO CADASTRO EM QUESTÃO //
        {
            if (c.Senha == HttpContext.Session.GetString("Senha"))
            {
                CadastroRepository cR = new CadastroRepository();
                cR.Editar(c);
                return RedirectToAction("MinhaConta");
            }
            else
            {
                CadastroRepository cR = new CadastroRepository();
                cR.Editar(c);
                return RedirectToAction("Logout");
            }
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}