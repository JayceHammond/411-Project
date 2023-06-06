using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DiceController : MonoBehaviour
{
    public GameObject twentySided;
    public float rollSpeed;
    public TextMeshProUGUI displayText;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        displayText.text = SideChecker.DisplayText();
        if(Input.GetKeyDown(KeyCode.K)){
            GameObject roll = Instantiate(twentySided, this.transform);
            roll.GetComponent<Rigidbody>().velocity = new Vector3(roll.GetComponent<Rigidbody>().velocity.x + Random.Range(-20, 20), roll.GetComponent<Rigidbody>().velocity.y, 
            roll.GetComponent<Rigidbody>().velocity.z + Random.Range(20, 40));
            roll.GetComponent<Rigidbody>().AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
            Destroy(roll, 10);
        }
    }
}
