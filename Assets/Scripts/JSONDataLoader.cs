using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;


[System.Serializable]
public class AncestryDataArrayWrapper{
    public List<JSONDataLoader.AncestryData> ancestryData;
}

public class JSONDataLoader : MonoBehaviour
{
    // Define a class to represent the structure of each ancestry entry
    [System.Serializable]
    public class AncestryData
    {
        public string id;
        public string name;
        public string type;
        public int hp;
        public SpeedData speed;
        public string[] size;
        public string[] ability;
        public string[] ability_flaw;
        public string[] trait;
        public string vision;
        public Dictionary<string, string> weakness;
        public Dictionary<string, string> resistance;
        public string rarity;
    }

    // Define a class to represent the structure of speed data
    [System.Serializable]
    public class SpeedData
    {
        public int land;
        public int max;
    }


    // Create a list to store all ancestry data
    public List<AncestryData> ancestryList;
    public List<AncestryData> ParseJsonArray(string json){
        // Use JsonUtility to directly deserialize the JSON array into a list of AncestryData objects
        AncestryDataArrayWrapper dataArray = JsonUtility.FromJson<AncestryDataArrayWrapper>(json);
        //Debug.Log(dataArray.ancestryData[1]);
        return new List<AncestryData>((IEnumerable<AncestryData>)dataArray.ancestryData);
    }

    void Start()
    {
        // Load JSON file from Resources folder
        TextAsset jsonFile = Resources.Load<TextAsset>("ancestry");

        // Parse JSON data into list of AncestryData objects
        if (jsonFile != null)
        {
            ancestryList = new List<AncestryData>();
            string jsonString = jsonFile.ToString();
            //ancestryList.AddRange((IEnumerable<AncestryData>)JsonUtility.FromJson<AncestryData>(jsonString));
            List<AncestryData> ancestryDataList = ParseJsonArray(jsonString);
            foreach(AncestryData data in ancestryDataList){
                ancestryList.Add(data);
                //Debug.Log(JsonUtility.ToJson(data));
            }
        }
        else
        {
            Debug.LogError("Ancestry JSON file not found!");
        }

        // Example usage:
        // Access individual ancestry data
        if (ancestryList.Count > 0)
        {
            Debug.Log("First Ancestry ID: " + ancestryList[0].id);
            Debug.Log("First Ancestry Name: " + ancestryList[0].name);
            // Access other properties similarly
        }
    }
}
