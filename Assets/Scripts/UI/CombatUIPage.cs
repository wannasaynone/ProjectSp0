using UnityEngine;
using KahaGameCore.Interface;
using KahaGameCore.Static;
using System;
using System.Collections.Generic;

namespace ProjectSP0.UI
{
    public class CombatUIPage : UIView
    {
        public bool allowPlayerInput = false;

        [SerializeField] private MonsterIcon m_monsterIconSource = null;

        private List<MonsterIcon> m_clonedMonsterIcons = new List<MonsterIcon>();
        private Monster m_currentSelectedMonster = null;

        public override bool IsShowing => throw new NotImplementedException();

        public override void ForceShow(KahaGameCore.Interface.Manager manager, bool show)
        {
            throw new NotImplementedException();
        }

        public override void Show(KahaGameCore.Interface.Manager manager, bool show, Action onCompleted)
        {
            throw new NotImplementedException();
        }

        public void RefreshEnemyIcon(List<Monster> monsters)
        {
            for (int i = 0; i < m_clonedMonsterIcons.Count; i++)
            {
                m_clonedMonsterIcons[i].OnSelected -= SelectMonster;
                GameObjectPoolManager.Recycle(m_clonedMonsterIcons[i]);
            }

            for(int i = 0; i < monsters.Count; i++)
            {
                MonsterIcon _cloneIcon = GameObjectPoolManager.GetInstance<MonsterIcon>(m_monsterIconSource);
                _cloneIcon.LinkMonster(monsters[i]);
                _cloneIcon.OnSelected += SelectMonster;
                m_clonedMonsterIcons.Add(_cloneIcon);
            }
        }

        private void SelectMonster(Monster monster)
        {
            m_currentSelectedMonster = monster;
            Debug.LogFormat("Select Monster {0}: Distance={1}", monster.GetName(), monster.Distance.Value);
        }

        // Testing
        private void Update()
        {
            if(!allowPlayerInput)
            {
                return;
            }

            if(Input.GetKeyDown(KeyCode.I))
            {
                Manager.CombatManager.Instance.Debug_ShowAllMonsterDistanceInfo();
            }

            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                m_clonedMonsterIcons[0].Button_Select();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                m_clonedMonsterIcons[1].Button_Select();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                m_clonedMonsterIcons[2].Button_Select();
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                m_clonedMonsterIcons[3].Button_Select();
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                m_clonedMonsterIcons[4].Button_Select();
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                Manager.CombatManager.Instance.Attack(m_currentSelectedMonster);
            }

            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Manager.CombatManager.Instance.Move(-1);
                if(m_currentSelectedMonster != null)
                {
                    Debug.LogFormat("Select Monster {0}: Distance={1}", m_currentSelectedMonster.GetName(), m_currentSelectedMonster.Distance.Value);
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Manager.CombatManager.Instance.Move(1);
                if (m_currentSelectedMonster != null)
                {
                    Debug.LogFormat("Select Monster {0}: Distance={1}", m_currentSelectedMonster.GetName(), m_currentSelectedMonster.Distance.Value);
                }
            }
        }
    }
}
