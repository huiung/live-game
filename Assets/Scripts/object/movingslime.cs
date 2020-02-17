using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movingslime : MonoBehaviour
{
    public Transform Player;
    public float Speed;    
    public float Hp;
    public float power;
    public float duration = 5f;
    public float scalex = 0.25f;
    public float scaley = 0.25f;
    public float bounce = 0f;            


    private Rigidbody2D e;
    private float curHp;
    private float curSpeed;
    private int movementflag;
    

    // Start is called before the first frame update

    void Start()
    {
        Vector3 cur;
        cur.x = Player.transform.position.x;
        if (cur.x >= transform.position.x) //플레이어 위치를 통해 처음 내 방향 파악
            movementflag = 1;
        else
            movementflag = 0;

        curHp = Hp-0.5f;        
        e = gameObject.GetComponent<Rigidbody2D>();
        Player = GameObject.Find("player").transform;
        StartCoroutine(moving(duration)); //코루틴 통해 좌우 반복 이동        
        StartCoroutine("jump");
    }

    // Update is called once per frame
    void Update()
    {
        if (BtnClick.deathflag == false)
        {
            Vector3 cur;
            cur.x = Player.transform.position.x;
            
            if (curHp > Hp) //한대 맞으면 유저를 따라감
            {

                if (cur.x >= transform.position.x)
                {
                    transform.localScale = new Vector3(scalex, scaley, 1);
                    transform.position += Vector3.right * Speed * Time.deltaTime;
                }
                else if (cur.x < transform.position.x - 0.1f)
                {
                    transform.localScale = new Vector3(-scalex, scaley, 1);
                    transform.position += Vector3.left * Speed * Time.deltaTime;
                }

                if (Hp < 0)
                {
                    movingcamera.score += 30; //slime은 30원
                    Destroy(gameObject);
                }
            }
            else // 맞기전 초기에는 그냥 좌우로 왔다갔다 (맵 끝에서 끝까지)
            {
                Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);                

                if(movementflag == 0) //왼쪽 3초 이동
                {
                    if (pos.x > 0)
                    {
                        transform.localScale = new Vector3(-scalex, scaley, 1);
                        transform.position += Vector3.left * Speed * Time.deltaTime;
                    }
                }
                else //오른쪽 3초이동
                {
                    if (pos.x < Screen.width)
                    {
                        transform.localScale = new Vector3(scalex, scaley, 1);
                        transform.position += Vector3.right * Speed * Time.deltaTime;
                    }
                }
                

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
                    Hp -= database.damage;
                    Vector2 attackedVelocity = Vector2.zero;
                    if (other.gameObject.transform.position.x > transform.position.x)
                    {
                        attackedVelocity = new Vector2(-1f, 0f);
                    }
                    else attackedVelocity = new Vector2(1f, 0f);
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

    IEnumerator moving(float time)
    {        
        yield return new WaitForSeconds(time);
        if (movementflag == 0)
            movementflag = 1;
        else
            movementflag = 0;
        StartCoroutine(moving(duration));        
    }

    IEnumerator jump()
    {
        e.AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);        
        StartCoroutine("jump");
    }

}
