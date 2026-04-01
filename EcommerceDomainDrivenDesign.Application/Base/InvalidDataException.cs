using System;

namespace EcommerceDomainDrivenDesign.Application.Base
{
    public class InvalidDataException : Exception
    {
        public InvalidDataException(string message) : base(message) { }
    }
}
