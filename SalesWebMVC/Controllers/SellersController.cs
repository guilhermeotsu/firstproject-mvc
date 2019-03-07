using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        //O Index vai ter que chamar a operaçao FinAll do SellerService, para isso sera necessario criar uma dependecia do SellerService
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            //Chamou o Controller, que por sua vez acessou o Model, pegou o dado na lista e encaminhou esses dados para a View
            var list = _sellerService.FindAll();
            return View(list);
        }
    }
}