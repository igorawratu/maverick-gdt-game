using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroyer : MonoBehaviour
{
    public float kill_timer = 1f;

    private float current_timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        current_timer += Time.deltaTime;
        if(current_timer > kill_timer)
        {
            Destroy(gameObject);
        }
    }
}
