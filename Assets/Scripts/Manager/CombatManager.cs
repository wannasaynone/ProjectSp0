using System.Collections.Generic;
using KahaGameCore.Interface;

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

        public List<ICombatUnit> AllUnits { get { return new List<ICombatUnit>(m_allUnits); } }
        public List<Monster> AllEnemies { get { return new List<Monster>(m_allEnemies); } }
        public Character Player { get { return m_player; } }

        private List<ICombatUnit> m_allUnits = null;
        private List<Monster> m_allEnemies = null;
        private Character m_player = null;

        private int m_currentTurnIndex = 0;
        private StateBase m_currentCombatState = null;

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

        private void StartNewRound()
        {
            m_allUnits.Sort((x, y) => y.GetDex().CompareTo(x.GetDex()));
            m_currentTurnIndex = 0;
            StartTurn();
        }

        private void StartTurn()
        {
            if(m_allUnits[m_currentTurnIndex] is Monster)
            {
                UnityEngine.Debug.Log("Is Monster");
            }
            
            if(m_allUnits[m_currentTurnIndex] is Character)
            {
                m_currentCombatState = new Combat.CharacterState();
                m_currentCombatState.OnEnded += EndTurn;
                m_currentCombatState.Start();
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
    }
}
