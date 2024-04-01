using UnityEngine;
using Mirror;
using Steamworks;

public class SpriteBillboard : NetworkBehaviour
{
    public Camera GameplayCamera;

    public override void OnStartAuthority()
    {
        GameplayCamera = null;
        GameplayCamera = GameObject.Find("GameplayCam").GetComponent<Camera>();
    }

    void OnEnable(){
        //GameplayCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Awake(){
        //GameplayCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x,GameplayCamera.transform.rotation.eulerAngles.y,transform.rotation.z);
    }
}
