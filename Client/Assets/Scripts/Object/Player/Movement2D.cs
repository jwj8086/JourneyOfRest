using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour {
    #region Componenets
    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigid2D;

    #endregion

    #region Variables
    [Header("Variables")]
    [SerializeField]  private float _runSpeed = 5.0f; //�̵� �ӵ�
    [SerializeField]  private float _jumpForce = 8.0f;

    [HideInInspector] public bool isLongJump = false;

    #endregion

    private void Awake() {
        
    }

    private void FixedUpdate() {
        if(isLongJump && _rigid2D.velocity.y > 0) {
            _rigid2D.gravityScale = 1.0f;
        }
        else {
            _rigid2D.gravityScale = 2.5f;
        }
    }

    public void Move(Vector2 dir) {
        //x�� �̵��� x * speed��, y�� �̵��� ������ �ӷ� �� (����� �߷�)
        dir *= _runSpeed;
        dir.y = _rigid2D.velocity.y;
        _rigid2D.velocity = dir;
    }

    public void Jump() {
        // jumpForce�� ũ�⸸ŭ ���� �������� �ӷ� ����
        _rigid2D.velocity = Vector2.up * _jumpForce;
    }
}
