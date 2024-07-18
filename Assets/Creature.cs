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

    private TileSelector tileSelector;

    void Start()
    {
        GameObject selectorGameObject = GameObject.Find("ScriptRunner");
        tileSelector = selectorGameObject.GetComponent<TileSelector>();
    }

    void Update()
    {
        // Get tile position
        tilePosition = tilemap.WorldToCell(this.transform.position);

        // Check selection
        foreach (var tile in tileSelector.leftSelectedTiles) {
            this.isSelected = tilePosition == tile; // tilepos == tile is an expression that returns true or false (if selected or now), which is then assigned to isSelected
        }

        
    }
}
