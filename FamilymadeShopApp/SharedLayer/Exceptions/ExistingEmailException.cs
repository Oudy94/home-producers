using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Exceptions
{
    public class ExistingEmailException : Exception
    {
        public ExistingEmailException() { }

        public ExistingEmailException(string message) : base(message) { }

        public ExistingEmailException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
