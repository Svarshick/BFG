using System.Collections.Generic;
using Core.Query;
using Scellecs.Morpeh;

namespace ECS
{
    public struct QQueue : IComponent
    {
        public Queue<IQ> value;
    }
}