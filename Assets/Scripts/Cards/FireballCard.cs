using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FireballCard : CardData
{
    void Awake(){
    Initialize("Fireball", 1, "Launch a fiery projectile", 3, "Fire", "2d6 damage");
    }
}
