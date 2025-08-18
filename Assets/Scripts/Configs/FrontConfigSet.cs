using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "FrontConfigSet", menuName = "Scriptable Objects/FrontSetConfig")]
    public class FrontConfigSet : ScriptableObject
    {
        [SerializeField] public List<FrontConfig> fronts;
    }
}