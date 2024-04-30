using System.Collections.Generic;
using System.Linq;
using Org.BouncyCastle.Security;
using Unity.Collections;
using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    public Camera GameplayCamera;
    public Quaternion SpriteQuat;

    void Start(){
        GameplayCamera = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
        SpriteQuat = new Quaternion{
            x = transform.localRotation.x,
            y = GameplayCamera.transform.rotation.y,
            z = transform.localRotation.z,
            w = transform.localRotation.w,
        };

        transform.rotation = SpriteQuat;
    }
}
