using System;
using Configs;
using ObservableCollections;
using Scellecs.Morpeh;

namespace ECS.Player
{
    [Serializable]
    public struct Tagged : IComponent
    {
        public ObservableHashSet<Tag> value;
    }
}