using System.Collections.Generic;
using System.Linq;
using UnityEngine;
 
public class StartGame : MonoBehaviour {
 
    public GameObject theTallTree;
    public GameObject theMediumTree; 
    float offset;
    List<GameObject> SpawningTrees;
    List<TreeInstance> SavedTrees;
 
    // Use this for initialization
    void Start () {
        SpawningTrees = treeArray();

        // Grab the Ground's terrain data
        TerrainData theGround;
        theGround = GameObject.Find("Ground").GetComponent<Terrain>().terrainData;

        // For every tree on the ground
        foreach (TreeInstance tree in theGround.treeInstances) {

            GameObject theTree = SpawningTrees[randomTreePicker(SpawningTrees)];

            offset = theTree.GetComponent<SpriteRenderer>().size.y/2;

            // Find its local position scaled by the terrain size (to find the real world position)
            Vector3 worldTreePos = Vector3.Scale(tree.position, theGround.size) + Terrain.activeTerrain.transform.position;
            worldTreePos.y = worldTreePos.y + offset;
            var Tree = Instantiate (theTree, worldTreePos, Quaternion.identity); // Create a prefab tree on its pos
            Tree.transform.parent = GameObject.Find("TreeGenerator").transform;
        }
        
        // Then delete all trees on the ground
        List<TreeInstance> newTrees = new List<TreeInstance>(0);
        SavedTrees = new List<TreeInstance>(theGround.treeInstances.Length);
        SavedTrees = theGround.treeInstances.ToList();
        theGround.treeInstances = newTrees.ToArray();
        
    }

    private List<GameObject> treeArray(){
        List<GameObject> Trees = new List<GameObject>();

        theTallTree = Resources.Load<GameObject>("Prefabs/Tall Tree");
        theMediumTree = Resources.Load<GameObject>("Prefabs/Medium Tree");

        Trees.Add(theTallTree);
        Trees.Add(theMediumTree);

        return Trees;
    }

    private int randomTreePicker(List<GameObject> Trees){
        return Random.Range(0,Trees.Count);
    }

    //Adds back the trees in terrain data so, we don't have to remake every run
    void OnApplicationQuit(){
        TerrainData theGround;
        theGround = GameObject.Find("Ground").GetComponent<Terrain>().terrainData;

        theGround.treeInstances = SavedTrees.ToArray();
    }
}
 