using System.Collections.Generic;
using UnityEngine;

namespace AStar
{
    /// <summary>
    ///     Path of graph
    /// </summary>
    public class GraphPath
    {
        /// <summary>
        ///     Path of graph. Direction from 0 element to end.
        /// </summary>
        public IList<Vector2Int> Path { get; set; }
    }
}