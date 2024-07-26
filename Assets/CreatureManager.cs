using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using System.Linq;

public class CreatureManager : MonoBehaviour
{
    public float animationSpeed = 5f;
    public Tilemap tilemap;
    private PathfinderGPT pathfinder;
    public Creature selectedCreature;
    public GameObject[] creatureObjects;
    private TileSelector tileSelector;
    private OccupationManager occupationManager;

    void Start()
    {
        GameObject scriptRunnerGameObject = GameObject.Find("ScriptRunner");
        tileSelector = scriptRunnerGameObject.GetComponent<TileSelector>();
        creatureObjects = GameObject.FindGameObjectsWithTag("Creature");
        occupationManager = scriptRunnerGameObject.GetComponent<OccupationManager>();
        pathfinder = scriptRunnerGameObject.GetComponent<PathfinderGPT>();
    }

    void Update()
    {
        // Find selected creature
        selectedCreature = null;
        foreach (GameObject creatureObject in creatureObjects)
        {
            Creature creature = creatureObject.GetComponent<Creature>();
            if (creature.isSelected)
            {
                selectedCreature = creature;
                break;
            }
        }

        // Get end position
        foreach (var targetTile in tileSelector.rightSelectedTiles)
        {
            if (selectedCreature == null) break; // If there isn't a selected creature then break. Not exactly optimal. Too bad!

            Vector3Int startTile = tilemap.WorldToCell(selectedCreature.transform.position);
            Dictionary<Vector3Int, int> reachableTiles = pathfinder.GetReachableTiles(startTile, selectedCreature.dexterity);


            if (!occupationManager.occupiedTiles.ContainsKey(targetTile))
            {
                if (!reachableTiles.ContainsKey(targetTile)) // Could've been a list rather than dictionary. Too bad!
                {
                    Debug.Log("Target tile is out of range.");
                    continue;
                }

                List<Vector3Int> path = pathfinder.FindPath(startTile, targetTile);
                if (path.Count > 0)
                {
                    StartCoroutine(MoveAlongPath(selectedCreature, path));
                    tileSelector.leftSelectedTiles.Clear();
                    tileSelector.leftSelectedTiles.Add(targetTile);
                }
                else
                {
                    // Debug.Log("No path found");
                }
            }
            else
            {
                // Debug.Log("Occupied");
            }
        }
    }

    private IEnumerator MoveAlongPath(Creature creature, List<Vector3Int> path)
    {
        foreach (Vector3Int tile in path)
        {
            Vector3 targetPosition = tilemap.GetCellCenterWorld(tile);
            while (Vector3.Distance(creature.transform.position, targetPosition) > 0.1f)
            {
                creature.transform.position = Vector3.MoveTowards(creature.transform.position, targetPosition, animationSpeed * Time.deltaTime);
                yield return null;
            }
            creature.transform.position = targetPosition; // Ensure exact position
        }
    }
}
