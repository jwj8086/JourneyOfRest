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
        //x축 이동은 x * speed로, y축 이동은 기존의 속력 값 (현재는 중력)
        dir *= _runSpeed;
        dir.y = _rigid2D.velocity.y;
        _rigid2D.velocity = dir;
    }

    public void Jump() {
        // jumpForce의 크기만큼 윗쪽 방향으로 속력 설정
        _rigid2D.velocity = Vector2.up * _jumpForce;
    }
}
