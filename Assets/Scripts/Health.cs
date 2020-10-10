using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float maximumHealth = 100f;
    public AudioSource deathAudioSource;

    public float currentHealth = 0f;

    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maximumHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsAlive()
    {
        return isAlive;
    }


    public void TakeDamage(float amount)
    {
        this.currentHealth -= amount;

        if (this.currentHealth <= 0)
        {
            StartCoroutine(Die());
        }

    }

    public IEnumerator Die()
    {
        if (this.currentHealth <= 0f && this.isAlive)
        {
            this.isAlive = false;

            float waitTime = 0;

            if (deathAudioSource != null)
            {
                waitTime = deathAudioSource.clip.length - .5f;
                deathAudioSource.PlayOneShot(deathAudioSource.clip);
            }

            yield return new WaitForSeconds(waitTime);

            Destroy(gameObject);


        }

    }

}
