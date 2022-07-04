using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : Interactable
{
    [SerializeField]
    private float healTimeDuration;

    [SerializeField]
    private int healAmount;

    private float _healTimer;

    public void CampFireHeal()
    {
        if(_healTimer > 0)
        {
            _healTimer -= Time.deltaTime * 1;
        }
        else
        {
            Player.Heal(healAmount);
            _healTimer = healTimeDuration;
        }
    }
}
