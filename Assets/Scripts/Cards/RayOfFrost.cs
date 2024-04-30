using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayOfFrost : CardData
{
    void Awake()
    {
        Initialize("Ray Of Frost", 1, "A frigid beam of blue-white streaks towards a creature within range.", 3, "Ice", "2d6 damage");
    }
}
