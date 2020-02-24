using UnityEngine;

namespace ProjectSP0.AI
{
    public abstract class AIBehaviourBase : ScriptableObject
    {
        public abstract void Do(Monster monster);
    }
}
