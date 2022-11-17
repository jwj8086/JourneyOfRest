using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
    #region Components
    [SerializeField] private Rigidbody2D rigid = null;
    [SerializeField] private Animator anim = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private BoxCollider2D boxcollider2D = null;
    private RigidbodyConstraints2D constraints2D;
    #endregion

    #region GameObjects
    [SerializeField] private Bullet bullet = null;
    [HideInInspector] public PlayerController player = null;

    #endregion
    public float nextMove;
    public Transform targetTransform;
    public float sight = 40;
    public bool isTracking = false;

    //나중에 피해 추가
    private bool _isAlive = true;
    public bool IsAlive { get => _isAlive; }

    [SerializeField] private float e_maxHp = 0.0f;
    private float e_curHp = 0.0f;
    public int E_atkDamage = 0;
    public bool E_Attack = false;
    public float E_atkCoolTime = 2.0f;
    private WaitForSeconds E_AttackInterval = null;

    public PlayerController Player {
        get { return player; }
        set {
            player = value;
            if(null == value) {
                E_Attack = false;
            }
            else {
                E_Attack = true;
                Attack();
            }
        }
    }

    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        E_AttackInterval = new WaitForSeconds(E_atkCoolTime);
        bullet = GetComponent<Bullet>();

        Invoke("Think", 0);
    }

    private void OnEnable()
    {
        e_curHp = e_maxHp;
    }

    void FixedUpdate() 
    {
        Move();
        Debug.Log($"CurHp: {e_curHp}");
    }

    void Move() {
        if (!_isAlive)
            return;

        if (E_Attack)
            return;
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down * 3, new Color(0, 1, 0));

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 3, LayerMask.GetMask("Platform"));

        if(rayHit.collider == null) {
            Turn();
        }

        Vector2 moveForce = targetTransform.position - transform.position;


        if(moveForce.magnitude < 10) {
            isTracking = true;

            //TODO: 바라보는 방향이 플레이어 위치가 아닐 경우
            Turn(moveForce.x < 0);
        }

        if(isTracking) {
            if(moveForce.y > 5) {
                isTracking = false;
            }
        }
    }

    void Attack() {
        if (!_isAlive)
            return;

        if(player == null)
            return;

        StartCoroutine(CoAttacking());
    }

    private IEnumerator CoAttacking() {
        while(true) {
            if (!_isAlive)
                break;

            if (player == null)
                break;
            player.Damage(E_atkDamage);

            anim.SetTrigger("Attack");

            yield return E_AttackInterval;
        }
        yield break;
    }

    void Think()
    {
        if (!_isAlive)
            return;

        if (isTracking == false) {
            nextMove = Random.Range(-1, 2) * 10;
        }
        else {
            nextMove = ( targetTransform.position - transform.position ).normalized.x * 10;
        }

        anim.SetFloat("WalkSpeed", Mathf.Abs(nextMove));

        if(nextMove != 0) {
            spriteRenderer.flipX = ( nextMove < 0 );
        }

        float nextThinkTime = Random.Range(2f, 5f);

        Invoke("Think", nextThinkTime);

    }

    void Turn() {
        if (!_isAlive)
            return;

        nextMove = nextMove * ( -1 );
        spriteRenderer.flipX = ( nextMove < 0 );

        CancelInvoke();
        Invoke("Think", 1);
    }

    void Turn(bool direction) {
        if (!_isAlive)
            return;

        if (spriteRenderer.flipX == direction)
            return;

        nextMove *= -1;
        spriteRenderer.flipX = direction;

        CancelInvoke();
        Invoke("Think", 1);
    }

    public void Takedamage(float damage)
    {
        if (_isAlive == false) return;
        Debug.Log($"Damage: {damage}");
        e_curHp = Mathf.Clamp(e_curHp - damage, 0f, e_maxHp);

        if (e_curHp <= 0.0f){
            _isAlive = false;
            anim.SetTrigger("Die");
            Destroy(this.rigid);
            Destroy(this.boxcollider2D);
            Destroy(this.gameObject, 1);
            return;
        }
        anim.SetTrigger("OnHit");
        return;
    }
}