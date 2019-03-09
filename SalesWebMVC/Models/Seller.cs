using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [Display(Name="Base Salary")]
        public double BaseSalary { get; set; }

        [Display(Name="Birth Date")]
        public DateTime BirthDate { get; set; }

        //Fazendo a associaçao do Seller com o Department (cada vendedor possuir um departamento)
        public Department Department { get; set; }

        //Esse atributo faz a integridade referencial dos cadastros de Sellers (Chave estrangeira nao pode ser nulao int é um strunct e nao pode ser nulo)
        [Display(Name="Departmanet")]
        public int DepartmentId { get; set; }

        //Fazendo a associaçao do Seller com o SalesRecord (cada vendedor tem varios registros de venda)
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        //Para o fazer o TotalSales vamos usar o LINQ
        public double TotalSales(DateTime inicial, DateTime final)
        {
            //1- Filtrar a coleçao Sales para obter apenas as vendas no intervalo da datas do parametro 
            return Sales.Where(sr => sr.Date >= inicial && sr.Date <= final)
                //Fazer o calcular bom base no que foi filtrado
                .Sum(sr => sr.Amount);
        }
    }
}
