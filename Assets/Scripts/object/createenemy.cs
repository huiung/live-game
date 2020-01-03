using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createenemy : MonoBehaviour
{
    // Start is called before the first frame update
    float time;
    public GameObject enemy;
    public int gentime = 5;
    public int unitnum = 5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
               
       if (time > gentime && gentime <= unitnum*5)
       {
            spawn();
            gentime+=5;
        }
        
    }

    void spawn()
    {
        Vector3 pos = new Vector3(-7.70375f, -4.878784f, 0);
        Vector3 pos2 = new Vector3(14.66625f, -4.878784f, 0);
        Instantiate(enemy, pos, Quaternion.identity);
        Instantiate(enemy, pos2, Quaternion.identity);
    }
}
