using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;

    public float speed = 1f;

    public GameObject death_effect;

    public GameManager game_manager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            Vector3 dir = Vector3.Normalize(player.gameObject.transform.position - gameObject.transform.position);
            gameObject.transform.forward = dir;
            gameObject.transform.position += dir * speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Bullet>() != null)
        {
            game_manager.IncrementScore();
            GameObject e = Instantiate(death_effect);
            e.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }
    }
}
