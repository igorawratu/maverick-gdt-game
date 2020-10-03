using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject player;
    public GameObject fire_effect;

    // Use this for initialization
    void Start()
    {
       GameObject fe = Instantiate(fire_effect);
       fe.gameObject.transform.position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            Destroy(gameObject);
            return;
        }

        if ((player.transform.position - gameObject.transform.position).magnitude > 100f)
        {
            Destroy(gameObject);
        }
    }
}
