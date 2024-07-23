using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public Tilemap tilemap;
    public bool isSelected;
    public int corpus;
    public int vigor;
    public int dexterity;
    private Vector3Int tilePosition;

    private TileSelector tileSelector;

    void Start()
    {
        GameObject scriptRunnerGameObject = GameObject.Find("ScriptRunner");
        tileSelector = scriptRunnerGameObject.GetComponent<TileSelector>();
    }

    void Update()
    {
        // Get tile position
        tilePosition = tilemap.WorldToCell(this.transform.position);

        // Check selection
        foreach (Vector3Int tile in tileSelector.leftSelectedTiles) {
            this.isSelected = tilePosition == tile; // tilepos == tile is an expression that returns true or false (if selected or now), which is then assigned to isSelected
        }

        
    }
}
