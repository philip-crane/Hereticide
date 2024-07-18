using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class Pathfinder : MonoBehaviour
{
    public Tilemap tilemap;
    public OccupationManager occupationManager;

    public List<Vector3Int> FindPath(Vector3Int start, Vector3Int target)
    {
        List<Vector3Int> openSet = new List<Vector3Int>();
        HashSet<Vector3Int> closedSet = new HashSet<Vector3Int>();
        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();
        Dictionary<Vector3Int, int> gScore = new Dictionary<Vector3Int, int>();
        Dictionary<Vector3Int, int> fScore = new Dictionary<Vector3Int, int>();

        openSet.Add(start);
        gScore[start] = 0;
        fScore[start] = Heuristic(start, target);

        while (openSet.Count > 0)
        {
            Vector3Int current = openSet.OrderBy(x => fScore.ContainsKey(x) ? fScore[x] : int.MaxValue).First();

            if (current == target)
            {
                return ReconstructPath(cameFrom, current);
            }

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (Vector3Int neighbor in GetNeighbors(current))
            {
                if (closedSet.Contains(neighbor) || occupationManager.occupiedTiles.ContainsKey(neighbor) || !IsWithinBounds(neighbor))
                {
                    continue;
                }

                int tentativeGScore = gScore[current] + 1; // Assuming cost between nodes is 1

                if (!openSet.Contains(neighbor))
                {
                    openSet.Add(neighbor);
                }
                else if (tentativeGScore >= gScore[neighbor])
                {
                    continue;
                }

                cameFrom[neighbor] = current;
                gScore[neighbor] = tentativeGScore;
                fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, target);
            }
        }

        return new List<Vector3Int>(); // Return an empty list if no path is found
    }

    private List<Vector3Int> ReconstructPath(Dictionary<Vector3Int, Vector3Int> cameFrom, Vector3Int current)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        while (cameFrom.ContainsKey(current))
        {
            path.Add(current);
            current = cameFrom[current];
        }
        path.Reverse();
        return path;
    }

    private IEnumerable<Vector3Int> GetNeighbors(Vector3Int tile)
    {
        return new List<Vector3Int>
        {
            tile + new Vector3Int(1, 0, 0),
            tile + new Vector3Int(-1, 0, 0),
            tile + new Vector3Int(0, 1, 0),
            tile + new Vector3Int(0, -1, 0)
        };
    }

    private int Heuristic(Vector3Int a, Vector3Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    private bool IsWithinBounds(Vector3Int tile)
    {
        return tilemap.HasTile(tile);
    }
}
