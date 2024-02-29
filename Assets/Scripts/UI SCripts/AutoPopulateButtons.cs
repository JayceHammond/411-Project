using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class AutoPopulateButtons : MonoBehaviour
{
    public Button ancestryButton;
    public Transform ancestryButtonParent;
    public JSONDataLoader dataLoader;
    public bool populating = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(populating == true){
            float offset = 0;
            for(int i = 0; i < dataLoader.ancestryList.Count; i++){
                Button button = Instantiate(ancestryButton, ancestryButtonParent);
                button.transform.position = new UnityEngine.Vector3(ancestryButtonParent.transform.position.x, ancestryButtonParent.transform.position.y - offset, ancestryButtonParent.transform.position.z);

                offset = button.GetComponent<RectTransform>().rect.height * i;

                button.GetComponentInChildren<TextMeshProUGUI>().text = dataLoader.ancestryList[i].name;
            }
            populating = false;
        }
    }
}
