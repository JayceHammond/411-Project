using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    public Camera GameplayCamera;

    void OnEnable(){
        GameplayCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Awake(){
        GameplayCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x,GameplayCamera.transform.rotation.eulerAngles.y,transform.rotation.z);
    }
}
