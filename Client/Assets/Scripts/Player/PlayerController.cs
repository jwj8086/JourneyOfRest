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
        //�÷��̾� �̵�
        //left or a = -1 / right or d = 1
        float x = Input.GetAxisRaw("Horizontal");
        _renderer.flipX = x == 0 ? _renderer.flipX : ( x < 0 );
        _movement2D.Move(new Vector2(x, 0));
        //�¿� �̵� ���� ����
        _animator.SetBool(_animRunStr, x != 0);


        //�÷��̾� ���� (�����̽� Ű�� ������ ����)
        if(Input.GetKeyDown(KeyCode.Space)) {
            _movement2D.Jump();
        }

        //�����̽� Ű�� ������ ������ isLongJump = true
        if(Input.GetKey(KeyCode.Space)) {
            _movement2D.isLongJump = true;
        }

        //�����̽� Ű�� ���� isLongJump = false
        else if(Input.GetKeyUp(KeyCode.Space)) {
            _movement2D.isLongJump = false;
        }
    }
}
