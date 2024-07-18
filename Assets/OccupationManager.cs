using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OccupationManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Dictionary<Vector3Int, GameObject> occupiedTiles = new Dictionary<Vector3Int, GameObject>();

    void Start()
    {
    }

    void Update()
    {
        GameObject[] creatureObjects = GameObject.FindGameObjectsWithTag("Creature");
        GameObject[] breakableObjects = GameObject.FindGameObjectsWithTag("Breakable");
        GameObject[] unbreakableObjects = GameObject.FindGameObjectsWithTag("Unbreakable");
        GameObject[] placeableObjects = creatureObjects.Concat(breakableObjects).Concat(unbreakableObjects).ToArray();
 
        occupiedTiles.Clear();
        foreach (var obj in placeableObjects) {
            Vector3 objectPosition = obj.transform.position;
            Vector3Int objectTilePosition = tilemap.WorldToCell(objectPosition);
            occupiedTiles[objectTilePosition] = obj;
        }

        
    }
}
