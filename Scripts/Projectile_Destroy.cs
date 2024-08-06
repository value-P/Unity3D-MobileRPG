using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Destroy : ProjectileAction
{
    // �浹 ������ Ƚ��
    public int pierce;

    public GameObject prefab;

    public override void Activate(ProjectileBase proj, MovableBase target, Vector3 position)
    {
        pierce--;
        if (pierce < 0 || proj.leftTime <= 0)
        {  // �浹 Ƚ���� ��� �����Ǹ� �߻�ü�� ���ָ鼭 ����Ʈ ��ȯ(�⺻������ 0�̱� ������ �ѹ� �ε����� ����)
            GameObject obj = Instantiate(prefab);
            obj.transform.position = position;
            Destroy(proj.gameObject);
        }
    }
}
