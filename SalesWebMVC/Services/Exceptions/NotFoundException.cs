using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message) : base(message) { }

        //Ao criar uma exceçao personalizada tem a possibilidade de tratar exclusivamente essa exceçao
    }
}
