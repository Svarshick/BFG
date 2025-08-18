using DataBinding;
using R3;
using Scellecs.Morpeh;

namespace ECS.GameWorld
{
    public struct GameWorld : IComponent
    {
        public int day;
        public ReactiveProperty<GameWorldDataView> dataView;
    }
}