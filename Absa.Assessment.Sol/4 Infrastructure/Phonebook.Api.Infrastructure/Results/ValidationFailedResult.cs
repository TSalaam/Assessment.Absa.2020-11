using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using FluentValidation.Results;

using PhoneBook.Api.Infrastructure.Models;

using DataAnnotations = System.ComponentModel.DataAnnotations;

namespace PhoneBook.Api.Infrastructure.Results {

    public class ValidationFailedResult : ObjectResult {

        public ValidationFailedResult(List<DataAnnotations.ValidationResult> validationResults, string source, string status = "UNPROCESSABLEENTITY")
            : base(new ValidationResultModel(validationResults, StatusCodes.Status422UnprocessableEntity, status, source)) {

            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }

        public ValidationFailedResult(ValidationResult validationResult, string source, string status = "UNPROCESSABLEENTITY")
            : base(new ValidationResultModel(validationResult, StatusCodes.Status422UnprocessableEntity, status, source)) {

            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}
