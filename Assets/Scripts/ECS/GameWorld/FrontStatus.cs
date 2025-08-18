using Scellecs.Morpeh;

namespace ECS.GameWorld
{
    public struct FrontStatus : IComponent
    {
        public bool success;
        public int stage;
        public int playerChoice;
    }
}