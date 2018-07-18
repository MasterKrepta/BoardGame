using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsMove : MonoBehaviour {

    [SerializeField] List<tutTile> selectableTiles = new List<tutTile>();
    [SerializeField] GameObject[] tiles;

    Vector3 raycastOffset = new Vector3(0f, 0.3f, 0f);
    Stack<tutTile> path = new Stack<tutTile>();
    tutTile currentTile = null;

    public float jumpHeight = 2;
    public float move = 5;
    public bool moving = false;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    float halfHeight = 0;

    protected void Init() {
        tiles = GameObject.FindGameObjectsWithTag("tile");

        halfHeight = GetComponentInChildren<Collider>().bounds.extents.y;
    }

    public void GetCurrentTile() {
        Debug.Log("start of get current");
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
    }

    public tutTile GetTargetTile(GameObject target) {
        RaycastHit hit;
        tutTile tile = null;
        
        if (Physics.Raycast(target.transform.position + raycastOffset, -Vector3.up, out hit, 1)){
            //Debug.Log(hit.collider.name);
            tile = hit.collider.GetComponentInParent<tutTile>();
        }
        else {
            Debug.Log("NO TILE FOUND");
        }
        
        return tile;
    }

    public void GetNeighbors() {
        //Debug.Log(tiles.Length);
        foreach (GameObject tile in tiles) {
            tutTile t = tile.GetComponent<tutTile>();
            
            t.FindNeighbors(jumpHeight); // STOPPED PART 3 At 20:50
        }
    }
    public void FindSelectableTiles() {
        GetNeighbors();
        GetCurrentTile();

        Queue<tutTile> process = new Queue<tutTile>();

        process.Enqueue(currentTile);
        currentTile.visited = true;

        while (process.Count > 0) {

            tutTile t = process.Dequeue();

            selectableTiles.Add(t);
            t.selectable = true;

            if (t.distance < move) {
                foreach (tutTile tile in t.neighbors) {
                    if (!tile.visited) {
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 2 + t.distance;
                        process.Enqueue(tile);
                    }
                }
            }
        
        }

       
    }

    public void MoveToTile(tutTile tile) {
        path.Clear();
        tile.target = true;
        moving = true;

        tutTile next = tile;
        while (next != null) {
            path.Push(next);
            next = next.parent;
        }
    }
    //private void OnDrawGizmos() {
    //    //Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1)
    //    Debug.DrawRay(gameObject.transform.position, -Vector3.up, Color.red,1);
    //}
}
