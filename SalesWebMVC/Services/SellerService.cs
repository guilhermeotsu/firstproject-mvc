using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;
using SalesWebMVC.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        //Criando uma dependecia para o SalesWebMVCContext (DbContext) - readonly previne que essa dependencia seja alterada
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
        {//vai retornar o vendedor que possui o id do parametro se n exitir retorna nulo o Include é um Eager Loading
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller obj)
        {
            //Any serve para verificar se existe algum com a condiçao entre ()
            if(_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }
            //Qndo se chama a operaçao de atualizar Db ele pode retornar uma Exceçao de Conflito de Ocorrencia
            //Se isso ocorrer o Entity vai gerar uma exceçao de DbUpdateConcurrentyException
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
