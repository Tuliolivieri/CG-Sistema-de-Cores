using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX_CG_AULA_2
{
    class CMY
    {
        private double C;
        private double M;
        private double Y;

        public CMY(double c, double m, double y)
        {
            C = c;
            M = m;
            Y = y;
        }

        public double getC()
        {
            return C;
        }
        public void setC(double C)
        {
            this.C = C;
        }
        public double getY()
        {
            return Y;
        }
        public void setY(double Y)
        {
            this.Y = Y;
        }
        public double getM()
        {
            return M;
        }
        public void setM(double M)
        {
            this.M = M;
        }
    }
}
