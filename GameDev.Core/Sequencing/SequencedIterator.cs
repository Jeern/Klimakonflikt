using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GameDev.Core.Sequencing
{
    public class SequencedIterator<T> : IEnumerator<T>
    {
        private List<T> m_ListOfItems;
        private Sequencer m_Sequencer;

        public SequencedIterator(List<T> listOfItems, Sequencer sequencer)
        {
            m_ListOfItems = listOfItems;
            m_Sequencer = sequencer;
        }


        #region IEnumerator<T> Members

        public T Current
        {
            get { return m_ListOfItems[m_Sequencer.Current]; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            //Nothing to dispose.
        }

        #endregion

        #region IEnumerator Members

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            return m_Sequencer.MoveNext();
        }

        public void Reset()
        {
            m_Sequencer.Reset();
        }

        #endregion
    }
}
