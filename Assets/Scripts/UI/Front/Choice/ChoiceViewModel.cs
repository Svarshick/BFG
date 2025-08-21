using DataBinding;
using R3;

namespace UI.Front.Choice
{
    public class ChoiceViewModel : ViewModel
    {
        private readonly DataViewStorage _dataViewStorage;
        public readonly GameLogicMediator GameLogicMediator;

        public ReactiveProperty<FrontDataView> FrontDataView { get; private set; }
        public StageChoice StageChoice { get; private set; }

        public ChoiceViewModel(
            DataViewStorage dataViewStorage,
            GameLogicMediator gameLogicMediator,
            ReactiveProperty<FrontDataView> frontDataView,
            StageChoice stageChoice
        )
        {
            _dataViewStorage = dataViewStorage;
            GameLogicMediator = gameLogicMediator;
            FrontDataView = frontDataView;
            StageChoice = stageChoice;
        }
    }
}