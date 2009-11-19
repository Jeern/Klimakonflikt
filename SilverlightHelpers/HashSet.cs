using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace SilverlightHelpers
{
    /// <summary>
    /// Silverlight contains no HashSet so this implementation is used instead
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HashSet<T> : Dictionary<T, T>
    {
        public void Add(T node)
        {
            Add(node, node);
        }

        public bool Contains(T node)
        {
            return ContainsKey(node);
        }
    }
}
