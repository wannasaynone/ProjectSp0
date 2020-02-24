using UnityEngine;

namespace ProjectSP0.Combat
{
    public class CharacterState : KahaGameCore.Interface.UIStateBase
    {
        protected override void OnStart()
        {
            Debug.Log("Character " + Manager.GameManager.Instance.CurrentCharacter.GetName() + " Turn Started");
            Debug.Log("Number To Select Monster");
            Debug.Log("Space To Attack Selected Monster");
        }

        protected override void OnStop()
        {
            Debug.Log("CharacterState End");
        }
    }
}
