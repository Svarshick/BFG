using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Configs
{
    [Serializable]
    public struct Choice
    {
        [SerializeField] public string text;
        [SerializedDictionary("Tag", "Value")] public SerializedDictionary<Tag, bool> requirements;
        [SerializedDictionary("Tag", "Value")] public SerializedDictionary<Tag, bool> effect;
    }

    [Serializable]
    public struct StageConfig
    {
        [SerializeField] public string text;
        [SerializeField] public List<Choice> choices;
    }
    
    [CreateAssetMenu(fileName = "FrontConfig", menuName = "Scriptable Objects/FrontConfig")]
    public class FrontConfig : ScriptableObject
    {
        [SerializeField] public string frontName;
        [SerializeField] public int awakeningDay;
        [SerializeField] public Location location;
        [SerializedDictionary("Tag", "Value")] public SerializedDictionary<Tag, bool> requirements;

        [SerializeField] public List<StageConfig> stages;

        [SerializeField] public string fiascoText;
        [SerializedDictionary("Tag", "Value")] public SerializedDictionary<Tag, bool> fiascoEffect;
    }
}