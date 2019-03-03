using System.Collections.Generic;
using UnityEngine;

namespace AStar
{
    /// <summary>
    ///     Service for finding neighbor nodes on graph 
    /// </summary>
    public interface IGraphNeighborsService
    {
        /// <summary>
        ///     Find reachable neighbour for node. Result stored in neighbors
        /// </summary>
        void GetNeighbors(IGraph graph, Vector2Int node, IList<Vector2Int> neighbors);
    }
}