
using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    // Test
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public float fireRate = 10f;
    public int maxAmmo = 0;
    public int currentAmmo;
    public float reloadTime = 2f;
    public Animator animator;
    public bool reloadingNow = false;

    public AudioSource shootAudio;

    public bool isAutomaticFire = false;


    private float nextTimeToFire = 0f;
    // Update is called once per frame

    public void Start()
    {
        // Reload current ammo to maxAmmo
        StartCoroutine(Reload(false));
    }


    void Update()
    {

        bool inputToFire = false;

        if (isAutomaticFire)
        {
            inputToFire = Input.GetButton("Fire1");
        } else
        {
            inputToFire = Input.GetButtonDown("Fire1");
        }

        // Check if gun has been fired
        if (inputToFire && Time.time >= nextTimeToFire)
        {
            // Check if gun has ammo
            if (currentAmmo > 0)
            {
                // Play Shoot Audio
                ShootAudio();
                nextTimeToFire = Time.time + 1f / fireRate;
                StartCoroutine(Shoot());
            }
            else
            {
                StartCoroutine(Reload());
            }

        }

        //if (Input.GetButton("Reload"))
        //{
        //    Reload();
        //}
    }

    private IEnumerator Reload(bool showAnimation = true)
    {

        if (!showAnimation)
        {
            currentAmmo = maxAmmo;
        }

        if (!reloadingNow && showAnimation)
        {
            reloadingNow = true;

            Debug.Log("Reloading...");

            if (animator != null)
            {
                animator.SetBool("isReloading", true);
            }

            // RELOAD.Play();
            yield return new WaitForSeconds(reloadTime - .25f);

            if (animator != null)
            {
                animator.SetBool("isReloading", false);
            }
            yield return new WaitForSeconds(.50f);
            currentAmmo = maxAmmo;

            reloadingNow = false;
            Debug.Log("Reload Complete...");

        }
    }


    private IEnumerator Shoot()
    {
        RaycastHit hit;

        // Reduce ammo by 1
        currentAmmo--;

        // Play Muzzle Animation
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // Trigger Recoil Animation
        if (animator != null)
        {
            animator.SetBool("isRecoil", true);
        }

        // Check if Raycast detects hit
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            EnemyDirector target = hit.transform.GetComponent<EnemyDirector>();

            if (target != null)
            {
                target.gameObject.GetComponent<Health>().TakeDamage(damage);

                if (impactEffect != null)
                {
                    Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                }
            }
        }

        // Come back once the recoil animation is done and next fire available
        yield return new WaitForSeconds(1f / fireRate);
        if (animator != null)
        {
            animator.SetBool("isRecoil", false);
        }
    }

    private void  ShootAudio()
    {
        if (shootAudio != null)
        {
            shootAudio.PlayOneShot(shootAudio.clip);
        }
    }
}

