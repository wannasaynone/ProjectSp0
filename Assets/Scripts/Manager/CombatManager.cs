using System.Collections.Generic;
using KahaGameCore.Interface;
using System;

namespace ProjectSP0.Manager
{
    public class CombatManager : KahaGameCore.Interface.Manager
    {
        public static CombatManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new CombatManager();
                }
                return m_instance;
            }
        }
        private static CombatManager m_instance = null;
        private CombatManager() { }

        public Action OnCombatPreStart = null;
        public Action<ICombatUnit> OnTurnPreStart = null;
        public Action<ICombatUnit> OnAttackPreStart = null;
        public Action<ICombatUnit> OnAttacked = null;
        public Action<ICombatUnit> OnTurnEnded = null;
        public Action OnCombatEnded = null;

        private List<ICombatUnit> m_allUnits = null;
        private List<Monster> m_allEnemies = null;
        private Character m_player = null;

        private int m_currentTurnIndex = 0;
        private UIStateBase m_currentCombatState = null;

        private int m_ap = 0;

        public void StartCombat(Character player, List<Monster> enemy)
        {
            m_allUnits = new List<ICombatUnit>();
            m_allEnemies = new List<Monster>();

            m_player = player;
            m_allUnits.Add(player);

            m_allEnemies = enemy;
            m_allUnits.AddRange(m_allEnemies);

            OnCombatPreStart?.Invoke();

            StartNewRound();
        }

        public void Attack(ICombatUnit target)
        {
            ICombatUnit _current = m_allUnits[m_currentTurnIndex];
            if (_current is Monster)
            {
                Monster _curentMonster = _current as Monster;
                if (!(_curentMonster.Distance.Value >= _curentMonster.MinAttackDistance
                    && _curentMonster.Distance.Value <= _curentMonster.MaxAttackDistance))
                {
                    UnityEngine.Debug.Log("Can't Attack");
                    return;
                }
            }
            else if (_current is Character)
            {
                Monster _targetMonster = target as Monster;
                Character _currentCharacter = _current as Character;
                if (!(_targetMonster.Distance.Value >= _currentCharacter.GetMinAttackDistance()
                    && _targetMonster.Distance.Value <= _currentCharacter.GetMaxAttackDistance()))
                {
                    UnityEngine.Debug.Log("Can't Attack");
                    return;
                }
            }

            OnAttackPreStart?.Invoke(_current);
            Combat.AttackState _attackState = new Combat.AttackState(new Combat.AttackState.AttackInfo()
            {
                attacker = _current,
                defender = target
            });
            _attackState.OnEnded += OnAttackEnded;

            ChangeTo(_attackState);
        }

        private void OnAttackEnded()
        {
            m_currentCombatState.OnEnded -= OnAttackEnded;
            OnAttacked?.Invoke(m_allUnits[m_currentTurnIndex]);
            m_ap--;
            if (m_ap > 0)
            {
                GoBack();
            }
            else
            {
                EndTurn();
            }
        }

        public void MoveCharatcer(int distance)
        {
            for (int i = 0; i < m_allEnemies.Count; i++)
            {
                if (m_allEnemies[i].Distance.Value - distance >= 0)
                {
                    m_allEnemies[i].Distance.Value -= distance;
                }
            }
            UnityEngine.Debug.LogFormat("{0} moved {1} distance", m_allUnits[m_currentTurnIndex].GetName(), distance);
            GetPage<UI.CombatUIPage>().RefreshEnemyIcon(m_allEnemies);
            m_ap--;
            if (m_ap > 0)
            {
                GoBack();
            }
            else
            {
                EndTurn();
            }
        }

        public void MoveMonster(int distance)
        {
            ((Monster)m_allUnits[m_currentTurnIndex]).Distance.Value += distance;
            UnityEngine.Debug.LogFormat("{0} moved {1} distance", m_allUnits[m_currentTurnIndex].GetName(), distance);

            GetPage<UI.CombatUIPage>().RefreshEnemyIcon(m_allEnemies);
            m_ap--;
            if (m_ap > 0)
            {
                GoBack();
            }
            else
            {
                EndTurn();
            }
        }

        private void GoBack()
        {
            if (m_allUnits[m_currentTurnIndex] is Monster)
            {
                ChangeTo(new Combat.MonsterState(m_allUnits[m_currentTurnIndex] as Monster));
            }

            if (m_allUnits[m_currentTurnIndex] is Character)
            {
                ChangeTo(new Combat.CharacterState());
            }
        }

        private void StartNewRound()
        {
            UnityEngine.Debug.Log("New Round Start");

            m_allUnits.Sort((x, y) => y.GetDex().CompareTo(x.GetDex()));
            m_currentTurnIndex = 0;
            StartTurn();
        }

        private void StartTurn()
        {
            UnityEngine.Debug.Log("==============================");
            GetPage<UI.CombatUIPage>().RefreshEnemyIcon(m_allEnemies);

            OnTurnPreStart?.Invoke(m_allUnits[m_currentTurnIndex]);

            if (m_allUnits[m_currentTurnIndex] is Monster)
            {
                UnityEngine.Debug.LogFormat("Monster {0}'s turn", m_allUnits[m_currentTurnIndex].GetName());
                m_ap = 1;
                ChangeTo(new Combat.MonsterState(m_allUnits[m_currentTurnIndex] as Monster));
            }
            
            if(m_allUnits[m_currentTurnIndex] is Character)
            {
                UnityEngine.Debug.LogFormat("Player {0}'s turn", m_allUnits[m_currentTurnIndex].GetName());
                m_ap = 3;
                ChangeTo(new Combat.CharacterState());
            }
        }

        private void EndTurn()
        {
            OnTurnEnded?.Invoke(m_allUnits[m_currentTurnIndex]);
            m_currentTurnIndex++;
            if (m_currentTurnIndex >= m_allUnits.Count)
            {
                StartNewRound();
            }
            else
            {
                StartTurn();
            }
        }

        private void ChangeTo(UIStateBase nextState)
        {
            m_currentCombatState?.Stop();
            m_currentCombatState = nextState;
            m_currentCombatState?.Start();
        }
    }
}
