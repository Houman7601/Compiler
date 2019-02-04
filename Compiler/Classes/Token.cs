using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class Token
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int BlockNumber { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
    }
}
