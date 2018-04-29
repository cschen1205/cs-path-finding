using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    public class Dijstra
    {
        private int s;
        private DirectedWeightedEdge[] edgeTo;
        private bool[] marked;
        private double[] cost;
        private IndexMinPQ<double> pq;

        public Dijstra(GridWorld G, int s)
        {
            this.s = s;
            int V = G.VertexCount;
            marked = new bool[V];
            edgeTo = new DirectedWeightedEdge[V];
            cost = new double[V];

            for (var i = 0; i < V; ++i)
            {
                cost[i] = double.MaxValue;
            }

            cost[s] = 0;

            pq = new IndexMinPQ<double>(V);


            pq.Insert(s, 0);

            while (!pq.IsEmpty)
            {
                var v = pq.DelMin();
                marked[v] = true;
                foreach (var e in G.Adj(v))
                {
                    Relax(G, e);
                }
            }
        }

        private void Relax(GridWorld G, DirectedWeightedEdge e)
        {
            int v = e.From();
            int w = e.To();
            if (cost[w] > cost[v] + e.Weight)
            {
                cost[w] = cost[v] + e.Weight;
                edgeTo[w] = e;
                if (!pq.Contains(w))
                {
                    pq.Insert(w, cost[w]);
                }
                else
                {
                    pq.DecreaseKey(w, cost[w]);
                }
            }
        }

        public bool HasPathTo(int v)
        {
            return marked[v];
        }

        public IEnumerable<DirectedWeightedEdge> PathTo(int v)
        {
            var path = new Stack<DirectedWeightedEdge>();
            for (var x = v; x != s; x = edgeTo[x].From())
            {
                path.Push(edgeTo[x]);
            }
            return path;
        }
    }
}
