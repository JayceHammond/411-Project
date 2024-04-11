using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
 
public class StartGame : MonoBehaviour {
 
    public GameObject theTree;
    //public GameObject theBush; //Can't do bushes the same way until I can grab speific list
    float offset;
    List<TreeInstance> SavedTrees;
 
    // Use this for initialization
    void Start () {

        theTree = Resources.Load<GameObject>("Prefabs/Tall Tree");
        offset = theTree.GetComponent<SpriteRenderer>().size.y/2;
        // Grab the Ground's terrain data
        TerrainData theGround;
        theGround = GameObject.Find("Ground").GetComponent<Terrain>().terrainData;
        Debug.Log(offset);
        // For every tree on the ground
        Debug.Log(theGround.treeInstances);
        foreach (TreeInstance tree in theGround.treeInstances) {
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

    //Adds back the trees in terrain data so, we don't have to remake every run
    void OnApplicationQuit(){
        TerrainData theGround;
        theGround = GameObject.Find("Ground").GetComponent<Terrain>().terrainData;

        theGround.treeInstances = SavedTrees.ToArray();
    }
}
 