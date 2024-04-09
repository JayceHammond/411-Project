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
    /*
    GameObject SpawnDelegate(Vector3 position, System.Guid assetId){
        return Instantiate(twentySided, position, twentySided.transform.rotation);
    }
    void UnSpawnDelegate(GameObject spawned){
         Destroy(spawned);
    }
*/

    private GameObject SpawnDie(SpawnMessage msg){
        return Instantiate(twentySided, new Vector3(msg.position.x, msg.position.y, msg.position.z + 2), msg.rotation);
    }

    public void UnSpawnDie(GameObject spawned){
        Destroy(spawned);
    }
    [Client]
    public void rollDie(GameObject dieToRoll,Transform transform){
        GameObject die = Instantiate(dieToRoll, new Vector3(transform.position.x, transform.position.y, transform.position.z + 2), transform.rotation);
        NetworkServer.Spawn(die);
        //die.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(10, 40), ForceMode.Impulse);
        //die.GetComponent<Rigidbody>().AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        
        //Destroy(die, 30);
        //return SideChecker.sharedSideVal;
    }


}
