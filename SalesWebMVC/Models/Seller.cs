﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public double BaseSalary { get; set; }
        public DateTime BirthDate { get; set; }
        //Fazendo a associaçao do Seller com o Department (cada vendedor possuir um departamento)
        public Department Department { get; set; }
        //Fazendo a associaçao do Seller com o SalesRecord (cada vendedor tem varios registros de venda)
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, double baseSalary, DateTime birthDate, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BaseSalary = baseSalary;
            BirthDate = birthDate;
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
