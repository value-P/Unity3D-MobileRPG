using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    Animator anim;

    public Camera mainCam;
    public Camera subCam;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("isOpen");
    }

    public void ShowSubCam()
    {
        mainCam.gameObject.SetActive(false);
        subCam.gameObject.SetActive(true);
    }

    public void ShowMainCam()
    {
        mainCam.gameObject.SetActive(true);
        subCam.gameObject.SetActive(false);

        Destroy(this);
    }


}
