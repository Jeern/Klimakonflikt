using System;

namespace GameDev.Core
{
    public class EventArgs<T> : EventArgs
    {
        public EventArgs(T data)
        {
            m_Data = data;
        }
        private T m_Data;
        public T Data { get { return m_Data; } }
    }
}
