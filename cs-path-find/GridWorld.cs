using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    public class GridWorld
    {
        private QuadTree space;
        private int[] s;
        private List<DirectedWeightedEdge>[] adj;
        private int V;
        private int colCount;
        private int rowCount;
        private int xInterval;
        private int zInterval;

        public GridWorld(QuadTree space, IntVec2 resolution=null)
        {
            if(resolution == null)
            {
                resolution = new IntVec2(100, 100);
            }
            this.colCount = resolution.x;
            this.rowCount = resolution.z;
            this.space = space;
            int width = this.space.Width;
            int height = this.space.Height;
            xInterval = width / resolution.x;
            zInterval = height / resolution.z;

            V = resolution.x * resolution.z;
            s = new int[V];
            adj = new List<DirectedWeightedEdge>[V];
            for(int i=0; i < V; ++i)
            {
                s[i] = i;
                adj[i] = new List<DirectedWeightedEdge>();
            }

            for(int i=0; i < this.rowCount; ++i)
            {
                for(int j=0; j < this.colCount; ++j)
                {
                    int v = i * this.colCount + j;
                    for(int ii=-1; ii <= 1; ++ii)
                    {
                        for(int jj=-1; jj <= 1; ++jj)
                        {
                            int w = (i + ii) * this.colCount + (j + jj);
                            if (w == v) continue;
                            if(w < 0 || w >= V)
                            {
                                continue;
                            }
                            Connect(w, v);
                        }
                    }
                }
            }
        }

        private bool IsConnected(int v, int w)
        {
            foreach (DirectedWeightedEdge edge in adj[v])
            {
                if (edge.Other(v) == w)
                {
                    return true;
                }
            }
            return false;
        }

        private double GetDistance(int v, int w)
        {
            FVec2 vv = GetPosition(v);
            FVec2 vw = GetPosition(w);
            return vv.GetDistanceTo(vw);
        }

        public void Connect(int v, int w)
        {
            if (IsConnected(w, v)) return;
            double d = GetDistance(w, v);
            DirectedWeightedEdge edge1 = new DirectedWeightedEdge(v, w, d);
            DirectedWeightedEdge edge2 = new DirectedWeightedEdge(w, v, d);
            adj[v].Add(edge1);
            adj[w].Add(edge2);
        }

        public int ColumnCount
        {
            get { return colCount; }
        }

        public int RowCount
        {
            get { return rowCount; }
        }

        public int VertexCount
        {
            get { return V; }
        }

        public List<DirectedWeightedEdge> Adj(int v)
        {
            return adj[v];
        }

        public FVec2 GetPosition(int v)
        {
            int z_v = (int)(Math.Floor((double)v / colCount));
            int x_v = v - z_v * colCount;

            return new FVec2(xInterval * x_v, zInterval * z_v);
        }

        public int GetVertex(FVec2 position)
        {
            int x = (int)(position.x / xInterval);
            int z = (int)(position.z / zInterval);

            if (x < 0) x = 0;
            if (x >= colCount) x = colCount - 1;
            if (z < 0) z = 0;
            if (z >= RowCount) z = rowCount - 1;

            return z * colCount + x;
        }

        public Dijstra dijstra(FVec2 target)
        {
            int s = GetVertex(target);
            return new Dijstra(this, s);
        }

    }

   
}
