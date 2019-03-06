
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //Fazendo a associaçao entre Department e Seller (ICollection mais generico), instanciando a coleçao para garantir que a lista vai ser instanciada 
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department() { }

        //Nao implementa no construtor os atributos que sao coleçoes
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        //Adicionar um vendedor
        public void AddSeller(Seller seller)
        {
            //Pega a Coleçao de vendedor a adicionar o que veio nos parametros
            Sellers.Add(seller);
        }


        //Calcular o total de vendas do departamento
        public double TotalSales(DateTime initial, DateTime final)
        {
            // Pegando cada vendedor da lista e chamando o TotalSales do vendedor e somando
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}
