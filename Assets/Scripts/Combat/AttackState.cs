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

            int _attack = m_attackInfo.attacker.GetAttack() + UnityEngine.Random.Range(0, m_attackInfo.attacker.GetAttack() + 1);
            int _defence = m_attackInfo.defender.GetDefence() + UnityEngine.Random.Range(0, m_attackInfo.attacker.GetDefence() + 1);
            int _dmg = _attack - _defence;
            _dmg = _dmg < 0 ? 0 : _dmg;
            UnityEngine.Debug.Log("Dmg=" + _dmg);
            m_attackInfo.defender.HP.Value -= _dmg;

            Stop();
        }

        protected override void OnStop()
        {
            UnityEngine.Debug.Log("Attack End");
        }
    }
}
