using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    #region Components
    [Header("Components")]
    [SerializeField] private Movement2D      _movement2D = null;
    [SerializeField] private SpriteRenderer  _renderer   = null;
    [SerializeField] private Animator        _animator   = null;

    #endregion

    #region Variables
    //[Header("Variables")]
    private string _animRunStr = "IsRun";
    #endregion

    private void Awake() {

    }

    private void Update() {
        //플레이어 이동
        //left or a = -1 / right or d = 1
        float x = Input.GetAxisRaw("Horizontal");
        _renderer.flipX = x == 0 ? _renderer.flipX : ( x < 0 );
        _movement2D.Move(new Vector2(x, 0));
        //좌우 이동 방향 제어
        _animator.SetBool(_animRunStr, x != 0);


        //플레이어 점프 (스페이스 키를 누르면 점프)
        if(Input.GetKeyDown(KeyCode.Space)) {
            _movement2D.Jump();
        }

        //스페이스 키를 누르고 있으면 isLongJump = true
        if(Input.GetKey(KeyCode.Space)) {
            _movement2D.isLongJump = true;
        }

        //스페이스 키를 떼면 isLongJump = false
        else if(Input.GetKeyUp(KeyCode.Space)) {
            _movement2D.isLongJump = false;
        }
    }
}
