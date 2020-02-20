using System;
using UnityEngine;

namespace ProjectSP0.GameCommand
{
    [CreateAssetMenu(menuName = "Game Data/Game Command/Add HP")]
    public class GameCommand_AddHP : GameCommandBase
    {
        public override void Process(Action onComplete)
        {
            Manager.GameManager.Instance.CurrentCharacter.HP.Value += 100;
        }
    }
}
