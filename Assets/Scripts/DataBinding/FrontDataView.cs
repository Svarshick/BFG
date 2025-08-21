using System.Collections.Generic;
using Configs;
using ECS.GW;

namespace DataBinding
{
    public class FrontDataView : IDataView
    {
        public string FrontName;
        public Stage Stage;
        public Fiasco Fiasco;

        public FrontDataView(
            string frontName,
            Stage stage,
            Fiasco fiasco
            )
        {
            FrontName = frontName;
            Stage = stage;
            Fiasco = fiasco;
        }
    }

    public struct Fiasco
    {
        public string Text;
        public Dictionary<Tag, bool> Effect;
    }
    
    public struct Stage
    {
        public string Text;
        public List<StageChoice> Choices;
    }
    
    public struct StageChoice
    {
        public string Text;
        public int Number;
        public Dictionary<Tag, bool> Effect;
    }
}