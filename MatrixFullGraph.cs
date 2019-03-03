using System;
using UnityEngine;

namespace AStar
{
    /// <summary>
    ///     Graph representation using full linked transition matrix
    /// </summary>
    public class MatrixFullGraph : IGraph
    {
        /// <summary>
        ///     Transition matrix of graph
        /// </summary>
        public readonly float[,] Matrix;
        
        /// <summary>
        ///     Graph horizontal node count
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        ///     Graph vertical node count
        /// </summary>
        public int Height { get; private set; }


        public MatrixFullGraph(int height, int width, float defaultCost = 0f)
        {
            if(height <= 0)
            {
                throw new ArgumentException("height should be more than zero", nameof(height));
            }
            
            if(width <= 0)
            {
                throw new ArgumentException("value should be more than zero", nameof(width));
            }

            Width = width;
            Height = height;
            
            var nodeCount = height * width;

            Matrix = new float[nodeCount, nodeCount];
            for (var y = 0; y < nodeCount; y++)
            {
                for (var x = 0; x < nodeCount; x++)
                {
                    Matrix[x, y] = defaultCost;
                }
            }
        }

        /// <summary>
        ///     Get a transition cost from one point to second
        /// </summary>
        /// <param name="from"> first node address </param>
        /// <param name="to"> second node address </param>
        /// <returns> cost of a transition </returns>
        public float GetTransition(Vector2Int from, Vector2Int to)
        {
            AssertNodeAddress(from, nameof(from));
            AssertNodeAddress(to, nameof(to));
            
            return Matrix[MapToFlat(from), MapToFlat(to)];
        }
        
        /// <summary>
        ///     Set or rewrite a transition cost from one point to second
        /// </summary>
        /// <param name="from"> first node address </param>
        /// <param name="to"> second node address </param>
        public void SetTransition(Vector2Int from, Vector2Int to, float cost)
        {
            AssertNodeAddress(from, nameof(from));
            AssertNodeAddress(to, nameof(to));
            
            Matrix[MapToFlat(from), MapToFlat(to)] = cost;
        }

        /// <summary>
        ///     Convert vector node address to flat representation
        /// </summary>
        private int MapToFlat(Vector2Int vector) =>  vector.y * Width + vector.x;

        /// <summary>
        ///     Assert an node address
        /// </summary>
        private void AssertNodeAddress(Vector2Int vector, string name)
        {
            if (vector.x < 0 || vector.y < 0)
            {
                throw new ArgumentException("value should be more or equal than zero", name);
            }

            if (vector.x >= Width || vector.y >= Height)
            {
                throw new ArgumentException("value should be less than graph bounds", name);
            }
        }
    }
}
