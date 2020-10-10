using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public GameObject player;
    public GameObject bigChongus;

    // Start is called before the first frame update
    void Start()
    {
        for (int counter = 0; counter < 2; counter++)
        {
            Debug.Log("Creating Chongus...");
            var newChongus = Instantiate(bigChongus, player.transform.position, Quaternion.identity) as GameObject;
            newChongus.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
