using Core.Query;
using ECS.Player;
using Scellecs.Morpeh;

namespace ECS.GW
{
    public class FrontCycleSystem : ISystem
    {
        public World World { get; set; }

        private Entity _qQueue;
        private Stash<QQueue> _qQueueStash;
        
        private Entity _gameWorld;
        private Stash<GameWorld> _gameWorldStash;

        private Stash<Front> _frontStash;
        
        private Entity _player;
        private Stash<Tagged> _taggedStash;

        public void OnAwake()
        {
            _qQueue = World.Filter.With<QQueue>().Build().First();
            _qQueueStash = World.GetStash<QQueue>();
            
            _gameWorld = World.Filter.With<GameWorld>().Build().First();
            _gameWorldStash = World.GetStash<GameWorld>();
            
            _player = World.Filter.With<Tagged>().Build().First();
            _taggedStash = World.GetStash<Tagged>();
            
            _frontStash = World.GetStash<Front>();
        }

        public void OnUpdate(float deltaTime)
        {
            ref var qQueueComp = ref _qQueueStash.Get(_qQueue);
            foreach (var q in qQueueComp.value)
            {
                if (q is MakeChoiceQ makeChoiceQ)
                {
                    MakeChoice(makeChoiceQ);
                }
            }
        }

        private void MakeChoice(MakeChoiceQ makeChoiceQ)
        {
            ref var qQueueComp = ref _qQueueStash.Get(_qQueue);
            ref var gameWorldComp = ref _gameWorldStash.Get(_gameWorld);
            ref var playerTags = ref _taggedStash.Get(_player);
            var front = gameWorldComp.reverseFrontMapping[makeChoiceQ.front];
            ref var frontComp = ref _frontStash.Get(front);
            var choice = frontComp.config.stages[gameWorldComp.day].choices[makeChoiceQ.choiceNumber - 1];
            
            FrontUtils.Affect(playerTags.value, choice.effect);
            qQueueComp.buffer.Query(new EndDayQ());
        }

        public void Dispose()
        {
        }
    }
}