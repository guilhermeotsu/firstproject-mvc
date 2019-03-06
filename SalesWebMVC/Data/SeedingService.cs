using SalesWebMVC.Models;
using SalesWebMVC.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Data
{
    public class SeedingService
    {
        //Dependecia do Seedingservice com o DbContext
        private SalesWebMVCContext _context;

        //Quando o SeedingService for criado, automaticamente ele vai receber uma instancia do context tambem
        public SeedingService(SalesWebMVCContext context)
        {
            _context = context;
        }

        //Operaçao responsavel por popular a Base. Esse metodo vai ser chamado na classe Startup dentro do método Configure
        public void Seed()
        {
            //Any teste se existe algum registro na Tabela
            if(_context.Department.Any() || _context.Seller.Any() || _context.SalesRecord.Any())
            {
                return; //BD ja foi populado
            }
            //Instanciaçao dos objetos e mandar salvar no bd
            Department d1 = new Department(1, "Computers");
            Department d2 = new Department(2, "Eletronics");
            Department d3 = new Department(3, "Fashion");
            Department d4 = new Department(4, "Books");

            //Registrando os vendedores
            Seller s1 = new Seller(1, "joao", "joao@outlook.com", new DateTime(1998, 4, 21), 1000.0, d1);
            Seller s2 = new Seller(2, "enzo", "enzo@outlook.com", new DateTime(1979, 1, 12), 3500.0, d2);
            Seller s3 = new Seller(3, "valentina", "valentina@outlook.com", new DateTime(1972, 12, 17), 2200.0, d3);
            Seller s4 = new Seller(4, "nicole", "nicole@outlook.com", new DateTime(1991, 8, 19), 3000.0, d4);
            Seller s5 = new Seller(5, "pedro", "pedro@outlook.com", new DateTime(2001, 5, 20), 4000.0, d3);
            Seller s6 = new Seller(6, "maria", "maria@outlook.com", new DateTime(1987, 10, 2), 3000.0, d2);

            //Criando um registro de venda dos vendedores
            SalesRecord r1 = new SalesRecord(1, new DateTime(2018, 12, 25), 1000.0, SalesStatus.Billed, s1);
            SalesRecord r2 = new SalesRecord(2, new DateTime(2018, 12, 25), 2200.0, SalesStatus.Billed, s2);
            SalesRecord r3 = new SalesRecord(3, new DateTime(2017, 08, 11), 980.0, SalesStatus.Billed, s3);
            SalesRecord r4 = new SalesRecord(4, new DateTime(2018, 10, 21), 2500.0, SalesStatus.Billed, s4);
            SalesRecord r5 = new SalesRecord(5, new DateTime(2018, 06, 3), 5300.0, SalesStatus.Billed, s5);
            SalesRecord r6 = new SalesRecord(6, new DateTime(2019, 07, 19), 1350.0, SalesStatus.Billed, s6);

            //Transferindo para Db

            //Adicionando os departamento(AddRanger permite transferir varios de uma só vez)
            _context.Department.AddRange(d1, d2, d3, d4);

            _context.Seller.AddRange(s1, s2, s3, s4, s5, s6);

            _context.SalesRecord.AddRange(r1, r2, r3, r4, r5, r6);

            //Salvar e confirmar as alteraçoes no Db
            _context.SaveChanges();
        }
    }
}
