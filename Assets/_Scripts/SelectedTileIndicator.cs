using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedTileIndicator : MonoBehaviour {

    MoveOnClick player;
    Transform _transform;
	// Use this for initialization
	void Start () {
        _transform = this.gameObject.transform;
        player = FindObjectOfType<MoveOnClick>();
        this.gameObject.SetActive(false);
	}

    public void MoveIndicator(Tile tileToMoveTo) {
        this.gameObject.SetActive(true);
        _transform.position = tileToMoveTo.transform.position;
    }

    public void HideIndicator() {
        this.gameObject.SetActive(false);
    }
}
