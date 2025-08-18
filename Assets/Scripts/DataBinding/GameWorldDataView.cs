using System.Collections.Generic;
using Configs;

namespace DataBinding
{
    public class GameWorldDataView : IDataView
    {
        public int Day;
        public Dictionary<Location, bool> tags;
    }
}