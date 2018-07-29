using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapons", order = 1)]
public class Weapon : ScriptableObject {
    public string modifier = "none";
    public float strength = 2f;
    public bool ranged = false;
}
