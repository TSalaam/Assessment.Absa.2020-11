using Microsoft.AspNetCore.Mvc.Filters;

namespace PhoneBook.Api.Infrastructure.Filters {

    /// <summary>
    /// Adds a header to the response
    /// </summary>
    public class AddHeader : IResultFilter {

        private readonly string _name;
        private readonly string _value;

        public AddHeader(string name, string value) {
            _name = name;
            _value = value;
        }

        public void OnResultExecuting(ResultExecutingContext context) {

            context.HttpContext.Response.Headers.Add(_name, new string[] { _value });
        }

        public void OnResultExecuted(ResultExecutedContext context) {
            // Can't add to headers here because response has already begun.
        }
    }
}
