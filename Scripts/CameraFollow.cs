using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 normalOffset;
    public float angle;
    public Transform target;

    void Update()
    {
        if (target == null) { target = GameManager.Instance.player.transform; }

        if (!target || target.gameObject.activeInHierarchy == false)
        {
            transform.position = new Vector3(0, 6f, 0);
            transform.localEulerAngles = new Vector3(angle, 0, 0);
        }
        else
        {
            transform.localEulerAngles = new Vector3(angle, 0, 0);
            transform.position = target.transform.position + normalOffset;
        }
    }
}
