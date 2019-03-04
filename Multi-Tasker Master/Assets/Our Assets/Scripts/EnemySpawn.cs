using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float Timer = 2;
    public GameObject enemy;
    GameObject enemyClone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
            enemyClone = Instantiate(enemy, new Vector3(0, 1, 11), Quaternion.identity);
            Timer = 2f;
        }
    }
}
