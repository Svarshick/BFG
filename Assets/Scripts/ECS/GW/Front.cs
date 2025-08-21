using System;
using Configs;
using DataBinding;
using R3;
using Scellecs.Morpeh;

namespace ECS.GW
{
    [Serializable]
    public struct Front : IComponent
    {
        public FrontConfig config;
        public ReactiveProperty<FrontDataView> view;
    }
}