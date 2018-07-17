using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {

    Quaternion from;
    Quaternion to;
    [SerializeField] float rotSpeed = 15f;

    public void RotateWorldRight() {
        //from = transform.rotation;
        //to = Quaternion.Euler(0, 90, 0);
        //StartCoroutine(SlerpRot(from, to, rotSpeed));
        transform.Rotate(Vector3.up, 90, Space.Self);
    }

    public void RotateWorldLeft() {
        //from = transform.rotation;
        //to = Quaternion.Euler(0, -90, 0);
        //StartCoroutine(SlerpRot(from, to, rotSpeed));
        transform.Rotate(Vector3.up, -90, Space.Self);
    }

    IEnumerator SlerpRot(Quaternion startRot, Quaternion endRot, float slerpTime) {
        float elapsed = 0;
        while (elapsed < slerpTime) {
            elapsed += Time.deltaTime;

            transform.rotation = Quaternion.Slerp(startRot, endRot, elapsed / slerpTime);

            yield return null;
        }
    }
}
