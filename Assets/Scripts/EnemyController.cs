using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Soldier[] Enemies;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        // foreach (Soldier enemy in Enemies)
        // {
        //     if(enemy.IsActive) 
        //         return;
        // }
        // int soldier = Random.Range(0, Enemies.Length);
        // Enemies[soldier].Activate();
    }
}
