using UnityEngine;
using ProjectSP0.AI;

namespace ProjectSP0
{
    [CreateAssetMenu(menuName = "Game Data/Monster")]
    public class MonsterRawData : ScriptableObject
    {
        public string MonsterName { get { return m_name; } }
        public int DefaultHP { get { return m_defaultHP; } }
        public int DefaultAttack { get { return m_defaultAttack; } }
        public int DefaultDefence { get { return m_defaultDefence; } }
        public int DefaultDex { get { return m_defaultDex; } }
        public int DefaultMinAttackDistance { get { return m_defaultMinAttackDistance; } }
        public int DefaultMaxAttackDistance { get { return m_defaultMaxAttackDistance; } }
        public GameBuff[] DefaultBuffs { get { return m_defaultBuffs; } }
        public AIBehaviourBase AIBehaviour { get { return m_defaultAI; } }

        [SerializeField] private string m_name = "";
        [SerializeField] private int m_defaultHP = 0;
        [SerializeField] private int m_defaultAttack = 0;
        [SerializeField] private int m_defaultDefence = 0;
        [SerializeField] private int m_defaultDex = 0;
        [SerializeField] private int m_defaultMinAttackDistance = 0;
        [SerializeField] private int m_defaultMaxAttackDistance = 1;
        [SerializeField] private GameBuff[] m_defaultBuffs = null;
        [SerializeField] private AIBehaviourBase m_defaultAI = null;
    }
}
