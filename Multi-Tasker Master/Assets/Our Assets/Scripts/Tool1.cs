using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision enemy)
    {
        if(enemy.gameObject.tag == "Enemy")
        {
            Destroy(enemy.gameObject);
            //You killed my father, prepare to die!
        }
    }

}
