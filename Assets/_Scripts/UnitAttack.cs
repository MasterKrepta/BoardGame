using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof (WeaponSlot))]
public class UnitAttack : MonoBehaviour {

    WeaponSlot weaponSlot;
    Weapon weapon;
    [SerializeField]SelectedTileIndicator indicator;
    // Use this for initialization
    void Start () {
        weaponSlot = GetComponent<WeaponSlot>();
        weapon = weaponSlot.weapon;
	}
	
	// Update is called once per frame
	void Update () {
        SelectTarget();
	}

    private void SelectTarget() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, 100)) {
            
            NPCMove target = hit.collider.GetComponentInParent<NPCMove>(); // TODO might change this in the future 
            if (target != null) {
                if (Input.GetMouseButtonUp(0) && indicator != null) {
                    // Move our indicator
                    Debug.Log("selected");
                    indicator.MoveIndicator(target.gameObject);
                }
            }
        }
    }

    public void BeginActionPhase() {
        Debug.Log("attacking now");
        
        EndActionPhase();
    }

    void LaunchAttack(Weapon weapon) {
        Debug.Log(weapon.name + " is used to attack");
    }




    void EndActionPhase() {
        TurnManager.EndTurn();
    }
}
