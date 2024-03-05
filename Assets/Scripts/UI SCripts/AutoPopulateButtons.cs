using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class AutoPopulateButtons : MonoBehaviour
{
    public Button ancestryButton;
    public Transform ancestryButtonParent;
    public TextMeshProUGUI ancestryDescTitle;
    public TextMeshProUGUI ancestrySummary;
    public JSONDataLoader dataLoader;
    public bool populating = true;
    // Start is called before the first frame update
    void Awake()
    {
        dataLoader.ancestryList.Sort((x, y) => x.name.CompareTo(y.name));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(populating == true){
            float offset = 0;
            for(int i = 0; i < dataLoader.ancestryList.Count; i++){
                if(dataLoader.ancestryList[i].type == "Ancestry"){
                    Button button = Instantiate(ancestryButton, ancestryButtonParent);
                    button.transform.position = new UnityEngine.Vector3(ancestryButtonParent.transform.position.x, ancestryButtonParent.transform.position.y - offset, ancestryButtonParent.transform.position.z);
                    offset = button.GetComponent<RectTransform>().rect.height * i;
                    button.GetComponentInChildren<TextMeshProUGUI>().text = dataLoader.ancestryList[i].name;
                    button.onClick.AddListener(delegate {onAncestryClick(button);});
                }
            }
            populating = false;
        }
    }

    void onAncestryClick(Button button){
        ancestryDescTitle.text = button.GetComponentInChildren<TextMeshProUGUI>().text;
        ancestrySummary.text = dataLoader.ancestryList.Find(x => x.name == ancestryDescTitle.text).summary;
        ancestrySummary.text.Truncate(6, "...Show More");
        //Debug.Log(dataLoader.ancestryList.Find(x => ))
    }
}
