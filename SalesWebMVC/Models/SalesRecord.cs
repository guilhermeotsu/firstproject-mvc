using SalesWebMVC.Models.Enums;
using System;

namespace SalesWebMVC.Models
{
    // Registro de venda
    public class SalesRecord
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public SalesStatus Status { get; set; }
        //Fazendo a associaçao do SalesRecord com Seller (os registros de venda tem apenas um vendedor)
        public Seller Seller { get; set; }

        public SalesRecord() { }

        public SalesRecord(int id, DateTime date, double amount, SalesStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }
    }
}
