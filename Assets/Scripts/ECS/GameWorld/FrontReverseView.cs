using System.Collections.Generic;
using DataBinding;
using R3;
using Scellecs.Morpeh;

namespace ECS.GameWorld
{
    public struct FrontReverseView : IComponent
    {
        public Dictionary<ReactiveProperty<FrontDataView>, Entity> value;
    }
}