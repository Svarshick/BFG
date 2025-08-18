using DataBinding;
using R3;

namespace Core.Query
{
    public struct MakeChoiceQ : IQ
    {
        public ReactiveProperty<FrontDataView> front;
        public int choiceNumber;
    }
}