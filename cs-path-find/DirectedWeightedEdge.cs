using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    public class DirectedWeightedEdge
    {
        private int v;
        private int w;
        private double weight;

        public DirectedWeightedEdge(int v, int w, double weight)
        {
            this.v = v;
            this.w = w;
            this.weight = weight;
        }
        public double Weight
        {
            get { return weight; }
        }
        public int From()
        {
            return v;
        }
        public int To()
        {
            return w;
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
