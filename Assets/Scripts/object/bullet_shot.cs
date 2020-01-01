using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_shot : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject effect;
    string cur;
    
    // Start is called before the first frame update
    void Start()
    {
        cur = BtnClick.cur;
    }

    // Update is called once per frame
    void Update()
    {        
       if(cur == "L")
            transform.position += Vector3.left * bulletSpeed * Time.deltaTime;                
       else if(cur == "R")
            transform.position += Vector3.right * bulletSpeed * Time.deltaTime;

    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        BoxCollider2D colider = gameObject.GetComponent<BoxCollider2D>();
        
        //총알이 적과의 충돌이후 다른 스크립트에서 제거되면 colider.enabled는 false로 바뀜
        if (collision.gameObject.tag == "enemy" && colider.enabled == true)
        {            
            Instantiate(effect, transform.position, Quaternion.identity);                                
        }                
    }


}
