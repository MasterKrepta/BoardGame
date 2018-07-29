using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedTileIndicator : MonoBehaviour {

    
    Transform _transform;

	// Use this for initialization
	void Start () {
        _transform = this.gameObject.transform;
    
        this.gameObject.SetActive(false);
	}

    public void MoveIndicator(GameObject target) {
        this.gameObject.SetActive(true);

        //_transform.position = target.transform.position;
        transform.position = target.transform.position;
        Debug.Log(transform.position);
        Debug.Log(target.transform.position + " after");
    }

    public void HideIndicator() {
        this.gameObject.SetActive(false);
    }
}
