using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "GameWorldInitConfig", menuName = "Scriptable Objects/GameWorldInitConfig")]
    public class GameWorldInitConfig : ScriptableObject
    {
        [SerializedDictionary("Tag", "Value")] public SerializedDictionary<Tag, bool> playerTags;
    }
}