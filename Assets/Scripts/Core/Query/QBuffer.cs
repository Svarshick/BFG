using System.Collections.Generic;
using UnityEngine;

namespace Core.Query
{
    public class QBuffer
    {
        private Queue<IQ> _buffer = new();

        public QBuffer() { }
        
        public void Query(IQ q)
        {
            _buffer.Enqueue(q);
        }

        public Queue<IQ> Fetch()
        {
            var oldBuffer = _buffer;
            _buffer = new ();
            return oldBuffer;
        }
    }
}