using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using System.Linq;

public class CreatureMover : MonoBehaviour
{
    public Tilemap tilemap;
    public Pathfinder pathfinder;
    private List<Creature> allSelectedCreatures = new List<Creature>();
    private Creature selectedCreature;
    private GameObject[] placableObjects;
    private TileSelector tileSelector;
    private Vector3Int targetTile;
    private OccupationManager occupationManager;
    public float speed = 5f;

    void Start()
    {
        GameObject scriptRunnerGameObject = GameObject.Find("ScriptRunner");
        tileSelector = scriptRunnerGameObject.GetComponent<TileSelector>();
        placableObjects = GameObject.FindGameObjectsWithTag("Creature");
        occupationManager = scriptRunnerGameObject.GetComponent<OccupationManager>();
        pathfinder = scriptRunnerGameObject.GetComponent<Pathfinder>();
    }

    void Update()
    {
        // Find selected creature
        selectedCreature = null;
        foreach (GameObject obj in placableObjects)
        {
            Creature creature = obj.GetComponent<Creature>();
            if (creature.isSelected)
            {
                selectedCreature = creature;
                break;
            }
        }

        // Get end position
        foreach (var targetTile in tileSelector.rightSelectedTiles)
        {
            if (selectedCreature == null) break;

            if (!occupationManager.occupiedTiles.ContainsKey(targetTile))
            {
                List<Vector3Int> path = pathfinder.FindPath(tilemap.WorldToCell(selectedCreature.transform.position), targetTile);
                if (path.Count > 0)
                {
                    StartCoroutine(MoveAlongPath(selectedCreature, path));
                    tileSelector.leftSelectedTiles.Clear();
                    tileSelector.leftSelectedTiles.Add(targetTile);
                }
                else
                {
                    Debug.Log("No path found");
                }
            }
            else
            {
                Debug.Log("Occupied");
            }
        }
        tileSelector.rightSelectedTiles.Clear();
    }

    private IEnumerator MoveAlongPath(Creature creature, List<Vector3Int> path)
    {
        foreach (Vector3Int tile in path)
        {
            Vector3 targetPosition = tilemap.GetCellCenterWorld(tile);
            while (Vector3.Distance(creature.transform.position, targetPosition) > 0.1f)
            {
                creature.transform.position = Vector3.MoveTowards(creature.transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
