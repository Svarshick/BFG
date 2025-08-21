using DataBinding;
using UnityEngine.UIElements;

namespace UI.Front.Choice
{
    public class ChoiceView : View
    {
        private readonly ChoiceViewModel _viewModel;
        private readonly UIEventAggregator _uiEventAggregator;
        
        private Button _choiceButton;

        public ChoiceView(
            ChoiceViewModel viewModel,
            UIEventAggregator uiEventAggregator,
            VisualElement root
        ) : base(root)
        {
            _viewModel = viewModel;
            _uiEventAggregator = uiEventAggregator;
        }

        protected override void SetVisualElements()
        {
            _choiceButton = Root.Q<Button>("choiceButton");
            _choiceButton.text = _viewModel.StageChoice.Text;
        }

        protected override void RegisterInputCallbacks()
        {
            _choiceButton.RegisterCallback<ClickEvent>(evt =>
            {
                _viewModel.GameLogicMediator.MakeChoice(_viewModel.FrontDataView, _viewModel.StageChoice);
                _uiEventAggregator.EndFront();
            });
        }
    }
}