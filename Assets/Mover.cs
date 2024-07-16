using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public Tilemap tilemap;

    private List<Creature> allSelectedCreatures = new List<Creature>();
    private Creature selectedCreature;
    private GameObject[] creatureObjects;

    private Selector selector;

    // Start is called before the first frame update
    void Start()
    {
        GameObject selectorGameObject = GameObject.Find("ScriptRunner");
        selector = selectorGameObject.GetComponent<Selector>();

        creatureObjects = GameObject.FindGameObjectsWithTag("Creature");
    }

    // Update is called once per frame
    void Update()
    {
        // Find selected creature
        selectedCreature = null;
        foreach (GameObject obj in creatureObjects)
        {
            Creature creature = obj.GetComponent<Creature>();
            if (creature.isSelected) {
                selectedCreature = creature;
                break;
            }
        }

        // Get end position
        foreach (var tile in selector.rightSelectedTiles) {
            selectedCreature.transform.position = tilemap.GetCellCenterWorld(tile);
            selector.leftSelectedTiles.Clear();
            selector.leftSelectedTiles.Add(tile);
        }


    }
}
