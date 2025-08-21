using System;
using System.Collections.Generic;
using DataBinding;
using R3;
using Scellecs.Morpeh;
using UnityEngine;

namespace ECS.GW
{
    [Serializable]
    public struct GameWorld : IComponent
    {
        public HashSet<ReactiveProperty<FrontDataView>> frontsView;
        public Dictionary<ReactiveProperty<FrontDataView>, Entity> reverseFrontMapping;
        public ReactiveProperty<ReactiveProperty<FrontDataView>> currentFront;
        public ReactiveProperty<GameWorldDataView> view;
        public int day;
    }
}