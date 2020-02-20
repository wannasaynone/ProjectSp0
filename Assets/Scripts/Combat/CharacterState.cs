using UnityEngine;
using System.Collections.Generic;

namespace ProjectSP0.Combat
{
    public class CharacterState : KahaGameCore.Interface.StateBase
    {
        private List<Monster> m_allEnemies = null;
        private Monster m_currentSelected = null;

        protected override void OnStart()
        {
            m_allEnemies = Manager.CombatManager.Instance.AllEnemies;

            Debug.Log("Is Character");
            Debug.Log("Number To Select Monster");
            Debug.Log("A To Attack Selected Monster");
        }

        protected override void OnStop()
        {
            Debug.Log("Character Turn End");
        }

        protected override void OnTick()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                m_currentSelected = m_allEnemies[0];
                Debug.Log("Select Monster 0");
            }

            if(Input.GetKeyDown(KeyCode.A))
            {
                if(m_currentSelected == null)
                {
                    Debug.Log("Select Monster first");
                    return;
                }

                Debug.Log("Character attack Monster 0 (test log)");

                Stop();
            }
        }
    }
}
