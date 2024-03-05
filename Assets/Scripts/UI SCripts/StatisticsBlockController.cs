using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsBlockController : MonoBehaviour
{
    public CharacterSheet selectedChar;
    public TextMeshProUGUI StrText;
    public TextMeshProUGUI DexText;
    public TextMeshProUGUI ConText;
    public TextMeshProUGUI IntText;
    public TextMeshProUGUI WisText;
    public TextMeshProUGUI ChaText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StrText.text = selectedChar.STR.ToString();
        DexText.text = selectedChar.DEX.ToString();
        ConText.text = selectedChar.CON.ToString();
        IntText.text = selectedChar.INT.ToString();
        WisText.text = selectedChar.WIS.ToString();
        ChaText.text = selectedChar.CHAR.ToString();
        
    }
}
