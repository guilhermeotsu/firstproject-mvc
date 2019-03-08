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

        //Método para inserir um novo vendedor no db 
        public void Insert(Seller obj)
        { 
            //inserir o metodo no db (esta recebendo da view - create)
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {//vai retornar o vendedor que possui o id do parametro se n exitir retorna nulo
            return _context.Seller.FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
    }
}
