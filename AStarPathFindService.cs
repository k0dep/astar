using System;
using System.Collections.Generic;
using UnityEngine;

namespace AStar
{
    public class AStarPathFindService : IPathFindService
    {
        private readonly IGraphNeighborsService _neighborsService;

        public AStarPathFindService(IGraphNeighborsService neighborsService)
        {
            _neighborsService = neighborsService;
        }

        /// <summary>
        ///     Found optimal path on graph using A* algorithm
        /// </summary>
        /// <returns> path object or null, if path not found </returns>
        public GraphPath Find(IGraph graph, PathFindingOptions options)
        {
            if (graph == null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            
            var neighbors = new List<Vector2Int>(8);
            var frontier = new PriorityQueue<Vector2Int>();
            frontier.Add(0, options.End);

            var cameFrom = new Dictionary<Vector2Int, Vector2Int>();
            var costsFar = new Dictionary<Vector2Int, float>()
            {
                [options.End] = 0
            };

            while (frontier.Count > 0)
            {
                if (NextVariant())
                {
                    break;
                }
            }

            if (!costsFar.ContainsKey(options.Start))
            {
                return null;
            }

            var result = new GraphPath();
            result.Path = new List<Vector2Int>();
            
            var reverseNode = options.Start;
            while (reverseNode != null)
            {
                result.Path.Add(reverseNode);
                if (!cameFrom.ContainsKey(reverseNode))
                {
                    break;
                }

                reverseNode = cameFrom[reverseNode];
            }

            return result;

            bool NextVariant()
            {
                var current = frontier.Peek();
                frontier.RemoveMin();


                if (current == options.Start)
                {
                    return true;
                }

                _neighborsService.GetNeighbors(graph, current, neighbors);

                foreach (var neighbor in neighbors)
                {
                    var newCost = costsFar[current] + graph.GetTransition(current, neighbor);
                    if (!costsFar.ContainsKey(neighbor) || newCost < costsFar[neighbor])
                    {
                        costsFar[neighbor] = newCost;
                        int priority = (int) (newCost + Heuristic(options.Start, neighbor));
                        frontier.Add(priority, neighbor);
                        cameFrom[neighbor] = current;
                    }
                }

                return false;
            }
        }

        /// <summary>
        ///     Heuristic function, https://en.wikipedia.org/wiki/Chebyshev_distance
        /// </summary>
        private float Heuristic(Vector2Int from, Vector2Int to)
        {
            return Math.Max(Math.Abs(from.x - to.x), Math.Abs(from.y - to.y));
        }
    }
}