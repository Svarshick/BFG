using Core.Query;
using Scellecs.Morpeh;

namespace ECS
{
    public class QReadSystem : ISystem
    {
        public World World { get; set; }
        private QBuffer _qBuffer;
        private Entity _qQueue;
        private Stash<QQueue> _qQueueStash;

        public QReadSystem(QBuffer qBuffer)
        {
            _qBuffer = qBuffer;
        }
        
        public void OnAwake()
        {
            var qFilter = World.Filter.With<QQueue>().Build();
            _qQueue = qFilter.First();

            _qQueueStash = World.GetStash<QQueue>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            ref var qQueueCmp = ref _qQueueStash.Get(_qQueue);
            qQueueCmp.value = _qBuffer.Fetch();
        }
        
        public void Dispose() { }
    }
}