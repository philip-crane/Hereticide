using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSelectionDrawer : MonoBehaviour
{
    public Tilemap baseTilemap;
    public Tile hoverHighlightTile;
    public Tile highlightTile;
    public Tilemap highlightTilemap;
    private TileSelector selector;
    private AttackManager attackManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject scriptRunnerGameObject = GameObject.Find("ScriptRunner");
        selector = scriptRunnerGameObject.GetComponent<TileSelector>();
        attackManager = scriptRunnerGameObject.GetComponent<AttackManager>();
    }

    // Update is called once per frame
    void Update()
    {
        highlightTilemap.ClearAllTiles();

        // Draw HoverTile
        foreach (Vector3Int tile in selector.currentHoverTiles) {
            highlightTilemap.SetTile(tile, hoverHighlightTile);
        }

        // Draw rightSelectedTile
        foreach (Vector3Int tile in selector.leftSelectedTiles) {
            highlightTilemap.SetTile(tile, highlightTile); 
        }

        // Draw availableTargets Tiles
        foreach (GameObject obj in attackManager.availableTargets) {
            Vector3Int availableTargetTilePosition = baseTilemap.WorldToCell(obj.transform.position);
            highlightTilemap.SetTile(availableTargetTilePosition, highlightTile);
        }
    }
}

