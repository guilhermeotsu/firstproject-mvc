﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        //O Index vai ter que chamar a operaçao FinAll do SellerService, para isso sera necessario criar uma dependecia do SellerService
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            //Chamou o Controller, que por sua vez acessou o Model, pegou o dado na lista e encaminhou esses dados para a View
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        //Criando o método POST da açao Create (Inserir o Seller criado no Db)
        [HttpPost]
        //Previnindo ataque CSRF (qndo alguem aproveita a autenticaçao para injetar dados malicioso)
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        //Ação para confirmação do Delete, o "?" indica que o parametro é opcional
        public IActionResult Delete (int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            //Implementou o Value pois o valor do parametro é opcional
            var obj = _sellerService.FindById(id.Value);
            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //Uma ação POST para realmente deletar o Seller
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        //Criando uma ação details
        public IActionResult Details (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Implementou o Value pois o valor do parametro é opcional
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
    }
}