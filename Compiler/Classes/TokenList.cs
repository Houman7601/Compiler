using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Classes
{
    class TokenList
    {
        public List<string> KeyWords = new List<string>
        {
            "int",
            "#include",
            "iostream",
            "iostream.h",
            "main",
            "float",
            "double",
            "string",
            "bool",
            "break",
            "case",
            "catch",
            "char",
            "const",
            "default",
            "do",
            "else",
            "enum",
            "false",
            "for",
            "friend",
            "goto",
            "if",
            "long",
            "namespace",
            "using",
            "new",
            "operator",
            "or",
            "private",
            "public",
            "protected",
            "register",
            "return",
            "short",
            "signed",
            "static",
            "switch",
            "this",
            "template",
            "throw",
            "true",
            "try",
            "void",
            "while",
            "class",
            "cin",
            "cout",
            "printf",
            "scanf",
            "typedef"
        };

        public List<string> Operators = new List<string>
        {
            "+" , "-" , "/" , "*" , "=" , "^" , "%" , ">" , "<"  , "&"
        };

        public List<string> Dellimeters = new List<string>
        {
            "," , ";" , ":" , "." , "(" , ")" , "[" , "]" , "\"" , "\'" ,  "?"
        };
    }
}
