using System;

namespace ProjectSP0.Combat
{
    public class AttackProcesser
    {
        public class AttackInfo
        {
            public ICombatUnit attacker = null;
            public ICombatUnit defender = null;
            public Action onAttackEnded = null;
        }

        public void Start(AttackInfo attackInfo)
        {
            UnityEngine.Debug.LogFormat("Attacker {0} start attack {1}",
                             attackInfo.attacker.GetName(),
                             attackInfo.defender.GetName());

            int _attack = attackInfo.attacker.GetAttack() + UnityEngine.Random.Range(0, attackInfo.attacker.GetAttack() + 1);
            int _defence = attackInfo.defender.GetDefence() + UnityEngine.Random.Range(0, attackInfo.defender.GetDefence() + 1);
            int _dmg = _attack - _defence;
            _dmg = _dmg < 0 ? 0 : _dmg;
            UnityEngine.Debug.Log("Dmg=" + _dmg);
            attackInfo.defender.HP.Value -= _dmg;

            KahaGameCore.Static.TimerManager.Schedule(0.5f, attackInfo.onAttackEnded);
        }
    }
}
