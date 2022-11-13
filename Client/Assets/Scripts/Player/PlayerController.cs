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

    #region Unity Event Functions
    private void Awake()
    {
        _weaponary.PlayerDirectionChangedTo(PlayerDirectionType.Right);
    }
    private void Update()
    {

        Movement();

        //�÷��̾� ���� (�����̽� Ű�� ������ ����)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _movement2D.Jump();
        }

        //�ϴ� J�� �����ϴ°ɷ� ����
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Shot();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Rolling();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            _movement2D.doubleJumped = false;
            _movement2D.isJumping = false;
            _animator.SetBool("IsGrounded", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            _animator.SetBool("IsGrounded", false);
    }

    #endregion



    private void Movement() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Turn(y, x);    // �÷��̾� ���� ȸ��
        _movement2D.Move(new Vector2(x, 0));    //�÷��̾� ĳ���� �̵�

        _animator.SetBool(_animRunStr, x != 0);
    }

    private void Shot() {
        _weaponary.PullTrigger();
        _animator.SetTrigger("OnFire");
    }

    private void Rolling() {
        _animator.SetTrigger("OnRoll");
    }

    private void Reload() {
        _weaponary.Reload();
    }

    private void Turn(float vertical, float horizontal)
    {
        _renderer.flipX = horizontal == 0 ? _renderer.flipX : (horizontal < 0);
        _weaponary.PlayerDirectionChangedTo(vertical > 0 ? PlayerDirectionType.Up : _renderer.flipX ? PlayerDirectionType.Left : PlayerDirectionType.Right);
    }

    public void Damage(float atkDamage) {

    }
}
