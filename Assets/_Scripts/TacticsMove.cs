using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsMove : MonoBehaviour {

    List<tutTile> selectableTiles = new List<tutTile>();
    GameObject[] tiles;

    Stack<tutTile> path = new Stack<tutTile>();
    tutTile currentTile;

    public float jumpHeight = 2;
    public float move = 5;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    float halfHeight = 0;

    protected void Init() {
        tiles = GameObject.FindGameObjectsWithTag("tile");

        halfHeight = GetComponentInChildren<Collider>().bounds.extents.y;
    }

    public void GetCurrentTile() {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
    }

    public tutTile GetTargetTile(GameObject target) {
        RaycastHit hit;
        tutTile tile = null;
        if(Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1)){
            tile = hit.collider.GetComponentInChildren<tutTile>();
        }

        return tile;
    }

    public void GetNeighbors() {
        foreach (GameObject tile in tiles) {
            tutTile t = tile.GetComponentInChildren<tutTile>();
            t.FindNeighbors(jumpHeight); // STOPPED PART 3 At 20:50
        }
    }
    public void FindSelectableTiles() {
        GetNeighbors();
        GetCurrentTile();

        Queue<tutTile> process = new Queue<tutTile>();
        currentTile.visisted = true;

        while (process.Count > 0) {
            tutTile t = process.Dequeue();
            selectableTiles.Add(t);
            t.selectable = true;

            if (t.distance < move) {
                foreach (tutTile tile in t.neighbors) {
                    if (!tile.visisted) {
                        tile.parent = t;
                        tile.visisted = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                    }
                }
            }
        
        }


    }
}
