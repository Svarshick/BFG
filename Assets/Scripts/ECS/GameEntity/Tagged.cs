using Configs;
using ObservableCollections;
using Scellecs.Morpeh;

namespace ECS.GameEntity
{
    public struct Tagged : IComponent
    {
        public ObservableDictionary<Tag, bool> value;
    }
}