using System;
using UnityEngine;

namespace ProjectSP0.AI
{
    [CreateAssetMenu(menuName = "Game Data/AI/SimpleAIBehaviour")]
    public class SimpleAIBehaviour : AIBehaviourBase
    {
        public override void Do(Monster monster)
        {
            if(monster.Distance.Value > monster.MaxAttackDistance)
            {
                Manager.CombatManager.Instance.MoveMonster(-1);
            }
            else if(monster.Distance.Value < monster.MinAttackDistance)
            {
                Manager.CombatManager.Instance.MoveMonster(1);
            }
            else
            {
                Manager.CombatManager.Instance.Attack(Manager.GameManager.Instance.CurrentCharacter);
            }
        }
    }
}
