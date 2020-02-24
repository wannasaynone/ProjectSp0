using UnityEngine;
using System.Collections.Generic;
using ProjectSP0.GameCommand;

namespace ProjectSP0
{
    [CreateAssetMenu(menuName = "Game Data/Game Buff")]
    public class GameBuff : ScriptableObject
    {
        public enum TriggerWhen
        {
            Immediate,
            OnCombatPreStart,
            OnTurnPreStart,
            OnAttackPreSatrt,
            OnDamaged,
            OnAttacked,
            OnDied,
            OnTurnEnded,
            OnCombatEnded
        }

        public TriggerWhen TriggerTiming { get { return m_triggerWhen; } }
        public int ExistTime { get { return m_existTime; } }
        public GameCommandBase[] Commands
        {
            get
            {
                GameCommandBase[] _newArray = new GameCommandBase[m_commands.Length];
                m_commands.CopyTo(_newArray, 0);
                return _newArray;
            }
        }

        [SerializeField] private TriggerWhen m_triggerWhen = TriggerWhen.Immediate;
        [SerializeField] private int m_existTime = 0;
        [SerializeField] private GameCommandBase[] m_commands = null;
    }
}