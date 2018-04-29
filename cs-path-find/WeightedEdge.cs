using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    public class WeightedEdge
    {
        private int v;
        private int w;
        private double weight;

        public WeightedEdge(int v, int w, double weight)
        {
            this.v = v;
            this.w = w;
            this.weight = weight;
        }
        public int Either()
        {
            return v;
        }
        public int Other(int d)
        {
            if(d == v)
            {
                return w;
            }
            return v;
        }
    }
}
