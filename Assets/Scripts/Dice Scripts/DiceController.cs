using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DiceController : MonoBehaviour
{
    public GameObject twentySided;
    public float rollSpeed;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            GameObject roll = Instantiate(twentySided, this.transform);
            roll.GetComponent<Rigidbody>().velocity = new Vector3(roll.GetComponent<Rigidbody>().velocity.x + Random.Range(-20, 20), roll.GetComponent<Rigidbody>().velocity.y, 
            roll.GetComponent<Rigidbody>().velocity.z + Random.Range(20, 40));
            roll.GetComponent<Rigidbody>().AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
            Destroy(roll, 10);
        }
    }
}