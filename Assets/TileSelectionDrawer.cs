using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSelectionDrawer : MonoBehaviour
{
    public Tile hoverHighlightTile;
    public Tilemap hoverTilemap;
    public Tile highlightTile;
    public Tilemap highlightTilemap;
    private TileSelector selector;

    // Start is called before the first frame update
    void Start()
    {
        GameObject selectorGameObject = GameObject.Find("ScriptRunner");
        selector = selectorGameObject.GetComponent<TileSelector>();
    }

    // Update is called once per frame
    void Update()
    {
        // Draw HoverTile
        hoverTilemap.ClearAllTiles();
        foreach (var tile in selector.currentHoverTiles) {
            hoverTilemap.SetTile(tile, hoverHighlightTile);
        }

        // Draw rightSelectedTile
        highlightTilemap.ClearAllTiles();
        foreach (var tile in selector.leftSelectedTiles) {
            highlightTilemap.SetTile(tile, highlightTile); // Draw selected tile
        }
        
    }
}

