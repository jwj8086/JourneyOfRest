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
    [SerializeField] private Weaponary       _weaponary  = null;

    #endregion

    #region Variables
    //[Header("Variables")]
    private const string _animRunStr = "IsRun";

    #endregion

    private void Update() {

        Movement();

        //�÷��̾� ���� (�����̽� Ű�� ������ ����)
        if(Input.GetKeyDown(KeyCode.Space)) {
            _movement2D.Jump();
        }

        //�ϴ� J�� �����ϴ°ɷ� ����
        if(Input.GetKeyDown(KeyCode.Z)) {
            Shot();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            Rolling();
        }

        if(Input.GetKeyDown(KeyCode.R)) {
            Reload();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Floor")) {
            _movement2D.doubleJumped = false;
            _movement2D.isJumping = false;
            _animator.SetBool("IsGrounded", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Floor"))
            _animator.SetBool("IsGrounded", false);
    }

    private void Movement() {
        //�÷��̾� �̵�
        //left or a = -1 / right or d = 1
        float x = Input.GetAxisRaw("Horizontal");
        _renderer.flipX = x == 0 ? _renderer.flipX : ( x < 0 );
        _movement2D.Move(new Vector2(x, 0));
        //�¿� �̵� ���� ����
        _animator.SetBool(_animRunStr, x != 0);
    }

    private void Shot() {
        _weaponary.PullTrigger(_renderer.flipX == true ? Vector2.left : Vector2.right);
        _animator.SetTrigger("Attack");
    }

    private void Rolling() {
        _animator.SetTrigger("OnRoll");
    }

    private void Reload() {
        _weaponary.Reload();
    }

    public void Damage(float atkDamage) {

    }
}
