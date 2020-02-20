using UnityEngine;

namespace KahaGameCore.Interface
{
    public abstract class ConditionBase : ScriptableObject
    {
        public abstract bool IsTrue();
    }

}
