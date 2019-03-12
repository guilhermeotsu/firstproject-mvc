using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;

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

        public async Task<IActionResult> Index()
        {
            //Chamou o Controller, que por sua vez acessou o Model, pegou o dado na lista e encaminhou esses dados para a View
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        //Criando o método POST da açao Create (Inserir o Seller criado no Db)
        [HttpPost]
        //Previnindo ataque CSRF (qndo alguem aproveita a autenticaçao para injetar dados malicioso)
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            //Fazendo a validaçao dos campos no backend (nao no js)
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        //Ação para confirmação do Delete, o "?" indica que o parametro é opcional
        public async Task<IActionResult> Delete (int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            //Implementou o Value pois o valor do parametro é opcional
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        //Uma ação POST para realmente deletar o Seller
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        //Criando uma ação details
        public async Task<IActionResult> Details (int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            //Implementou o Value pois o valor do parametro é opcional
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            //Testando se o Id realmente existe no Db
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            //Teste de validaçao dos campos
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }


            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id miss match" });
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                //Macete para pegar o Id inteiro da requisiçao
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}