using System;
using UnityEngine;

namespace ProjectSP0.GameCommand
{
    [CreateAssetMenu(menuName = "Game Data/Game Command/Debug Log")]
    public class GameCommand_DebugLog : GameCommandBase
    {
        [SerializeField] private string m_message = "";

        public override void Process(Action onComplete)
        {
            Debug.Log(m_message);
            onComplete?.Invoke();
        }
    }
}
