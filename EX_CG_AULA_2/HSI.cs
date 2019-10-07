using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX_CG_AULA_2
{
    class HSI
    {
        private double H;
        private double S;
        private double I;

        public HSI(double h, double s, double i)
        {
            H = h;
            S = s;
            I = i;
        }

        public double getH()
        {
            return H;
        }
        public void setH(double H)
        {
            this.H = H;
        }
        public double getS()
        {
            return S;
        }
        public void setS(double S)
        {
            this.S = S;
        }
        public double getI()
        {
            return I;
        }
        public void setI(double I)
        {
            this.I = I;
        }
    }
}
