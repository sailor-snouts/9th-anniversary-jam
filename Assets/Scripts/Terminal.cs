using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;

public class Terminal : MonoBehaviour
{
    [SerializeField] private Turret turret = null;

    public void Enable(PlayerController player)
    {
        this.turret.Equip(player);
    }
}
