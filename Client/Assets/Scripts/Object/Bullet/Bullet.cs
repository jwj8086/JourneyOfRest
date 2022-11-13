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
    public Vector3 _dir = Vector3.zero;
    [SerializeField] private float _bulletSpeed = 20.0f;
    [SerializeField] private int _bulletDamage = 20;
    #endregion

    #region Unity Event Functions

    private void Awake() {
        if(_collider == null)
            _collider = GetComponent<BoxCollider2D>();
        if(_rigid == null)
            _rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Debug.Log($"Pos: {transform.position}");
    }

    private void OnEnable() {
        _rigid.velocity = _dir * _bulletSpeed;
        Invoke("Destroy", 10.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger"))
            return;

        if (collision.CompareTag("Enemy")){
            collision.gameObject.GetComponent<EnemyMove>().Takedamage(_bulletDamage);
        }

        Destroy();
    }

    #endregion

    #region Internal Functions
    public void Initialize(Vector3 dir, Vector3 pos) {
        transform.position = pos;
        _dir = dir;
    }

    #endregion
}
