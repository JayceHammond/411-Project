using System.Collections.Generic;
using System.Linq;
using Org.BouncyCastle.Security;
using Unity.Collections;
using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    public Camera GameplayCamera;
    public Quaternion SpriteQuat;

    void OnEnable(){
        GameplayCamera = GameObject.Find("GameplayCamera").GetComponent<Camera>();
    }

    void Awake(){
        GameplayCamera = GameObject.Find("GameplayCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        SpriteQuat = new Quaternion{
            x = transform.localRotation.x,
            y = Quaternion.Euler(0,GameplayCamera.transform.rotation.eulerAngles.y,0).y,
            z = transform.localRotation.z,
            w = transform.localRotation.w,
        };

        transform.rotation = SpriteQuat;
    }
}
