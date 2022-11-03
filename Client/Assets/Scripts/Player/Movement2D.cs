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
    [SerializeField]  private float _runSpeed = 5.0f; //이동 속도
    [SerializeField]  private float _jumpForce = 8.0f;
    [HideInInspector] public bool doubleJumped = false;
    [HideInInspector] public bool isJumping = false;


    #endregion

    private void Awake() {
        
    }

    private void FixedUpdate() {
        
    }

    public void Move(Vector2 dir) {
        //x축 이동은 x * speed로, y축 이동은 기존의 속력 값 (현재는 중력)
        dir *= _runSpeed;
        dir.y = _rigid2D.velocity.y;
        _rigid2D.velocity = dir;
    }

    public void Jump() {
        // jumpForce의 크기만큼 윗쪽 방향으로 속력 설정
        if (doubleJumped)
            return;
        if (isJumping)
            doubleJumped = true;

        isJumping = true;
        _rigid2D.velocity = Vector2.up * _jumpForce;
    }
}
