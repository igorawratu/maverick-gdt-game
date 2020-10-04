using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float max_shooting_chargetime = 1f;
    public float max_bullet_force = 1f;
    public float bullet_force_gain = 1f;
    public float movement_speed = 1f;
    public float rotation_speed = 5f;
    public float min_bullet_force = 0.25f;

    public GameObject gun;
    public GameObject bullet_prefab;
    public GameObject death_effect;
    public GameObject cam;

    public GameManager game_manager;

    public Slider slider;

    private float current_bullet_force_ = 0f;
    private Vector3 last_mouse_pos = Vector3.zero;

    void Start()
    {
        last_mouse_pos = Input.mousePosition;
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        //version 1
        /*if (Input.GetMouseButtonDown(0))
        {
            ShootV1();
        }*/

        //version 2
        
        if (Input.GetMouseButton(0))
        {
            current_bullet_force_ = Mathf.Min(current_bullet_force_ + Time.deltaTime * bullet_force_gain, max_bullet_force);
        }

        if (Input.GetMouseButtonUp(0))
        {
            ShootV2();
        }

        slider.value = current_bullet_force_ / max_bullet_force;
    }

    private void Move()
    {
        Vector3 movement = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movement += gameObject.transform.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movement -= gameObject.transform.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement -= gameObject.transform.right;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movement += gameObject.transform.right;
        }

        movement *= movement_speed * Time.deltaTime;
        gameObject.transform.position += movement;

        Vector3 curr_mouse_pos = Input.mousePosition;
        Vector3 mouse_delta = curr_mouse_pos - last_mouse_pos;
        last_mouse_pos = curr_mouse_pos;

        float y_rot = mouse_delta.x * Mathf.PI * Time.deltaTime * rotation_speed;
        gameObject.transform.Rotate(Vector3.up, y_rot, Space.World);
    }

    private void ShootV1()
    {
        Shoot(max_bullet_force);
    }

    private void ShootV2()
    {
        if(current_bullet_force_ > min_bullet_force)
        {
            Shoot(current_bullet_force_);
        }
        current_bullet_force_ = 0f;
    }

    private void Shoot(float power)
    {
        Vector3 direction = gun.transform.forward;

        GameObject bullet = Instantiate(bullet_prefab);
        bullet.transform.position = gun.transform.position;
        bullet.transform.localScale = new Vector3(Mathf.Max(0.05f, power / 50f), Mathf.Max(0.05f, power / 50f), Mathf.Max(0.05f, power / 50f));
        bullet.GetComponent<Rigidbody>().velocity = direction * power;
        bullet.GetComponent<Bullet>().player = gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Enemy>() == null)
        {
            return;
        }

        Vector3 p = cam.transform.position;
        cam.transform.parent = null;
        cam.transform.position = p;
        ScreenShake shake = cam.GetComponent<ScreenShake>();
        shake.originalPos = p;
        shake.shakeAmount = 1f;
        shake.shakeDuration = 1f;
        game_manager.EndGame();
        GameObject e = Instantiate(death_effect);
        e.transform.position = gameObject.transform.position;
        Destroy(gameObject);
    }
}
