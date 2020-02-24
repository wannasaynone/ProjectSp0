using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSP0
{
    public class Monster : ICombatUnit
    {
        public Int32ValueObject Distance { get; private set; }
        public Int32ValueObject HP { get; private set; }
        public int MinAttackDistance { get { return m_defaultMinAttackDistance; } }
        public int MaxAttackDistance { get { return m_defaultMaxAttackDistance; } }

        public Manager.GameBuffManager GameBuffManager { get; private set; }

        private string m_name = "";
        private int m_defaultAttack = 0;
        private int m_defaultDefence = 0;
        private int m_defaultDex = 0;
        private int m_defaultMinAttackDistance = 0;
        private int m_defaultMaxAttackDistance = 0;

        public Monster(MonsterData monsterData, int distance)
        {
            Distance = new Int32ValueObject(distance);
            HP = new Int32ValueObject(monsterData.DefaultHP);
            m_defaultDefence = monsterData.DefaultDefence;
            m_defaultAttack = monsterData.DefaultAttack;
            m_defaultDex = monsterData.DefaultDex;
            m_defaultMinAttackDistance = monsterData.DefaultMinAttackDistance;
            m_defaultMaxAttackDistance = monsterData.DefaultMaxAttackDistance;
            m_name = monsterData.MonsterName;
            GameBuffManager = new Manager.GameBuffManager(this);
            if(monsterData.DefaultBuffs != null && monsterData.DefaultBuffs.Length != 0)
            {
                for(int i = 0; i < monsterData.DefaultBuffs.Length; i++)
                {
                    GameBuffManager.AddBuff(monsterData.DefaultBuffs[i]);
                }
            }
        }

        public string GetName()
        {
            return m_name;
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
