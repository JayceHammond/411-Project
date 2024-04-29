using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBoltCard : CardData
{
    void Awake()
    {
        Initialize("Lightning bolt", 1, "Shoot a bolt of lightining", 3, "Lightining", "2d6 damage");
    }
}
