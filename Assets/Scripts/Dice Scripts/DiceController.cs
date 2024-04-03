using UnityEngine;
using TMPro;
using Mirror;
using UnityEngine.SceneManagement;
using Org.BouncyCastle.Asn1.X509;


public class DiceController : NetworkBehaviour
{
    public static int dieVal = 0;
    public static GameObject twentySided;
    public GameObject objD20;
    public float rollSpeed;
    public static TextMeshProUGUI displayText;
    public TextMeshProUGUI displayTextOBJ;
    // Start is called before the first frame update

    void Awake()
    {
        twentySided = objD20;
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MultiplayerTest" && displayTextOBJ == null){
            displayTextOBJ = GameObject.Find("RollText").GetComponent<TextMeshProUGUI>();
            displayText = displayTextOBJ;
        }
    }

    public int rollDie(GameObject dieToRoll,Transform transform){
        GameObject roll = Instantiate(dieToRoll, new Vector3(transform.position.x, transform.position.y, transform.position.z + 2), transform.rotation);
        roll.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(10, 40), ForceMode.Impulse);
        roll.GetComponent<Rigidbody>().AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        Destroy(roll, 10);
        return SideChecker.sharedSideVal;
    }



}
