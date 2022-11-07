using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    //데미지 크기, 공격당한 위치, 공격당한 방향
    void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal);
}
