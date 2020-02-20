using UnityEngine;
using KahaGameCore.Interface;

namespace ProjectSP0.GameCommand
{
    public abstract class GameCommandBase : ScriptableObject, IProcessable
    {
        public abstract void Process(System.Action onComplete);
    }
}
