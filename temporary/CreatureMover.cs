using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using System.Linq;

public class CreatureMover : MonoBehaviour
{
    public Tilemap tilemap;
    private List<Creature> allSelectedCreatures = new List<Creature>();
    private Creature selectedCreature;
    private GameObject[] placableObjects;
    private TileSelector tileSelector;
    private Vector3Int targetTile;
    private OccupationManager occupationManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject scriptRunnerGameObject = GameObject.Find("ScriptRunner");
        tileSelector = scriptRunnerGameObject.GetComponent<TileSelector>();
        placableObjects = GameObject.FindGameObjectsWithTag("Creature");
        occupationManager = scriptRunnerGameObject.GetComponent<OccupationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Find selected creature
        selectedCreature = null;
        foreach (GameObject obj in placableObjects)
        {
            Creature creature = obj.GetComponent<Creature>();
            if (creature.isSelected) {
                selectedCreature = creature;
                break;
            }
        }

        // Get end position
        foreach (var targetTile in tileSelector.rightSelectedTiles) {
            if (selectedCreature == null) break;

            if (!occupationManager.occupiedTiles.ContainsKey(targetTile)) {
                // selectedCreature.transform.position = tilemap.GetCellCenterWorld(targetTile);
                tileSelector.leftSelectedTiles.Clear();
                tileSelector.leftSelectedTiles.Add(targetTile);
            }
            else Debug.Log("Occupied");
        }
        tileSelector.rightSelectedTiles.Clear();

        // if (!occupationManager.occupiedTiles.ContainsKey(targetTile)) return;
    }
}
