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
    [HideInInspector] public bool doubleJumped = false;
    [HideInInspector] public bool isJumping = false;


    #endregion

    private void Awake() {
        
    }

    private void FixedUpdate() {
        
    }

    public void Move(Vector2 dir) {
        //x�� �̵��� x * speed��, y�� �̵��� ������ �ӷ� �� (����� �߷�)
        dir *= _runSpeed;
        dir.y = _rigid2D.velocity.y;
        _rigid2D.velocity = dir;
    }

    public void Jump() {
        // jumpForce�� ũ�⸸ŭ ���� �������� �ӷ� ����
        if (doubleJumped)
            return;
        if (isJumping)
            doubleJumped = true;

        isJumping = true;
        _rigid2D.velocity = Vector2.up * _jumpForce;
    }
}
