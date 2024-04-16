using Unity.Collections;
using UnityEngine;

public class SpriteBillboardPlayer : MonoBehaviour
{
    public Camera GameplayCamera;
    //public Quaternion SpriteQuat;

    void OnEnable(){
        GameplayCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Awake(){
        GameplayCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0,GameplayCamera.transform.rotation.eulerAngles.y,0);
    }
}
