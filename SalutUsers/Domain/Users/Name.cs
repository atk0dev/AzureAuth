using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public record Name
    {
        private const int DefaultLength = 4;

        private Name(string value) => Value = value;

        public string Value { get; init; }

        public static Name? Create(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            if (value.Length <= DefaultLength)
            {
                return null;
            }

            return new Name(value);
        }
    }
}
