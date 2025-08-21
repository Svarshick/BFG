using DataBinding;
using ObservableCollections;
using R3;
using Scellecs.Morpeh;

namespace ECS.GW
{
    public struct Location : IComponent
    {
        public Configs.Location value;
        public ObservableList<ReactiveProperty<FrontDataView>> fronts;
    }
}