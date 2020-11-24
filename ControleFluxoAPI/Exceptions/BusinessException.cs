using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFluxoAPI.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException()
        {

        }

        public BusinessException(String message) : base(message)
        {

        }
    }
}
