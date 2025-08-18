using DataBinding;
using R3;

namespace Core.Query
{
    public struct StartFrontQ : IQ
    {
        public ReactiveProperty<FrontDataView> front;
    }
}