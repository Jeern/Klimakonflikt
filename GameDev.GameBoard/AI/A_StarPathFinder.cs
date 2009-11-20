using System;
using System.Collections.Generic;
#if SILVERLIGHT
using SilverlightHelpers;
#endif

namespace GameDev.GameBoard.AI
{
    public static class A_StarPathFinder
    {
       public static Path<Node> FindPath<Node>(
       Node start,
       Node destination,
       Func<Node, Node, double> distance,
       Func<Node, double> estimate)
       where Node : IHasNeighbours<Node>
            //where Node : IHasNeighbours<Node>
        {
            var closed = new HashSet<Node>();
            var queue = new PriorityQueue<double, Path<Node>>();
            queue.Enqueue(0, new Path<Node>(start));
            while (!queue.IsEmpty)
            {
                var path = queue.Dequeue();
                if (closed.Contains(path.LastStep))
                    continue;
                if (path.LastStep.Equals(destination))
                    return path;
                closed.Add(path.LastStep);
                foreach (Node n in path.LastStep.AccessibleNeighbours)
                {
                    double stepcost = estimate(n);
                    double d = distance(path.LastStep, n);
                    var newPath = path.AddStep(n, stepcost);
                    queue.Enqueue(newPath.TotalCost + estimate(n), newPath);
                }
            }
            return null;
        }

    }
}
