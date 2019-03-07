using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        //Criando uma dependecia para o SalesWebMVCContext (DbContext) - readonly previne que essa denpencia seja alterada
        private readonly SalesWebMVCContext _context;

        //Construtor para que a injeçao de dependencia possa ocorrer
        public SellerService(SalesWebMVCContext context)
        {
            _context = context;
        }

        //Operaçao para retornar todos os vendedores do db em uma lista
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }
    }
}
