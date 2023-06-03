using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideChecker : MonoBehaviour
{
    // Start is called before the first frame update
    public int sideVal; //Must always be opposite of the side collider is on: i.e 20 -> 1, 19 -> 2, 18 -> 3 etc
    private float timer = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void OnTriggerStay(Collider col) {
        GameObject obj = col.gameObject;
        timer += Time.deltaTime;
        if(obj.tag == "Tray" && timer > 3){
            Debug.Log(sideVal);
            timer = 0;
        }
    }
}
