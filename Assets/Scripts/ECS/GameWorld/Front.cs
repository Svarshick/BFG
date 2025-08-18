using Configs;
using DataBinding;
using R3;
using Scellecs.Morpeh;

namespace ECS.GameWorld
{
    public struct Front : IComponent
    {
        public FrontConfig config;
        public ReactiveProperty<FrontDataView> dataView;
    }
}