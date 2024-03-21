using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;


[System.Serializable]
public class AncestryDataArrayWrapper{
    public List<JSONDataLoader.AncestryData> ancestryData;
}

[System.Serializable]
public class ClassDataArrayWrapper{
    public List<JSONDataLoader.ClassData> classData;
}

public class JSONDataLoader : MonoBehaviour
{
    // Define a class to represent the structure of each ancestry entry
    [System.Serializable]
    public class AncestryData
    {
        public string name;
        public string summary;
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

    [System.Serializable]
    public class ClassData
    {
        public string name;
        public string[] ability;
        public AttackProficiency attack_proficiency;
        public DefenseProficiency defense_proficiency;
        public string fortitude_proficiency;
        public string reflex_proficiency;
        public string will_proficiency;
        public string perception_proficiency;
        public Dictionary<string, string> weakness;
        public Dictionary<string, string> resistance;
        public string[] skill_proficiency;
        public string hp;
    }


    // Define a class to represent the structure of attack proficiency data
    [System.Serializable]
    public class AttackProficiency {

        //Basic Weapon Groups
        public string simple_weapons;
        public string martial_weapons;
        public string advanced_weapons;
        public string unarmed_attacks;

        //Weapons
        public string alchemical_bombs;
        public string longsword;
        public string rapier;
        public string sap;
        public string shortbow;
        public string shortsword;
        public string whip;
        public string club;
        public string crossbow;
        public string dagger;
        public string heavy_crossbow;
        public string staff;
        public string advanced_firearms;
        public string advanced_crossbows;
        public string simple_firearms;
        public string simple_crossbows;
        public string martial_firearms;
        public string martial_crossbows;
    }
    
    // Define a class to represent the structure of defense proficiency data
    [System.Serializable]
    public class DefenseProficiency{
        public string light_armor;
        public string medium_armor;
        public string heavy_armor;
        public string unarmored_defense;
    }

     // Define a class to represent the structure of skill proficiency data
    //[System.Serializable]

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

    // Create a list to store all class data
    public List<ClassData> classList;
    public List<ClassData> ParseJsonArrayClass(string json){
        // Use JsonUtility to directly deserialize the JSON array into a list of AncestryData objects
        ClassDataArrayWrapper dataArray = JsonUtility.FromJson<ClassDataArrayWrapper>(json);
        //Debug.Log(dataArray.ancestryData[1]);
        return new List<ClassData>((IEnumerable<ClassData>)dataArray.classData);
    }


    void Start()
    {
        makeAncestryList();
        makeClassList();

        Debug.Log(classList[0].attack_proficiency.unarmed_attacks);
    }


    private void makeAncestryList(){
        
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
            ancestryList.Sort((x, y) => x.name.CompareTo(y.name));
        }
        else
        {
            Debug.LogError("Ancestry JSON file not found!");
        }
    
    }

     private void makeClassList(){
        
        // Load JSON file from Resources folder
        TextAsset jsonFile = Resources.Load<TextAsset>("class");

        // Parse JSON data into list of AncestryData objects
        if (jsonFile != null)
        {
            classList = new List<ClassData>();
            string jsonString = jsonFile.ToString();
            //ancestryList.AddRange((IEnumerable<AncestryData>)JsonUtility.FromJson<AncestryData>(jsonString));
            List<ClassData> classDataList = ParseJsonArrayClass(jsonString);
            foreach(ClassData data in classDataList){
                classList.Add(data);
                //Debug.Log(JsonUtility.ToJson(data));
            }
            classList.Sort((x, y) => x.name.CompareTo(y.name));
        }
        else
        {
            Debug.LogError("Ancestry JSON file not found!");
        }
    
    }

}
