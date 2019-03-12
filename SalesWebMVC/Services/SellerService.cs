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
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        //Método para inserir um novo vendedor no db 
        public async Task InsertAsync(Seller obj)
        { 
            //inserir o metodo no db (esta recebendo da view - create)
            _context.Add(obj);
            //Async vai ficar no save pois ele que acessa o db
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {//vai retornar o vendedor que possui o id do parametro se n exitir retorna nulo o Include é um Eager Loading
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }catch (DbUpdateException e)
            {
                throw new IntegrityException("can't delete seller because he/she has sales");
            }
        }

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            //Any serve para verificar se existe algum com a condiçao entre ()
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            //Qndo se chama a operaçao de atualizar Db ele pode retornar uma Exceçao de Conflito de Ocorrencia
            //Se isso ocorrer o Entity vai gerar uma exceçao de DbUpdateConcurrentyException
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
