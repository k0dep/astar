using UnityEngine;

namespace AStar
{
    /// <summary>
    ///     Options for finding path on graoh
    /// </summary>
    public class PathFindingOptions
    {
        public Vector2Int Start { get; set; }
        public Vector2Int End { get; set; }
    }
}