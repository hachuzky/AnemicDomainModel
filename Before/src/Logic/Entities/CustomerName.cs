using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Entities
{
    public class CustomerName : ValueObject<CustomerName>
    {
        public string Value { get; }
        private CustomerName(string value)
        {
            Value = value;
        }

        protected override bool EqualsCore(CustomerName other)
        {
           return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static Result<CustomerName> Create(string customerName)
        {
            customerName = (customerName ?? string.Empty).Trim();

            if (customerName.Length == 0)
                return Result.Failure<CustomerName>("Customer name should not be empty");

            if (customerName.Length > 100)
                return Result.Failure<CustomerName>("Customer name is too long");

            return Result.Ok(new CustomerName(customerName));
        }

        public static implicit operator string(CustomerName customerName)
        {
            return customerName.Value;
        }

        public static explicit operator CustomerName(string customerName)
        {
            return Create(customerName).Value;
        }
    }
}
