using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAction : MonoBehaviour
{
    public virtual void Activate(ProjectileBase proj, MovableBase target, Vector3 position) { }
}
