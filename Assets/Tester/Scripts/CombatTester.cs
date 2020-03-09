using UnityEngine;

using ProjectSP0;
using ProjectSP0.Manager;

using System.Collections.Generic;

public class CombatTester : MonoBehaviour
{
    [SerializeField] MonsterRawData[] monsterDatas = null;

    private void Start()
    {
        List<Monster> _monsters = new List<Monster>();
        for(int i = 0; i < monsterDatas.Length; i++)
        {
            _monsters.Add(new Monster(monsterDatas[i], Random.Range(1, 6)));
        }
        CombatManager.Instance.StartCombat(GameManager.Instance.CurrentCharacter, _monsters);
    }
}
