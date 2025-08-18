using Configs;
using DataBinding;

namespace UI
{
    public class LocationViewModel
    {
        public GameLogicMediator GameLogicMediator { get; }
        public Location Location { get; }
        
        public LocationViewModel(
            GameLogicMediator gameLogicMediator,
            Location location
            )
        {
            GameLogicMediator = gameLogicMediator;
            Location = location;
        }
    }
}