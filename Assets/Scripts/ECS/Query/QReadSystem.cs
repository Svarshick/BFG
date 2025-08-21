using Core.Query;
using Scellecs.Morpeh;

namespace ECS
{
    public class QReadSystem : ISystem
    {
        public World World { get; set; }
        private Entity _qQueue;
        private Stash<QQueue> _qQueueStash;
        
        public void OnAwake()
        {
            var qFilter = World.Filter.With<QQueue>().Build();
            _qQueue = qFilter.First();

            _qQueueStash = World.GetStash<QQueue>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            ref var qQueueComp = ref _qQueueStash.Get(_qQueue);
            qQueueComp.value = qQueueComp.buffer.Fetch();
        }
        
        public void Dispose() { }
    }
}