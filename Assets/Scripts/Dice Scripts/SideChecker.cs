using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideChecker : MonoBehaviour
{
    // Start is called before the first frame update
    public static string pubSideVal; //Must always be opposite of the side collider is on: i.e 20 -> 1, 19 -> 2, 18 -> 3 etc
    public static int sideVal;
    private float timer = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string DisplayText(){
        return pubSideVal;
    }

    private void OnTriggerStay(Collider col) {
        GameObject obj = col.gameObject;
        timer += Time.deltaTime;
        if(obj.tag == "Tray" && timer > 3){
            pubSideVal = sideVal.ToString();
            timer = 0;
        }else if(obj.tag != "Tray" && timer > 3){
            pubSideVal = "reroll";
            timer = 0;
        }
    }
}
