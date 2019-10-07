using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX_CG_AULA_2
{
    class RGB
    {
        private double R;
        private double G;
        private double B;

        public RGB(double r, double g, double b)
        {
            R = r;
            G = g;
            B = b;
        }

        public double getR()
        {
            return R;
        }

        public void setR(double R)
        {
            this.R = R;
        }

        public double getG()
        {
            return G;
        }

        public void setG(double G)
        {
            this.G = G;
        }

        public double getB()
        {
            return B;
        }

        public void setB(double B)
        {
            this.B = B;
        }
    }
}
