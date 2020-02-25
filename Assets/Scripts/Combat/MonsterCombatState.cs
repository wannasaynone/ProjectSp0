using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSP0.Combat
{
    public class MonsterCombatState : CombatStateBase
    {
        private Monster m_actor = null;
        private List<Monster> m_allEnemies = null;
        private Action m_onTurnEnded = null;

        public MonsterCombatState(Monster actor, List<Monster> allEnemies)
        {
            m_actor = actor;
            m_allEnemies = allEnemies;
        }

        public override void Attack(ICombatUnit target, Action onAttackEnded)
        {
            if (!(m_actor.Distance.Value >= m_actor.MinAttackDistance
                && m_actor.Distance.Value <= m_actor.MaxAttackDistance))
            {
                Debug.Log("Can't Attack");
                return;
            }

            onAttackEnded += DecreaseAP;
            new AttackProcesser().Start(new AttackProcesser.AttackInfo
            {
                attacker = m_actor,
                defender = target,
                onAttackEnded = onAttackEnded
            });
        }

        public override void Move(int distance)
        {
            m_actor.Distance.Value += distance;
            Debug.LogFormat("{0} moved {1} distance", m_actor.GetName(), distance);

            GetPage<UI.CombatUIPage>().RefreshEnemyIcon(m_allEnemies);
            DecreaseAP();
        }

        public override void StartTurn(Action onTurnEnded)
        {
            Debug.LogFormat("Monster {0}'s turn", m_actor.GetName());
            m_onTurnEnded = onTurnEnded;
            CurrentAP = 1;
            Debug.Log("AP=" + CurrentAP);
            m_actor.AIBehaviour.Start();
        }

        private void DecreaseAP()
        {
            CurrentAP--;
            Debug.Log("AP=" + CurrentAP);
            if (CurrentAP <= 0)
            {
                m_onTurnEnded?.Invoke();
            }
            else
            {
                m_actor.AIBehaviour.Start();
            }
        }
    }
}
