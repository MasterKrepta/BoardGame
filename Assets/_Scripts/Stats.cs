using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats")]
public class Stats : ScriptableObject {

    public new string Name;
    public string Description;
    public int Movement;
    public int Strength;
    public int Power;
    public int Health;
}
