using System.Collections.Generic;
using Configs;
using ECS.GameWorld;

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

        //should be somewere else
        public static FrontDataView Create(Front front, int stageNumber)
        {
            var frontName = front.config.frontName;
            var stageConf = front.config.stages[stageNumber];
            var choicesConf = stageConf.choices;
            var choices = new List<StageChoice>(choicesConf.Count);
            for (int i = 0; i < choicesConf.Count; ++i)
            {
                var choiceConf = choicesConf[i];
                //stageNumber is the programming number (0-based); Number is the human number (1-based)
                var choice = new StageChoice { Text = choiceConf.text, Number = i+1, Effect = choiceConf.effect };
                choices.Add(choice);
            }
            var stage = new Stage { Text = stageConf.text, Choices = choices };
            var fiasco = new Fiasco { Text = front.config.fiascoText, Effect = front.config.fiascoEffect };
            return new FrontDataView(frontName, stage, fiasco);
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