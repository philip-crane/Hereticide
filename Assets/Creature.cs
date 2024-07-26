using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using Unity.VisualScripting;

public class Creature : MonoBehaviour
{
    public Tilemap tilemap;
    public bool isSelected;
    public int damage;
    public int corpus;
    public int vigor;
    public int dexterity;
    public Vector3Int tilePosition;
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
        
        // Kill
        if (corpus <=0) { Destroy(gameObject); }
    }

    public void TakeDamage(int damage) {
        if (vigor > 0) { vigor -= damage; }
        if (vigor < 0) { corpus += vigor; vigor = 0; } // NB: Vigor is negative. Vig = 0 is to ensure that it doesn't go below 0
    }
}
