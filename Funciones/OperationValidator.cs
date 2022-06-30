using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_Calculadora.Funciones
{
    public class OperationValidator
    {
        public static bool IsAValidLexicon(string expresion)
        {
            foreach (char temp in expresion)
            {
                if (!(Char.IsDigit(temp) || IsAnOperationSign(temp) || IsAGroupingSign(temp) || temp == '.'))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IsAnOperationSign(char temp)
        {
            if (temp == '+' || temp == '-' || temp == '*' || temp == '/' || temp == '^')
            {
                return true;
            }
            return false;
        }
        public static bool IsAGroupingSign(char temp)
        {
            //|| temp == '[' || temp == ']' || temp == '{' || temp == '}'
            if (temp == '(' || temp == ')')
            {
                return true;
            }
            return false;
        }
    }
}
