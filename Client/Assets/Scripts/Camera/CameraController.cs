using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private Transform _targetTransform = null;


    private void Update() {
        if(_targetTransform == null)
            return;

        transform.position = new Vector3(_targetTransform.position.x, _targetTransform.position.y, -15f);
    }
}