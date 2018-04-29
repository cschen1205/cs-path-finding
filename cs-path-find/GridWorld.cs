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
        private List<WeightedEdge>[] adj;
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
            for(int i=0; i < V; ++i)
            {
                s[i] = i;
                adj[i] = new List<WeightedEdge>();
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
            foreach (WeightedEdge edge in adj[v])
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
            int z_v = v / colCount;
            int x_v = v - z_v * colCount;

            int z_w = w / colCount;
            int x_w = w - z_w * colCount;

            FVec2 vv = new FVec2(xInterval * x_v, zInterval * z_v);
            FVec2 vw = new FVec2(xInterval * x_w, zInterval * z_w);
            return vv.GetDistanceTo(vw);
        }

        public void Connect(int v, int w)
        {
            if (IsConnected(w, v)) return;
            WeightedEdge edge = new WeightedEdge(v, w, GetDistance(v, w));
            adj[w].Add(edge);
            adj[v].Add(edge);
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



    }

   
}
