using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AttackManager : MonoBehaviour
{
    public Tilemap tilemap;
    
    // Declare Imports
    private CreatureManager creatureManager;
    private OccupationManager occupationManager;
    private TileSelector tileSelector;
    
    // Vars 
    public List<GameObject> availableTargets;
    string ability = "";
    string previousAbility;
    bool hasJustAttacked;
    Vector3Int previousTilePosition;

    void Start()
    {
        
    }

    void Update()
    {
        Import();
        // Manage Select Creature
        if (creatureManager.selectedCreature == null) { availableTargets.Clear(); return; }
        if (creatureManager.selectedCreature.tilePosition == previousTilePosition) ability = GetAbility(ability);
        else ability = "";
        if (ability != previousAbility ) availableTargets = GetAvailableTargets(ability);
        previousTilePosition = creatureManager.selectedCreature.tilePosition;
        previousAbility = ability;

        // Execute Attack
        foreach (Vector3Int targetTile in tileSelector.rightSelectedTiles)
        {
            foreach (GameObject obj in availableTargets) {
                if (targetTile == tilemap.WorldToCell(obj.transform.position)) {

                    Creature targetCreature = obj.GetComponent<Creature>();
                    // Breakable targetBreakable = obj.GetComponent<Breakable>();
                    // Unbreakable targetUnbreakable = obj.GetComponent<Unbreakable>();

                    if (ability == "strike") Strike(creatureManager.selectedCreature, targetCreature);
                    if (ability == "shoot") Shoot(creatureManager.selectedCreature, targetCreature);
                    // if (ability == "grab") Grab(creatureManager.selectedCreature, targetCreature);
                    hasJustAttacked = true;
                }
            }
        }
        if (hasJustAttacked) {
            tileSelector.rightSelectedTiles.Clear();
            hasJustAttacked = false;
        }
    }
    Dictionary<string, Tuple<int, string, string>> abilityTargetInfo = new Dictionary<string, Tuple<int, string, string>>
    {
        { "", new Tuple<int, string, string> ( 0, "", "" ) },
        { "strike", new Tuple<int, string, string> ( 1, "Creature", "" ) },
        { "shoot", new Tuple<int, string, string> ( 10, "Creature", "" ) },
        { "grab", new Tuple<int, string, string> ( 1, "Creature", "" ) }
    };

    void Import() {
        GameObject scriptRunnerGameObject = GameObject.Find("ScriptRunner");
        creatureManager = scriptRunnerGameObject.GetComponent<CreatureManager>();
        occupationManager = scriptRunnerGameObject.GetComponent<OccupationManager>();
        tileSelector = scriptRunnerGameObject.GetComponent<TileSelector>();
        
    }

    // Attack Mechanics
    void Strike(Creature attacker, Creature targetCreature) {
        // defender.vigor -= attacker.vigor;
        targetCreature.TakeDamage(attacker.vigor);
        Debug.Log("Strike!");
    }
    void Shoot(Creature attacker, Creature defender) {
        defender.dexterity -= attacker.vigor;
        Debug.Log("Shoot");
    }

    string GetAbility(string ability) {
        if (Input.GetKeyDown(KeyCode.Escape)) return "";
        else if (Input.GetKeyDown(KeyCode.Alpha1)) return "strike"; 
        else if (Input.GetKeyDown(KeyCode.Alpha2)) return "shoot";
        else if (Input.GetKeyDown(KeyCode.Alpha3)) return "grab";
        else return ability;
    }

    List<GameObject> GetAvailableTargets(string ability) {

        availableTargets = new List<GameObject>();
        int range = abilityTargetInfo[ability].Item1;
        string targetableObjectType = abilityTargetInfo[ability].Item2;

        Vector3Int attackerPosition = creatureManager.selectedCreature.tilePosition;

        foreach (GameObject obj in occupationManager.placeableObjects) {
            Vector3Int targetPosition = tilemap.WorldToCell(obj.transform.position);
            int distanceX = Math.Abs(attackerPosition[0] - targetPosition[0]);
            int distanceY = Math.Abs(attackerPosition[1] - targetPosition[1]);
            if ((distanceX > 0 || distanceY > 0) && distanceX <= range && distanceY <= range) {
                if (obj.tag == targetableObjectType) availableTargets.Add(obj);
            }
        }

        return availableTargets;
    }
    

    // If rightclick on tile that is part of availableTargets
    // if click on self, have confirm menu; add later. for now just allow for hitting yourself lol
    // 
    
}