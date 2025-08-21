using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "GameInitConfig", menuName = "Scriptable Objects/GameInitConfig")]
    public class GameInitConfig : ScriptableObject
    {
        public List<Tag> playerTags;
    }
}