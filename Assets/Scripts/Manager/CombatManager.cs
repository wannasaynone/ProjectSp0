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
        public Action<ICombatUnit> OnDamaged = null;
        public Action<ICombatUnit> OnAttacked = null;
        public Action<ICombatUnit> OnTurnEnded = null;
        public Action OnCombatEnded = null;

        private List<ICombatUnit> m_allUnits = null;
        private List<Monster> m_allEnemies = null;
        private Character m_player = null;

        private int m_currentTurnIndex = 0;
        private UIStateBase m_currentCombatState = null;

        public void StartCombat(Character player, List<Monster> enemy)
        {
            m_allUnits = new List<ICombatUnit>();
            m_allEnemies = new List<Monster>();

            m_player = player;
            m_allUnits.Add(player);

            m_allEnemies = enemy;
            m_allUnits.AddRange(m_allEnemies);

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

            ChangeTo(new Combat.AttackState(new Combat.AttackState.AttackInfo()
            {
                attacker = _current,
                defender = target
            }));
        }

        public void MoveCharatcer(int distance)
        {
            for(int i = 0; i < m_allEnemies.Count; i++)
            {
                m_allEnemies[i].Distance.Value -= distance;
            }
            GetPage<UI.CombatUIPage>().RefreshEnemyIcon(m_allEnemies);
        }

        private void StartNewRound()
        {
            UnityEngine.Debug.Log("Combat Start");
            UnityEngine.Debug.Log("==============================");

            m_allUnits.Sort((x, y) => y.GetDex().CompareTo(x.GetDex()));
            m_currentTurnIndex = 0;
            StartTurn();
        }

        private void StartTurn()
        {
            GetPage<UI.CombatUIPage>().RefreshEnemyIcon(m_allEnemies);
            if (m_allUnits[m_currentTurnIndex] is MonsterData)
            {
                UnityEngine.Debug.Log("Is Monster");
            }
            
            if(m_allUnits[m_currentTurnIndex] is Character)
            {
                ChangeTo(new Combat.CharacterState());
            }
        }

        private void EndTurn()
        {
            m_currentCombatState.OnEnded -= EndTurn;

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
            m_currentCombatState.Start();
        }
    }
}
