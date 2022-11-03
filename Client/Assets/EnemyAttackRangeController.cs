using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRangeController : MonoBehaviour
{
    [SerializeField] private EnemyMove enemyController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        enemyController.Player = collision.gameObject.GetComponent<PlayerController>();
        Debug.Log("Player����");
        //TODO: �߰����� ���� �Լ� �ۼ� �ʿ�
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        Debug.Log("Player����");
        enemyController.Player = null;
    }


}
