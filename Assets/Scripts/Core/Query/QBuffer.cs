using System.Collections.Generic;
using UnityEngine;

namespace Core.Query
{
    public class QBuffer
    {
        private Queue<IQ> _qQueue = new();
        
        public void Query(IQ q)
        {
            _qQueue.Enqueue(q);
        }

        public Queue<IQ> Fetch()
        {
            var oldBuffer = _qQueue;
            _qQueue = new ();
            return oldBuffer;
        }
    }
}