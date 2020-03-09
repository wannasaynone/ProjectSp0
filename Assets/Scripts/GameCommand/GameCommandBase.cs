using UnityEngine;
using System;
using KahaGameCore.Interface;

namespace ProjectSP0.GameCommand
{
    public abstract class GameCommandBase : ScriptableObject, IProcessable
    {
        public ICombatUnit caster;
        public ICombatUnit receiver;

        public abstract void Process(Action info);
    }
}
