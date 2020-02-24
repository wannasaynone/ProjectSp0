using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSP0.Combat
{
    public class MonsterState : KahaGameCore.Interface.UIStateBase
    {
        private readonly Monster m_monster = null;

        public MonsterState(Monster monster)
        {
            m_monster = monster;
        }

        protected override void OnStart()
        {
            Debug.Log("Monster " + m_monster.GetName() + " Start Acting");
            m_monster.AIBehaviour.Start();
        }

        protected override void OnStop()
        {
            Debug.Log("MonsterState End");
        }
    }
}

