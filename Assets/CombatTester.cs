using UnityEngine;

using ProjectSP0;
using ProjectSP0.Manager;

using System.Collections.Generic;

public class CombatTester : MonoBehaviour
{
    [SerializeField] Monster[] monsters = null;

    private void Start()
    {
        CombatManager.Instance.StartCombat(GameManager.Instance.CurrentCharacter, new List<Monster>(monsters));
    }
}
