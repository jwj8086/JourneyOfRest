using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    //해야 할 것: 트리거, 유저가 근처에 있을 시 공격 모션 출력
    Rigidbody2D rigid;
    public Animator anim;
    SpriteRenderer spriteRenderer;
    public BoxCollider2D boxcollider2D;
    [HideInInspector]public PlayerController player;

    public float nextMove;
    public Transform targetTransform;
    public float sight = 30;
    public bool isTracking = false;

    //나중에 피해 추가
    public int E_atkDamage;
    public bool E_Attack;
    public float E_atkCoolTime = 2.0f;
    public float E_atkDistance;
    private WaitForSeconds E_AttackInterval;

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

        Invoke("Think", 0);
    }

    void FixedUpdate() {
        Move();
    }

    void Move() {
        if(E_Attack)
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
        if(player == null)
            return;

        StartCoroutine(CoAttacking());
    }

    private IEnumerator CoAttacking() {
        while(true) {
            if(player == null)
                break;
            player.Damage(E_atkDamage);

            anim.SetTrigger("Attack");

            yield return E_AttackInterval;
        }
        yield break;
    }

    void Think() {
        if(isTracking == false) {
            nextMove = Random.Range(-1, 2) * 9;
        }
        else {
            nextMove = ( targetTransform.position - transform.position ).normalized.x * 9;
        }

        anim.SetFloat("WalkSpeed", nextMove < 0 ? nextMove * -1 : nextMove);

        if(nextMove != 0) {
            spriteRenderer.flipX = ( nextMove < 0 );
        }

        float nextThinkTime = Random.Range(2f, 5f);

        Invoke("Think", nextThinkTime);

    }

    void Turn() {
        nextMove = nextMove * ( -1 );
        spriteRenderer.flipX = ( nextMove < 0 );

        CancelInvoke();
        Invoke("Think", 1);
    }

    void Turn(bool direction) {
        if(spriteRenderer.flipX == direction)
            return;

        nextMove *= -1;
        spriteRenderer.flipX = direction;

        CancelInvoke();
        Invoke("Think", 1);
    }
}


