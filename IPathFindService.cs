namespace AStar
{
    /// <summary>
    ///     Service for find optimal path on a graph
    /// </summary>
    public interface IPathFindService
    {
        GraphPath Find(IGraph graph, PathFindingOptions options);
    }
}