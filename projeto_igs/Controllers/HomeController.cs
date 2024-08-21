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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }





        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EmAlta()
        {
            return View();
        }

        public IActionResult Vitrine() // RETORNA OS PRODUTOS NA VIEW VITRINE SEM AS FUNÇÕES DE ADMIN //
        {
            ProdutoRepository pR = new ProdutoRepository();
            ViewBag.NaoEncontrado = "Nenhum produto encontrado.";
            return View(pR.Listar());
        }



        [HttpGet]
        public IActionResult Buscar(string Busca)
        {
            BuscaRepository bR = new BuscaRepository();
            var resultado = bR.Pesquisar(Busca);
            if (resultado == null)
            {
                ViewBag.NaoEncontrado = "Nenhum produto encontrado.";
                return View("VitrineC");
            }
            else
            {
                return View("VitrineB", bR.Pesquisar(Busca));
            }
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
