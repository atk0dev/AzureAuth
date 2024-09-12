using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public record Youtube
    {
        private Youtube(string value) => Value = value;

        public string Value { get; init; }

        public static Youtube? Create(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            if (!value.StartsWith("@"))
            {
                return null;
            }

            return new Youtube(value);
        }
    }
}
