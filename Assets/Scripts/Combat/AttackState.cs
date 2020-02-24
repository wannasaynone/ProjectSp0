using KahaGameCore.Interface;

namespace ProjectSP0.Combat
{
    public class AttackState : UIStateBase
    {
        public class AttackInfo
        {
            public ICombatUnit attacker = null;
            public ICombatUnit defender = null;
        }

        private readonly AttackInfo m_attackInfo = null;

        public AttackState(AttackInfo attackInfo)
        {
            m_attackInfo = attackInfo;
        }
        protected override void OnStart()
        {
            UnityEngine.Debug.LogFormat("Attacker {0} start attack {1}",
                m_attackInfo.attacker.GetName(),
                m_attackInfo.defender.GetName());
        }

        protected override void OnStop()
        {
            throw new System.NotImplementedException();
        }
    }
}
