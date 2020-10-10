
using System.Collections;
using UnityEngine;

public class EnemyDirector : MonoBehaviour
{

    public float health = 50f;
    public CharacterController enemyController;
    public Transform playerTransform;
    public Transform enemyTransform;
    public AudioSource closeRangeAudio;
    public AudioSource dieAudio;
    public float closeRangeDistance = 40f;
    public float distance;
    public float speed = 12f;
    public float gravity = -9.81f;
    public bool isDead = false;
    Vector3 velocity;

    // Update is called once per frame
    public void Update()
    {
        // Apply Audio for Enemy
        Audio();

        float x = 0;
        float z = 0;


        // Forward / Back Movement
        if (playerTransform.position.x >= enemyTransform.position.x + 20)
        {
            z = 3;
        }
        if (playerTransform.position.x < enemyTransform.position.x + 20)
        {
            z = 1;
        }

        // Move enemy in player direction
        Vector3 move = transform.right * x + transform.forward * z;
        enemyController.Move(move * speed * Time.deltaTime);

        // Rotate enemy towards player
        Vector3 direction = playerTransform.position - enemyTransform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        enemyTransform.rotation = Quaternion.Lerp(enemyTransform.rotation, rotation, speed * Time.deltaTime);

        // Apply Gravity
        velocity.y += gravity * Time.deltaTime;
        enemyController.Move(velocity * Time.deltaTime);


    }

    // Plays, Stops or Adjust Volume for Enemy Proximity Audio
    private void Audio()
    {
        if (closeRangeAudio != null && !isDead)
        {
            distance = Vector3.Distance(playerTransform.position, enemyTransform.position);

            if (distance < closeRangeDistance && !closeRangeAudio.isPlaying)
            {
                closeRangeAudio.Play();
            }
            else if (distance > closeRangeDistance && !closeRangeAudio.isPlaying)
            {
                closeRangeAudio.Stop();
            }

            // Adjust Audio by distance
            if (closeRangeAudio.isPlaying)
            {
                closeRangeAudio.volume = 1 - (distance / closeRangeDistance);
            }
        }
    }

    // Record Damage from hit
    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die()
    {
        if (this.health <= 0f && !isDead)
        {
            this.isDead = true;

            if (closeRangeAudio != null && closeRangeAudio.isPlaying)
            {
                this.closeRangeAudio.Stop();
            }

            if (this.dieAudio != null)
            {
                this.dieAudio.Play();
            }


            //gameObject.GetComponent<Renderer>().enabled = false;
            //yield return null;

            //if (renderer != null)
            //{
            //
            //    
            //    Debug.Log("Setting transparent...");
            //    var colorToAdjust = renderer.material.color;
            //    colorToAdjust.a = 0f;
            //    renderer.material.color = colorToAdjust;
            //
            //}

            float waitTime = 0;

            if (dieAudio != null)
            {
                waitTime = dieAudio.clip.length;
            }


            yield return new WaitForSeconds(waitTime);
            if (dieAudio != null)
            {
                this.dieAudio.Stop();
            }
            Destroy(gameObject);

            
        }

    }

}








