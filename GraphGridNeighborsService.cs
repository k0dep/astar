using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AStar
{
    /// <summary>
    ///     Service for finding neighbor nodes on grid-graph 
    /// </summary>
    public class GraphGridNeighborsService : IGraphNeighborsService
    {
        /// <summary>
        ///     Directions, where needs find neighbors
        /// </summary>
        public Vector2Int[] Directions { get; set; } = new[]
        {
            new Vector2Int(1, 0),
            new Vector2Int(1, -1),
            new Vector2Int(0, -1),
            new Vector2Int(-1, -1),
            new Vector2Int(-1, 0),
            new Vector2Int(-1, 1),
            new Vector2Int(0, 1),
            new Vector2Int(1, 1),
        };

        /// <summary>
        ///     Maximum cost value of transition on graph. if transition more or equal than the value,
        ///  destination node is not reachable, or not is neighbor. 
        /// </summary>
        private float MaxCost { get; set; }
        
        /// <param name="maxCost"> threshold value of cost after which neighbor node is not reachable </param>
        public GraphGridNeighborsService(float maxCost)
        {
            MaxCost = maxCost;
        }
        
        /// <summary>
        ///     Find reachable neighbour for node. Result stored in neighbors
        /// </summary>
        public void GetNeighbors(IGraph graph, Vector2Int node, IList<Vector2Int> neighbors)
        {
            if (graph == null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            
            if (neighbors == null)
            {
                throw new ArgumentNullException(nameof(neighbors));
            }
            
            if (!IsValidNode(graph, node))
            {
                throw new ArgumentException("node not in graph bounds", nameof(node));
            }
            
            neighbors.Clear();
            for (var index = 0; index < Directions.Length; index++)
            {
                var direction = Directions[index];
                var nextNodeAddr = node + direction;
                if (!IsValidNode(graph, nextNodeAddr))
                {
                    continue;
                }

                var cost = graph.GetTransition(node, nextNodeAddr);
                if (cost >= MaxCost)
                {
                    continue;
                }
                
                neighbors.Add(nextNodeAddr);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsValidNode(IGraph graph, Vector2Int node)
        {
            return node.x >= 0 && node.y >= 0 && node.x < graph.Width && node.y < graph.Height;
        }
    }
}