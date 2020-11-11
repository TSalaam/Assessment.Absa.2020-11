using System;

using FluentValidation;

using PhoneBook.Api.Domain.Models;
using PhoneBook.Api.Domain.Models.Request;

namespace PhoneBook.Api.Validators {

    public class EntryRequestValidator : AbstractValidator<EntryRequest> {

        public EntryRequestValidator() {

            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);

            RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(10);
        }
    }
}