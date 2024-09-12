using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public record Linkedin
    {
        private Linkedin(string value) => Value = value;

        public string Value { get; init; }

        public static Linkedin? Create(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            if (!value.StartsWith("https://www.linkedin.com/in/"))
            {
                return null;
            }

            return new Linkedin(value);
        }
    }
}
