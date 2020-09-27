using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy_prefab;

    public float spawn_rate_max = 10f;
    public float enemy_speed_max = 2f;

    public float spawn_rate_initial = 1f;
    public float enemy_speed_initial = 0.5f;

    public float enemy_speed_gain = 0.01f;
    public float spawnrate_gain = 0.01f;

    public int player_score = 0;

    public float enemy_spawn_distance = 30f;

    public Text score;
    public Text gameover;

    private float spawn_timer = 0f;

    private void Awake()
    {
        player.GetComponent<Player>().game_manager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawn_timer += Time.deltaTime;

        if(spawn_timer > spawn_rate_initial)
        {
            spawn_timer -= spawn_rate_initial;

            GameObject enemy = Instantiate(enemy_prefab);

            Vector3 player_offset = Vector3.Normalize(new Vector3(UnityEngine.Random.value - 0.5f, 0f, UnityEngine.Random.value - 0.5f)) * enemy_spawn_distance;
            enemy.transform.position = player.transform.position + player_offset;
            Enemy e = enemy.GetComponent<Enemy>();
            e.player = player;
            e.speed = enemy_speed_initial;
            e.game_manager = this;
        }

        spawn_rate_initial = Mathf.Min(spawn_rate_max, spawn_rate_initial + spawnrate_gain * Time.deltaTime);
        enemy_speed_initial = Mathf.Min(enemy_speed_max, enemy_speed_initial + enemy_speed_gain * Time.deltaTime);
    }

    public void IncrementScore()
    {
        player_score++;
        score.text = player_score.ToString();
    }

    public void EndGame()
    {
        gameover.text = "Game Over!\nYour score is " + player_score;
    }
}
