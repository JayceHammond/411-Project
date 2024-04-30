using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayOfSickness : CardData
{
    void Awake()
    {
        Initialize("Ray Of Sickness", 1, "A ray of sickening greenish energy lashes out toward a creature within range.", 3, "Poision", "2d8 damage");
    }
}
