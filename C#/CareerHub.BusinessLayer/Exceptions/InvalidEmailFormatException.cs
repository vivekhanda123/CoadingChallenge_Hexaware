using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.BusinessLayer.Exceptions
{
    internal class InvalidEmailFormatException : Exception
    {
        public InvalidEmailFormatException(string message) : base(message) { }
    }
}
