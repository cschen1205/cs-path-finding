﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFinding
{
    public abstract class IAgent
    {
        public bool enabled = true;
        protected FVec3 mPosition = new FVec3();
        protected int agentID = -1;

        private QuadTree mSpace;
        private GridWorld mPathFinder;

        public QuadTree Space
        {
            get { return mSpace; }
            set { mSpace = value; }
        }

        public GridWorld PathFinder
        {
            get { return mPathFinder; }
            set { mPathFinder = value; }
        }
		
        /// <summary>
        /// QuadTreeNode of this agent
        /// </summary>
        private QuadTreeNode node;
        public QuadTreeNode Node
        {
            get { return node; }
            set { node = value;
            }
        }

        public int AgentID
        {
            get { return agentID; }
            set { agentID = value; }
        }


        public virtual FVec3 Position
        {
            get
            {
                return mPosition;
            }
            set
            {
                mPosition = new FVec3(value.x, value.y, value.z);
                if (Node==null)
                {
                    UpdateQuadTree(this);
                }
                else
                {
                    if (!Node.Bounds.Contains(mPosition))
                    {
                        UpdateQuadTree(this);
                    }
                }
            }
        }

        public void UpdateQuadTree(IAgent item)
        {
            RemoveFromQuadTree(item);
            mSpace.Insert(item);
        }

        private void RemoveFromQuadTree(IAgent toRemoveAgent)
        {
            if (toRemoveAgent.Node != null)
            {
                toRemoveAgent.Node.contents.Remove(toRemoveAgent);
            }
            else
            {
                mSpace.OutOfBoundList().Remove(toRemoveAgent);
            }
        }

    }


}
