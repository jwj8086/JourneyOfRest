using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public enum BulletType : ushort
{
    Normal = 0,
    Incendiary,
    Explosive,
}
public class Bullet : Poolable {
    #region Components
    private BoxCollider2D _collider = null;
    private Rigidbody2D _rigid = null;
    private Animator _anim = null;
    #endregion


    #region Variables
    public Vector3 _dir = Vector3.zero;
    [SerializeField] private float _bulletSpeed = 20.0f;
    [SerializeField] private int _bulletDamage = 20;
    [SerializeField] private BulletType _bulletType = BulletType.Normal;
    #endregion

    #region Unity Event Functions

    private void Awake() {
        if(_collider == null)
            _collider = GetComponent<BoxCollider2D>();
        if(_rigid == null)
            _rigid = GetComponent<Rigidbody2D>();
        if (_anim == null)
            _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Debug.Log($"Pos: {transform.position}");
    }

    private void OnEnable() {
        _anim.SetInteger("BulletType", (int)_bulletType);
        _rigid.velocity = _dir * _bulletSpeed;
        Invoke("Destroy", 10.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger"))
            return;

        if (collision.CompareTag("Enemy")){
            collision.gameObject.GetComponent<EnemyMove>().Takedamage(_bulletDamage);
            _rigid.velocity = Vector3.zero;
            _anim.SetTrigger("OnHit");
            Invoke("Destroy", 1.0f);
            return;
        }
        Destroy();
    }

    #endregion

    #region Internal Functions
    public void Initialize(Vector3 dir, Transform trans) {
        transform.position = trans.position;
        transform.rotation = Quaternion.identity;
        _dir = dir;
        transform.Rotate(new Vector3(0, 0, -1 * trans.forward.x * 90));
    }

    #endregion
}
