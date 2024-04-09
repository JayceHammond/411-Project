using UnityEngine;
using TMPro;
using Mirror;
using UnityEngine.SceneManagement;


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
        System.Guid dieAssetId = System.Guid.NewGuid();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MultiplayerTest" && displayTextOBJ == null){
            displayTextOBJ = GameObject.Find("RollText").GetComponent<TextMeshProUGUI>();
            displayText = displayTextOBJ;
        }
    }

    [Command]
    public void rollDie(GameObject dieToRoll,Transform transform){
        GameObject die = Instantiate(dieToRoll, new Vector3(transform.position.x, transform.position.y, transform.position.z + 2), transform.rotation);
        NetworkServer.Spawn(die);
        die.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(10, 40), ForceMode.Impulse);
        die.GetComponent<Rigidbody>().AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        
        Destroy(die, 30);
        //return SideChecker.sharedSideVal;
    }

    [Server]
        public void HostRollDie(GameObject dieToRoll,Transform transform){
        GameObject die = Instantiate(dieToRoll, new Vector3(transform.position.x, transform.position.y, transform.position.z + 2), transform.rotation);
        NetworkServer.Spawn(die);
        die.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(10, 40), ForceMode.Impulse);
        die.GetComponent<Rigidbody>().AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        
        Destroy(die, 30);
        //return SideChecker.sharedSideVal;
    }


}
