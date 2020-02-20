using UnityEngine;
using KahaGameCore.Static;

namespace ProjectSP0
{
    [CreateAssetMenu(menuName = "Game Data/Game Properties")]
    public class GameProperties : ScriptableObject
    {
        public static GameProperties Instance
        {
            get
            {
                if(m_instance == null)
                {
                    m_instance = GameResourcesManager.LoadResource<GameProperties>("GameProperties");
                }
                return m_instance;
            }
        }
        private static GameProperties m_instance = null;

        public int BaseAttack { get { return m_baseAttack; } }
        public int BaseDefence { get { return m_baseDefence; } }
        public int BaseDex { get { return m_baseDex; } }

        [Header("Character")]
        [SerializeField] private int m_baseAttack = 10;
        [SerializeField] private int m_baseDefence = 0;
        [SerializeField] private int m_baseDex = 10;
    }
}


