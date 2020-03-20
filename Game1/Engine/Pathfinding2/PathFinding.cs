﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1.Engine.Pathfinding2
{
    public class PathFinding : IPathFinding
    {
        public IGrid mGrid { get; }

        IList<INode> openNodes;
        IList<INode> closeNodes;

        const int straightCost = 10;
        const int diagonalCost = 14;

        public PathFinding(IGrid pGrid)
        {
            mGrid = pGrid;
            //mGrid = new Grid(pMapWidth, pMapHeight, pTileSizeWidth, pTileSizeHeight);
            openNodes = new List<INode>();
            closeNodes = new List<INode>();
        }

        public IList<Vector2> FindPath(Vector2 pStartPos, Vector2 pTargetPos)
        {
            // If the start position and target position is the same return
            if (pStartPos == pTargetPos)
            {
                return new List<Vector2>();
            }

            // Get start node grid position
            INode startNode = mGrid.GetNodePosition(pStartPos);
            // Get target node grid position
            INode targetNode = mGrid.GetNodePosition(pTargetPos);
            // The current node is the start node
            INode currentNode = startNode;

            // Clear
            openNodes.Clear();
            openNodes.Add(startNode);

            while (openNodes.Count != 0)
            {
                openNodes.Remove(currentNode);
                currentNode.Visited = true;

                if (currentNode == targetNode)
                {
                    return GetFinalPath(startNode, targetNode);
                }

                foreach (INode neighbour in mGrid.GetNeighbourNodes(currentNode))
                {
                    if (neighbour.Walkable)
                    {
                        startNode.HCost = EstimateHCost(startNode, targetNode);

                        int movementCost = currentNode.GCost + EstimateHCost(currentNode, neighbour);

                        if (movementCost < neighbour.GCost || !openNodes.Contains(neighbour))
                        {
                            neighbour.GCost = movementCost;
                            neighbour.HCost = EstimateHCost(neighbour, targetNode);
                            neighbour.Parent = currentNode;

                            if (!openNodes.Contains(neighbour))
                            {
                                openNodes.Add(neighbour);
                                neighbour.Visited = true;
                            }
                        }
                    }                    
                }
            }

            return null;
        }

        /// <summary>
        /// Tracing the path backwards
        /// </summary>
        /// <param name="startNode"></param>
        /// <param name="targetNode"></param>
        private IList<Vector2> GetFinalPath(INode startNode, INode targetNode)
        {
            //IList<INode> path = new List<INode>();
            IList<Vector2> path = new List<Vector2>();

            INode currentNode = targetNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode.gridPos);
                currentNode = currentNode.Parent;
            }

            path.Reverse();
            return path;
        }

        public int EstimateHCost(INode pStartNode, INode pTargetNode)
        {
            int distanceX = (int)Math.Abs(pStartNode.Position.X - pTargetNode.Position.X);
            int distanceY = (int)Math.Abs(pStartNode.Position.Y - pTargetNode.Position.Y);
            
            if (distanceX > distanceY)
            {
                return diagonalCost * distanceY + straightCost * (distanceX - distanceY);
            }

            return diagonalCost * distanceX + straightCost * (distanceY - distanceX);
        }
        
    }
}
