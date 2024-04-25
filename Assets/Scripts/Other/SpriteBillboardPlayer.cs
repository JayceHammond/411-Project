using Unity.Collections;
using UnityEngine;
using Mirror;

public class SpriteBillboardPlayer : MonoBehaviour
{
    public Camera GameplayCamera;
    //public Quaternion SpriteQuat;


    void Awake(){
        //GameplayCamera = GameObject.Find("GameplayCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0,GameplayCamera.transform.rotation.eulerAngles.y,0);
    }
}
