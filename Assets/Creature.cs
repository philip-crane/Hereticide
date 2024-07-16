using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public Tilemap tilemap;
    public bool isControllable;
    public bool isSelected;

    private Vector3Int tilePosition;

    private Selector selector;

    void Start()
    {
        GameObject selectorGameObject = GameObject.Find("ScriptRunner");
        selector = selectorGameObject.GetComponent<Selector>();
    }

    void Update()
    {
        // Get tile position
        tilePosition = tilemap.WorldToCell(this.transform.position);

        // Check selection
        foreach (var tile in selector.leftSelectedTiles) {
            this.isSelected = tilePosition == tile; // tilepos == tile is an expression that returns true or false (if selected or now), which is then assigned to isSelected
        }

        
    }
}
