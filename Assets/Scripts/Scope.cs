using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public Animator animator;

    public GameObject scopeOverlay;

    public GameObject WeaponCamera;

    public Camera mainCamera;

    private bool isScoped = false;

    public float scopedFOV = 15f;

    private float normalFOV;

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isScoped = !isScoped;
            animator.SetBool("isScoped", isScoped);

            if (isScoped)
                StartCoroutine(OnScoped());
            else
                OnUnScoped();
            

        }


    }

    void OnUnScoped ()
    {
        scopeOverlay.SetActive(false);
        WeaponCamera.SetActive(true);

        mainCamera.fieldOfView = normalFOV;
    }

    IEnumerator OnScoped ()
    {
        yield return new WaitForSeconds(.15f);
        WeaponCamera.SetActive(false);
        scopeOverlay.SetActive(true);
        normalFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = scopedFOV;
    }
}
