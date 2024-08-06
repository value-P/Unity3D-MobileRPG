using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Destroy : ProjectileAction
{
    // 충돌 가능한 횟수
    public int pierce;

    public GameObject prefab;

    public override void Activate(ProjectileBase proj, MovableBase target, Vector3 position)
    {
        pierce--;
        if (pierce < 0 || proj.leftTime <= 0)
        {  // 충돌 횟수가 모두 소진되면 발사체를 없애면서 이펙트 소환(기본적으로 0이기 때문에 한번 부딪히면 실행)
            GameObject obj = Instantiate(prefab);
            obj.transform.position = position;
            Destroy(proj.gameObject);
        }
    }
}
