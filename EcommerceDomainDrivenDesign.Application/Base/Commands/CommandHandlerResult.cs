using FluentValidation.Results;
using System;

namespace EcommerceDomainDrivenDesign.Application.Base.Commands
{
    public class CommandHandlerResult
    {
        public ValidationResult ValidationResult { get; set; }
        public Guid Id { get; set; }
    }
}
