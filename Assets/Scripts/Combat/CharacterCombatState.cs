using System;
using System.Collections.Generic;

namespace ProjectSP0.Combat
{
    public class CharacterCombatState : CombatStateBase
    {
        private Character m_actor = null;
        private List<Monster> m_allEnemies = null;
        private Action m_onTurnEnded = null;

        public CharacterCombatState(Character actor, List<Monster> allEnemies)
        {
            m_actor = actor;
            m_allEnemies = allEnemies;
        }

        public override void Attack(ICombatUnit target, Action onAttackEnded)
        {
            GetPage<UI.CombatUIPage>().allowPlayerInput = false;

            Monster _targetMonster = target as Monster;
            if (!(_targetMonster.Distance.Value >= m_actor.GetMinAttackDistance()
                && _targetMonster.Distance.Value <= m_actor.GetMaxAttackDistance())
                || target.HP.Value <= 0)
            {
                UnityEngine.Debug.Log("Can't Attack");
                WaitPlayerCommand();
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
            GetPage<UI.CombatUIPage>().allowPlayerInput = false;

            for (int i = 0; i < m_allEnemies.Count; i++)
            {
                if (m_allEnemies[i].Distance.Value - distance >= 0)
                {
                    m_allEnemies[i].Distance.Value -= distance;
                }
            }
            UnityEngine.Debug.LogFormat("{0} moved {1} distance", m_actor.GetName(), distance);
            GetPage<UI.CombatUIPage>().RefreshEnemyIcon(m_allEnemies);
            DecreaseAP();
        }

        public override void StartTurn(Action onTurnEnded)
        {
            UnityEngine.Debug.LogFormat("Player {0}'s turn", m_actor.GetName());
            CurrentAP = 3;
            UnityEngine.Debug.Log("AP=" + CurrentAP);
            m_onTurnEnded = onTurnEnded;
            WaitPlayerCommand();
        }

        private void WaitPlayerCommand()
        {
            UnityEngine.Debug.Log("Waiting Player Command...");
            GetPage<UI.CombatUIPage>().allowPlayerInput = true;
        }

        private void DecreaseAP()
        {
            if(m_actor.HP.Value <= 0)
            {
                m_onTurnEnded?.Invoke();
                return;
            }

            CurrentAP--;
            UnityEngine.Debug.Log("AP=" + CurrentAP);
            if (CurrentAP <= 0)
            {
                m_onTurnEnded?.Invoke();
            }
            else
            {
                WaitPlayerCommand();
            }
        }
    }
}
