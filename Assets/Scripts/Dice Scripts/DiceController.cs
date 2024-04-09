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
    [Server]
    public void rollDie(GameObject dieToRoll,Transform transform){
        System.Guid dieAssetId = System.Guid.NewGuid();
        GameObject die = Instantiate(dieToRoll, new Vector3(transform.position.x, transform.position.y, transform.position.z + 2), transform.rotation);
        NetworkServer.Spawn(die);
        die.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(10, 40), ForceMode.Impulse);
        die.GetComponent<Rigidbody>().AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        
        Destroy(die, 30);
        //return SideChecker.sharedSideVal;
    }



}
