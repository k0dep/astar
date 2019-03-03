using UnityEngine;

namespace AStar
{
    /// <summary>
    ///     Graph representation
    /// </summary>
    public interface IGraph
    {
        /// <summary>
        ///     Graph horizontal node count
        /// </summary>
        int Width { get; }

        /// <summary>
        ///     Graph vertical node count
        /// </summary>
        int Height { get; }

        /// <summary>
        ///     Get a transition cost from one point to second
        /// </summary>
        /// <param name="from"> first node address </param>
        /// <param name="to"> second node address </param>
        /// <returns> cost of a transition </returns>
        float GetTransition(Vector2Int from, Vector2Int to);

        /// <summary>
        ///     Set or rewrite a transition cost from one point to second
        /// </summary>
        /// <param name="from"> first node address </param>
        /// <param name="to"> second node address </param>
        void SetTransition(Vector2Int from, Vector2Int to, float cost);
    }
}