using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movingenemy : MonoBehaviour
{
    public Transform Player;
    public float Speed;
    private float curSpeed;
    public float Hp = 5;
    public float power = 1;
    Rigidbody2D e;    
    public static int score;

    // Start is called before the first frame update
    
    void Start()
    {
        score = 0;
        e = gameObject.GetComponent<Rigidbody2D>();
        Player = GameObject.Find("player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (BtnClick.deathflag == false)
        {
            Vector3 cur;
            cur.x = Player.transform.position.x;

            if (cur.x >= transform.position.x)
            {
                transform.localScale = new Vector3(0.25f, 0.25f, 1);
                transform.position += Vector3.right * Speed * Time.deltaTime;
            }
            else if (cur.x < transform.position.x - 0.1f)
            {
                transform.localScale = new Vector3(-0.25f, 0.25f, 1);
                transform.position += Vector3.left * Speed * Time.deltaTime;
            }

            if (Hp < 0)
            {
                score += 20; //fireball은 20원
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    //rigidBody가 무언가와 충돌할때 호출되는 함수 입니다.
    //Collider2D other로 부딪힌 객체를 받아옵니다.
    {
        
            if (other.gameObject.tag.Equals("Player"))
            //부딪힌 객체의 태그를 비교해서 적인지 판단합니다.
            {
                curSpeed = Speed;
                Speed = 0.01f;                
                e.constraints = RigidbodyConstraints2D.FreezeAll; // 플레이어와 충돌시 움직임 제한
            }

            if (other.gameObject.tag.Equals("bullet"))
            {

                BoxCollider2D bullet = other.gameObject.GetComponent<BoxCollider2D>();                

                if (bullet.enabled == true)
                {
                    Hp--;
                    Vector2 attackedVelocity = Vector2.zero;
                    if (other.gameObject.transform.position.x > transform.position.x)
                    {
                        attackedVelocity = new Vector2(-1f, 1.5f);
                    }
                    else attackedVelocity = new Vector2(1f, 1.5f);
                    e.constraints = RigidbodyConstraints2D.None; //피격당하면 Y freeze 잠시 해제
                    e.AddForce(attackedVelocity, ForceMode2D.Impulse); //피격당한 적 밀어내기                    
                    Destroy(other.gameObject); //가장 먼저 총알과 충돌한 enemy의 체력을 깎고 총알을 파괴함
                }
            }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
            if (collision.gameObject.tag.Equals("Player"))
            //부딪힌 객체의 태그를 비교해서 적인지 판단합니다.
            {
                Speed = curSpeed;
                e.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY; //X축 움직임 제한 해제 Y/Z축 움직임은 기본적으로 제한상태
        }
        
        
    }
}
