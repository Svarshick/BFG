using System.Collections.Generic;
using Configs;
using DataBinding;
using ObservableCollections;
using R3;
using Scellecs.Morpeh;

namespace ECS.GW
{
    public static class FrontUtils
    {
        //todo change ObservableHashSet to just HashSet
        public static bool RequirementsMet(in ObservableHashSet<Tag> status, in Dictionary<Tag, bool> requirements)
        {
            foreach (var requirement in requirements)
            { 
                var tag = requirement.Key;
                var needed = requirement.Value;
                if (needed && !status.Contains(tag))
                    return false;
                if (!needed && status.Contains(tag))
                    return false;
            }
            return true;
        }
 
        //todo change ObservableHashSet to just HashSet
        public static void Affect(in ObservableHashSet<Tag> status, in Dictionary<Tag, bool> effect)
        {
            foreach (var changes in effect)
            {
                if (changes.Value)
                    status.Add(changes.Key);
                else
                    status.Remove(changes.Key);
            }
        }       
        
        //todo I don't like givinig Entity parameter just bacause filling reverseFrontMapping. But I don't want to give World and GetStash because it seems like overkill
        public static ReactiveProperty<FrontDataView> AddFrontView (ref Front front, ref GameWorld gameWorld, ref Entity frontEntity)
        {
            var dataView = CreateFrontView(front, 0);

            front.view = new ReactiveProperty<FrontDataView>(dataView);
            gameWorld.frontsView.Add(front.view);
            gameWorld.reverseFrontMapping[front.view] = frontEntity;
            return front.view;
        }

        public static FrontDataView CreateFrontView(Front front, int configNumber)
        {
            var stageConfig = front.config.stages[configNumber];
            var choicesConfig = stageConfig.choices;
            var choices = new List<StageChoice>(choicesConfig.Count);
            for (int i = 0; i < choicesConfig.Count; ++i)
            {
                var choiceConfig = choicesConfig[i];
                //stageNumber is the programming number (0-based); Number is the human number (1-based)
                var choice = new StageChoice { Text = choiceConfig.text, Number = i+1, Effect = choiceConfig.effect };
                choices.Add(choice);
            }
            var stage = new Stage { Text = stageConfig.text, Choices = choices };
            var name = front.config.frontName;
            var fiasco = new Fiasco { Text = front.config.fiascoText, Effect = front.config.fiascoEffect };
            return new FrontDataView(name, stage, fiasco);
        }

    }
}