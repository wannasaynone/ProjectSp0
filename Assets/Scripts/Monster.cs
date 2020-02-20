using UnityEngine;
using System.Collections.Generic;

namespace ProjectSP0
{
    [CreateAssetMenu(menuName = "Game Data/Monster")]
    public class Monster : ScriptableObject, ICombatUnit
    {
        public Int32ValueObject Distance { get; private set; }

        public Int32ValueObject HP { get; private set; }
        [SerializeField] private int m_defaultDistance = 0;
        [SerializeField] private int m_defaultAttack = 0;
        [SerializeField] private int m_defaultDefence = 0;
        [SerializeField] private int m_defaultDex = 0;

        public Manager.GameBuffManager GameBuffManager { get; private set; }

        public Monster()
        {
            Distance = new Int32ValueObject(m_defaultDistance);
            GameBuffManager = new Manager.GameBuffManager();
        }

        public int GetAttack()
        {
            return m_defaultAttack;
        }

        public int GetDefence()
        {
            return m_defaultDefence;
        }

        public int GetDex()
        {
            return m_defaultDex;
        }
    }
}
