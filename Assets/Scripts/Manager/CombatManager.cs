using System.Collections.Generic;
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

        public event Action OnCombatPreStart = null;
        public event Action<ICombatUnit> OnTurnPreStart = null;
        public event Action<ICombatUnit> OnAttackPreStart = null;
        public event Action<ICombatUnit> OnAttacked = null;
        public event Action<ICombatUnit> OnCombatUnitDied = null;
        public event Action<ICombatUnit> OnTurnEnded = null;
        public event Action OnCombatEnded = null;

        public int CurrentAP { get { return m_currentState == null ? -1 : m_currentState.CurrentAP; } }

        private List<ICombatUnit> m_allUnits = null;
        private List<Monster> m_allEnemies = null;
        private Character m_player = null;

        private int m_currentTurnIndex = 0;

        private Combat.CombatStateBase m_currentState = null;

        public void StartCombat(Character player, List<Monster> enemy)
        {
            m_allUnits = new List<ICombatUnit>();
            m_allEnemies = new List<Monster>();

            m_player = player;
            m_allUnits.Add(player);

            m_allEnemies = enemy;
            m_allUnits.AddRange(m_allEnemies);

            for(int i = 0; i < m_allUnits.Count; i++)
            {
                m_allUnits[i].OnDied += OnUnitDied;
            }

            OnCombatPreStart?.Invoke();

            StartNewRound();
        }

        public void Debug_ShowAllMonsterDistanceInfo()
        {
            UnityEngine.Debug.Log("==============================");
            for (int i = 0; i < m_allEnemies.Count; i++)
            {
                UnityEngine.Debug.LogFormat("---Monster {0}: Distance={1}", m_allEnemies[i].GetName(), m_allEnemies[i].Distance.Value);
            }
            UnityEngine.Debug.Log("==============================");
        }

        public void Attack(ICombatUnit target)
        {
            if(target == null)
            {
                UnityEngine.Debug.Log("Select Target First");
                return;
            }

            OnAttackPreStart?.Invoke(m_allUnits[m_currentTurnIndex]);
            m_currentState.Attack(target, OnAttackEnded);
        }

        private void OnAttackEnded()
        {
            OnAttacked?.Invoke(m_allUnits[m_currentTurnIndex]);
        }

        public void Move(int distance)
        {
            m_currentState.Move(distance);
        }

        private void OnUnitDied(ICombatUnit unit)
        {
            UnityEngine.Debug.Log(unit.GetName() + " died");
            OnCombatUnitDied?.Invoke(unit);
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
            m_currentState = GetCurrentState();
            m_currentState.StartTurn(EndTurn);
        }

        private Combat.CombatStateBase GetCurrentState()
        {
            if (m_allUnits[m_currentTurnIndex] is Character)
            {
                return new Combat.CharacterCombatState(m_player, m_allEnemies);
            }
            else if (m_allUnits[m_currentTurnIndex] is Monster)
            {
                return new Combat.MonsterCombatState(m_allUnits[m_currentTurnIndex] as Monster, m_allEnemies);
            }

            return null;
        }

        private void EndTurn()
        {
            UnityEngine.Debug.Log("End Turn");
            OnTurnEnded?.Invoke(m_allUnits[m_currentTurnIndex]);

            if (m_player.HP.Value <= 0)
            {
                UnityEngine.Debug.Log("Combat End: Player Died");
                EndCombat();
                return;
            }

            for (int i = 0; i < m_allEnemies.Count; i++)
            {
                if(m_allEnemies[i].HP.Value > 0)
                {
                    break;
                }

                if(i == m_allEnemies.Count - 1)
                {
                    UnityEngine.Debug.Log("Combat End: Player Win");
                    EndCombat();
                    return;
                }
            }

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

        private void EndCombat()
        {
            for (int i = 0; i < m_allUnits.Count; i++)
            {
                m_allUnits[i].OnDied -= OnUnitDied;
            }

            OnCombatEnded?.Invoke();
        }
    }
}
