using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Checkers
    {
        public bool IsVar(char v)
        {
            if (v >= 'a' && v <= 'z')
            {              
                return true;
            }
            else if (v >= 'A' && v <= 'Z')
            {
                return true;
            }
            return false;
        }

        public bool IsDigit(char a)
        {
            if (a >= '0' && a <= '9')
            {
                return true;
            }
            return false;
        }
    }
}
