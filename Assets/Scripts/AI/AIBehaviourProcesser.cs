namespace ProjectSP0.AI
{
    public class AIBehaviourProcesser
    {
        private readonly Monster m_monster = null;
        private readonly AIBehaviourBase m_behaviour = null;

        public AIBehaviourProcesser(Monster monster, AIBehaviourBase behaviour)
        {
            m_monster = monster;
            m_behaviour = behaviour;
        }

        public void Start()
        {
            UnityEngine.Debug.Log("Monster " + m_monster.GetName() + " start AI");
            m_behaviour.Do(m_monster);
        }
    }
}
