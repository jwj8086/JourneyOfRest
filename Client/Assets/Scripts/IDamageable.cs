using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    //������ ũ��, ���ݴ��� ��ġ, ���ݴ��� ����
    void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal);
}
