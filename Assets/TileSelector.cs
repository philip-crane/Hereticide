using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    public Tilemap tilemap;
    public List<Vector3Int> leftSelectedTiles = new List<Vector3Int>();
    public List<Vector3Int> rightSelectedTiles = new List<Vector3Int>();
    public List<Vector3Int> currentHoverTiles = new List<Vector3Int>();

    private AttackManager attackManager;

    void Start() 
    {
        
    }

    void Update()
    {
        GameObject scriptRunnerGameObject = GameObject.Find("ScriptRunner");
        attackManager = scriptRunnerGameObject.GetComponent<AttackManager>();


        // Get current tile under mouse
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = tilemap.transform.position.z;
        Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);

        // Handle left click select tiles
        if (Input.GetMouseButtonDown(0))
        {
            leftSelectedTiles.Clear();
            rightSelectedTiles.Clear();
            if (tilemap.HasTile(cellPosition)) leftSelectedTiles.Add(cellPosition);
        }

        // Handle right click select tiles
        if (Input.GetMouseButtonDown(1))
        {
            rightSelectedTiles.Clear();
            if (tilemap.HasTile(cellPosition)) rightSelectedTiles.Add(cellPosition);
        }

        // Handle hover
        currentHoverTiles.Clear();
        if (tilemap.HasTile(cellPosition))
        {
            currentHoverTiles.Add(cellPosition);
        }
    }
}

