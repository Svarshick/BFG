using Core.Query;
using R3;
using TriInspector;
using UnityEngine;
using VContainer;

namespace DataBinding
{
    // todo it shouldn't be monobeh, but with buttons in editor
    public class GameLogicMediator : MonoBehaviour
    {
        private QBuffer _qBuffer;
        
        [Inject]
        public void Inject(
            QBuffer qBuffer
        )
        {
            _qBuffer = qBuffer;
        }

        [Button] public void EndDay() => _qBuffer.Query(new EndDayQ());
        [Button] public void StartFront(ReactiveProperty<FrontDataView> front) => _qBuffer.Query(new StartFrontQ { front = front});
        [Button] public void MakeChoice(ReactiveProperty<FrontDataView> front, StageChoice choice) => _qBuffer.Query(new MakeChoiceQ {front = front, choiceNumber = choice.Number});
    }
}