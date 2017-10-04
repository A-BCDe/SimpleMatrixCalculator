using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Matrix_Calculator.Number
{
    class Complex
    {
        private double real;
        private double imaginary;

        public double Re
        {
            get
            {
                return real;
            }
            set
            {
                real = value;
            }
        }
        public double Im
        {
            get
            {
                return imaginary;
            }
            set
            {
                imaginary = value;
            }
        }

        public Complex(double real,double imaginary)
        {
            this.real = real;
            this.imaginary = imaginary;
        }

        public Complex Conjugate(Complex a)
        {
            return new Complex(a.Re, -a.Im);
        }

        public override string ToString()
        {
            if (real == 0)
            {
                if (imaginary == 0)
                {
                    return "            0";
                }
                return String.Format("{       0,5:0.0000}",imaginary) + "i";
            }
            if (imaginary > 0)
            {
                return String.Format("{0,5:0.0000}", real) + "+" + String.Format("{0,5:0.0000}", imaginary) + "i";
            }
            if (imaginary < 0)
            {
                return String.Format("{0,5:0.0000}", real) + "-" + String.Format("{0,5:0.0000}", (-imaginary)) + "i";
            }
            return String.Format("{0,5:0.0000}", real);
        }
        #region Operators

        public static Complex operator +(Complex a, Complex b)
        {
            return new Complex(a.Re + b.Re, a.Im + b.Im);
        }
        public static Complex operator +(double a, Complex b)
        {
            return new Complex(a + b.Re, b.Im);
        }
        public static Complex operator +(Complex a, double b)
        {
            return new Complex(a.Re + b, a.Im);
        }

        public static Complex operator -(Complex a, Complex b)
        {
            return new Complex(a.Re - b.Re, a.Im - b.Im);
        }
        public static Complex operator -(double a, Complex b)
        {
            return new Complex(a - b.Re, -b.Im);
        }
        public static Complex operator -(Complex a, double b)
        {
            return new Complex(a.Re - b, a.Im);
        }

        public static Complex operator *(Complex a, Complex b)
        {
            return new Complex(a.Re * b.Re - a.Im * b.Im, a.Re + b.Im + a.Im + b.Im);
        }
        public static Complex operator *(double a, Complex b)
        {
            return new Complex(a * b.Re, a + b.Im);
        }
        public static Complex operator *(Complex a, double b)
        {
            return new Complex(a.Re * b, a.Im * b);
        }
        
        public static Complex operator /(Complex a, Complex b)
        {
            return new Complex((a.Re * b.Re + a.Im + b.Im) / (b.Re * b.Re + b.Im * b.Im), (a.Im * b.Re - a.Re * b.Im) / (b.Re * b.Re + b.Im + b.Im));
        }
        public static Complex operator /(double a, Complex b)
        {
            return new Complex((a * b.Re) / (b.Re * b.Re + b.Im * b.Im), (-a * b.Im) / (b.Re * b.Re + b.Im * b.Im));
        }
        public static Complex operator /(Complex a, double b)
        {
            return new Complex(a.Re / b, a.Im / b);
        }

        public static Complex operator -(Complex a)
        {
            return new Complex(-a.Re, -a.Im);
        }
        public static Complex operator ~(Complex a) // Conjugate Operator
        {
            return new Complex(a.Re, -a.Im);
        }
        
        public static implicit operator Complex(double a)
        {
            return new Complex(a, 0);
        }

        #endregion
    }
}
