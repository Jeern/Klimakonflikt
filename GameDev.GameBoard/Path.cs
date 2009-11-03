using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GameDev.GameBoard
{
    public class Path<T> : IEnumerable<T>
    {
        public T LastStep { get; private set; }
        public Path<T> PreviousSteps { get; private set; }
        public double TotalCost { get; private set; }
        private Path(T lastStep, Path<T> previousSteps, double totalCost)
        {
            LastStep = lastStep;
            PreviousSteps = previousSteps;
            TotalCost = totalCost;
        }
        public Path(T start) : this(start, null, 0) { }
        public Path<T> AddStep(T step, double stepCost)
        {
            return new Path<T>(step, this, TotalCost + stepCost);
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (Path<T> p = this; p != null; p = p.PreviousSteps)
                yield return p.LastStep;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
