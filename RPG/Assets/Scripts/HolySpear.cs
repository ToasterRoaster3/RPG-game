using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolySpear : Weapon
{
    [SerializeField] int holyStrength = 6;
    public Player player;
    
    public override void ApplyEffect(Character target)
    {
        Debug.Log(target.name + " holied for " + holyStrength);
        
        if (player.health <= 10)
        {
            target.GetHit(holyStrength);
        }
    }
}
