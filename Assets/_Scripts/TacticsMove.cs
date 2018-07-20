using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TacticsMove : MonoBehaviour {

    [SerializeField]DisplayStats displayStats;
    public bool turn = false;
    [SerializeField] List<tutTile> selectableTiles = new List<tutTile>();
    [SerializeField] GameObject[] tiles;

    Vector3 raycastOffset = new Vector3(0f, 0.3f, 0f);
    Stack<tutTile> path = new Stack<tutTile>();
    tutTile currentTile = null;

    public float jumpHeight = 2;
    public float moveSpeed = 4;
    public float moveRange;
    public bool moving = false;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    float halfHeight = 0;

    //FOR A*
    public tutTile actualTargetTile;

    protected void Init() {
        tiles = GameObject.FindGameObjectsWithTag("tile");
        displayStats = GetComponent<DisplayStats>();
        moveRange = displayStats.stats.Movement;
        halfHeight = GetComponentInChildren<Collider>().bounds.extents.y;
        if(displayStats == null) {
            Debug.LogWarning("NO STATS ASSIGNED TO " + this.gameObject.name);
        }
        TurnManager.AddUnit(this);
    }

    public void GetCurrentTile() {
        //Debug.Log("start of get current");
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
        
        return tile;
    }

    public void GetNeighbors(float jumpHeight, tutTile target) {
        //Debug.Log(tiles.Length);
        foreach (GameObject tile in tiles) {
            tutTile t = tile.GetComponent<tutTile>();
            
            t.FindNeighbors(jumpHeight, target); 
        }
    }
    public void FindSelectableTiles() {
        GetNeighbors(jumpHeight, null);
        GetCurrentTile();

        Queue<tutTile> process = new Queue<tutTile>();

        process.Enqueue(currentTile);
        currentTile.visited = true;

        while (process.Count > 0) {

            tutTile t = process.Dequeue();

            selectableTiles.Add(t);
            t.selectable = true;

            if (t.distance < moveRange) {
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

    public void Move() {
        if(path.Count > 0) {
            tutTile t = path.Peek();
            Vector3 target = t.transform.position;

            //target.y += halfHeight + t.GetComponentInChildren<Collider>().bounds.extents.y; // Needed incase we have multi sized tiles otherwise we would cache it
            target.y += .25f;

            //All we need if no jumping/falling // Jumping part starts at 14:00 in section 4
            if(Vector3.Distance(transform.position, target) >= 0.25f) {
                CalculateHeading(target);
                SetHorizontalVelocity();

                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
            }
            else {
                //Tile center reached
                transform.position = target;
                path.Pop();
            }

        }
        else {
            RemoveSelectableTiles();
            moving = false;

            TurnManager.EndTurn();
        }
    }

    private void SetHorizontalVelocity() {
        velocity = heading * moveSpeed;

    }

    private void CalculateHeading(Vector3 target) {
        heading = target - transform.position;
        heading.Normalize(); // Make it a unit Vector

    }

    void RemoveSelectableTiles() {
        if (currentTile != null) {
            currentTile.current = false;
            currentTile = null;
        }
    
        foreach (tutTile tile in selectableTiles) {
            tile.Reset();
        }
        selectableTiles.Clear();
    }

    public void BeginTurn() {
        turn = true;
    }


    public void EndTurn() {
        turn = false;
    }

    protected void FindPath(tutTile target) {
        
        GetNeighbors(jumpHeight, target);
        GetCurrentTile();

        List<tutTile> openList = new List<tutTile>();
        List<tutTile> closedList = new List<tutTile>();

        openList.Add(currentTile);
        //currentTile.parent = ?? 
        currentTile.h = Vector3.Distance(currentTile.transform.position, target.transform.position);
        currentTile.f = currentTile.h;

        while (openList.Count > 0) {
            tutTile t = FindLowestF(openList);

            closedList.Add(t);

            if(t == target) {
                actualTargetTile = FindEndTile(t);
                MoveToTile(actualTargetTile);
                return;
            }

            foreach (tutTile tile in t.neighbors) {
                if (closedList.Contains(tile)) {
                    //DO nothing
                } else if (openList.Contains(tile)) {
                    float tempG = t.g + Vector3.Distance(tile.transform.position, t.transform.position);

                    if(tempG < tile.g) {
                        tile.parent = t;
                        tile.g = tempG;
                        tile.f = tile.g + tile.h;
                    }
                }
                else {
                    tile.parent = t;

                    tile.g = tile.g + Vector3.Distance(tile.transform.position, tile.transform.position);
                    tile.h = Vector3.Distance(tile.transform.position, target.transform.position);
                    tile.f = tile.g + tile.h;

                    openList.Add(tile);
                }
            }
        }


        //TODO what happens if there is no path
        Debug.Log("Path not found");
    }

    private tutTile FindLowestF(List<tutTile> openList) {
        
        tutTile lowest = openList[0];
        foreach (tutTile t in openList) {
            if (t.f < lowest.f) {
                lowest = t;
            }
        }

        openList.Remove(lowest);
        return lowest;
    }


    protected tutTile FindEndTile(tutTile t) {
        
        Stack<tutTile> tempPath = new Stack<tutTile>();

        tutTile next = t.parent;
        
        while (next != null) {
        
            tempPath.Push(next);
            next = next.parent;
        }

        if (tempPath.Count <= moveRange) {
            return t.parent;
        }

        tutTile endTile = null;
        for (int i = 0; i <= moveRange; i++) {
            endTile = tempPath.Pop();
        }

        return endTile;
    }


    //private void OnDrawGizmos() {
    //    //Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1)
    //    Debug.DrawRay(gameObject.transform.position, -Vector3.up, Color.red,1);
    //}
}
