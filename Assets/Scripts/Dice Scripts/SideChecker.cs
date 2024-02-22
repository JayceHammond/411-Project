using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SideChecker : MonoBehaviour
{
    // Start is called before the first frame update
    public static string pubSideVal; //Must always be opposite of the side collider is on: i.e 20 -> 1, 19 -> 2, 18 -> 3 etc
    public int sideVal;
    public static int sharedSideVal;
    private static float timer = 0;
    public static bool allowedToCalculate;
    private int collisionCount = 0;
    public bool done;
    void Start()
    {
        
    }

    void Update(){
        if(IsNotColliding && CompareTag("D4") && timer > 6){
            Debug.Log("I rolled " + sideVal);
            pubSideVal = sideVal.ToString();
            timer = 0;
        }
    }


    public bool IsNotColliding{
        get {return collisionCount == 0;}
    }

    public static string DisplayText(){
        return pubSideVal;
    }

    private void OnTriggerEnter(Collider col){
        collisionCount++;
    }

    private void OnTriggerExit(Collider col){
        collisionCount--;
    }
    private void OnTriggerStay(Collider col) {
        GameObject obj = col.gameObject;
        timer += Time.deltaTime;

        if(CompareTag("D20"))
        {
            Debug.Log("Rolled D20");
            if(obj.CompareTag("Tray") && timer > 3){
                pubSideVal = sideVal.ToString();
                allowedToCalculate = true;
                sharedSideVal = sideVal;
                timer = 0;
            }else if(!obj.CompareTag("Tray") && timer > 3){
                pubSideVal = "reroll";
                timer = 0;
            }
        }
    }

    public static bool isDone(){
        return allowedToCalculate;
    }
}
