using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    public float damageAmount = 5f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsEnemy(gameObject) != IsEnemy(other.gameObject))
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damageAmount);
        }
    }


    private bool IsEnemy(GameObject gameobject)
    {
        return gameObject.GetComponent<PlayerDirector>() == null;
    }
}
