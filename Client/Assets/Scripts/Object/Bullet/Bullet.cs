using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : Poolable {
    #region Components
    private BoxCollider2D _collider = null;
    private Rigidbody2D _rigid = null;
    #endregion


    #region Variables
    public Vector2 _dir = Vector2.zero;
    [SerializeField] private float _bulletSpeed = 10.0f;
    [SerializeField] private int _bulletDamage = 20;
    #endregion

    #region Unity Event Functions

    private void Awake() {
        if(_collider == null)
            _collider = GetComponent<BoxCollider2D>();
        if(_rigid == null)
            _rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        _rigid.velocity = _dir * _bulletSpeed;
        Invoke("Destroy", 10.0f);
    }

    private void OnTriggerEnter(Collider other) {
        Destroy();
    }

    #endregion

    #region Internal Functions
    public void Initialize(Vector2 dir) {
        _dir = dir;
    }

    #endregion
}
