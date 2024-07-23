using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AttackManager : MonoBehaviour
{
    public Tilemap tilemap;
    private CreatureMover creatureMover;
    private OccupationManager occupationManager;
    public List<GameObject> availableTargets;

    void Start()
    {

    }

    void Update()
    {
        GameObject scriptRunnerGameObject = GameObject.Find("ScriptRunner");
        creatureMover = scriptRunnerGameObject.GetComponent<CreatureMover>();
        occupationManager = scriptRunnerGameObject.GetComponent<OccupationManager>();

        if (creatureMover.selectedCreature == null) return;
       
        string[] ability = new string[] {""};

        // Listen for ability input
        if (Input.GetKeyDown(KeyCode.Alpha1)) ability[0] = "strike"; 
        else if (Input.GetKeyDown(KeyCode.Alpha2)) ability[0] = "shoot";
        else if (Input.GetKeyDown(KeyCode.Alpha3)) ability[0] = "grab";
        else return;

        // FindAvailableTargets
        int range = abilityTargetInfo[ability[0]].Item1;
        string targetableObjectType = abilityTargetInfo[ability[0]].Item2;
        availableTargets = new List<GameObject>();
        Vector3Int attackerPosition = tilemap.WorldToCell(creatureMover.selectedCreature.transform.position);

            // Get 
        foreach (GameObject obj in occupationManager.placeableObjects) {
            Vector3Int targetPosition = tilemap.WorldToCell(obj.transform.position);
            
            int distanceX = Math.Abs(attackerPosition[0] - targetPosition[0]);
            int distanceY = Math.Abs(attackerPosition[1] - targetPosition[1]);
            if ((distanceX > 0 || distanceY > 0) && distanceX <= range && distanceY <= range) {
                if (obj.tag == targetableObjectType) availableTargets.Add(obj);
            }
        }
    }


    Dictionary<string, Tuple<int, string, string>> abilityTargetInfo = new Dictionary<string, Tuple<int, string, string>>
    {
        { "", new Tuple<int, string, string> ( 0, "", "" ) },
        { "strike", new Tuple<int, string, string> ( 1, "Creature", "" ) },
        { "shoot", new Tuple<int, string, string> ( 10, "Creature", "" ) },
        { "grab", new Tuple<int, string, string> ( 1, "Creature", "" ) }
    };

    void Strike(Creature attacker, Creature defender) {
        defender.vigor -= attacker.vigor;
    }
    void Shoot(Creature attacker, Creature defender) {
        defender.dexterity -= attacker.vigor;
    }
}