using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using WPF_Calculadora.Funciones;
using info.lundin.math;

namespace WPF_Calculadora.Models
{
    public class MathOperationCollection : ObservableCollection<Operation>
    {

    }

    public class Operation
    {
        private string nombre;
        private string expresion;
        private double resultado;
        public Operation(string nombre, string expresion)
        {
            Nombre = nombre;
            Expresion = expresion;
        }
        public string Nombre
        {
            get { return this.nombre; }
            set 
            {
                if (value != null && value != "")
                {
                    this.nombre = value;
                }
                else
                {
                    MessageBox.Show("El NOMBRE no puede quedar vacio");
                }
            }
        }
        public string Expresion
        {
            get { return this.expresion; }
            set 
            {
                if (value != null && value != "")
                {
                    if (OperationValidator.IsAValidLexicon(value))
                    {
                        ExpressionParser parser = new ExpressionParser();
                        this.expresion = value;
                        this.Resultado = parser.Parse(value);
                    }
                    else
                    {
                        this.expresion = "0";
                        this.resultado = 0;
                        MessageBox.Show("La expresión no es valida");
                    }
                }
                else
                {
                    this.expresion = "0";
                    this.resultado = 0;
                    MessageBox.Show("La EXPRESION no puede quedar vacia");
                }
            }
        }
        public double Resultado
        {
            get { return this.resultado; }
            set { this.resultado = value; }
        }
        public bool SonSemejantes(Operation temp)
        {
            if (string.Compare(Nombre, temp.Nombre)==0 && string.Compare(Expresion, temp.Expresion) == 0 && Resultado == temp.Resultado)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        override
        public String ToString()
        {
            //return Convert.ToString(resultado);
            return this.expresion;
        }
    }
}
