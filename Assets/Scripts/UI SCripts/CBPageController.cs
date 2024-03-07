using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBPageController : MonoBehaviour
{
    public List<GameObject> menues;
    public GameObject selectGameObject;

    public void onNext(){
        for(int i = 0; i < menues.Count - 1; i++){
            if(menues[i].activeSelf){
                menues[i+1].SetActive(true);
                menues[i].SetActive(false);
            }
        }
    }
    public void onPrev(){
        for(int i = 0; i < menues.Count; i++){
            if(menues[i].activeSelf){
                menues[i-1].SetActive(true);
                menues[i].SetActive(false);
            }
        }
    }

    public void onAncestryClick(){
        selectGameObject.SetActive(true);
    }

    public void onExitAncestry(){
        selectGameObject.SetActive(false);
    }
}
