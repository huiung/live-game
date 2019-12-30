using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnClick : MonoBehaviour
{
     
    public Transform Player;
    public float Speed;  
    int buttonleft, buttonright;
    public float Hp = 10;
    SpriteRenderer spriteRenderer;
    bool isUnBeatTime;
    public static bool deathflag;

    //점프 관련 변수
    float bounce = 4.5f;
    int jump;
    bool isfloor;
    public Rigidbody2D rigid;

    //총알 생성 관련 변수
    public GameObject bullet;   
    float shootDelay = 0.5f;
    float shootTimer = 1.5f;
    bool bulletflag;
    bool firstflag = false;
    bool iscolision;
    public static string cur = "L";

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        isUnBeatTime = false;
        deathflag = false;
        Physics2D.IgnoreLayerCollision(9, 9);        
        jump = 0;
        animator = GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!deathflag)
        {
            //오브젝트의 월드 좌표를 스크린 좌표로 변환
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

            if (buttonleft == 1 && pos.x > 0)  //왼쪽 버튼이 눌려지고 스크린 좌표가 0보다 클때만 이동
            {
                Player.position += Vector3.left * Speed * Time.deltaTime;
            }
            else if (buttonright == 1 && pos.x < Screen.width) //오른쪽 버튼이 눌려지고 스크린 좌표가 스크린 오른쪽 끝좌표 보다 작을떄만 이동
            {
                Player.position += Vector3.right * Speed * Time.deltaTime;
            }
            if (bulletflag == true)
                ShootControl();
        }
    }


    private void FixedUpdate()
    {
        if (jump == 1 && isfloor == true) // 버튼이 눌려지고 바닥이나 적과 닿아 있을때 점프 가능 
        {         
            rigid.AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
        }

        //if (Hp < 0) return;
    }
    

    public void ButtonDown(string type) //버튼 타입을 받음(2방향)
    {
        if (!deathflag)
        {
            switch (type)
            {
                case "A":
                    bulletflag = true;
                    animator.SetBool("Shot", true);
                    break;

                case "L":
                    cur = "L";
                    buttonleft = 1;
                    animator.SetBool("Walking", true);
                    animator.SetFloat("dirX", -1);
                    break;
                case "R":
                    cur = "R";
                    buttonright = 1;
                    animator.SetBool("Walking", true);
                    animator.SetFloat("dirX", 1);
                    break;
                case "J":
                    jump = 1;
                    if(isfloor == true)
                        soundmanager.instance.jumpsound();
                    break;

            }
        }
    }

    public void ButtonUp(string type)
    {
        if (!deathflag)
        {

            switch (type)
            {
                case "A":
                    firstflag = false;
                    bulletflag = false;
                    animator.SetBool("Shot", false);
                    break;

                case "L":
                    buttonleft = 0;
                    animator.SetBool("Walking", false);
                    break;

                case "R":
                    buttonright = 0;
                    animator.SetBool("Walking", false);
                    break;
                case "J":
                    jump = 0;
                    break;
            }
        }
    }


    //BoxColider 부분 player의 발에만 적용되어 있음
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!deathflag) {
            if (collision.gameObject.tag.Equals("enemy") || collision.gameObject.tag.Equals("floor"))
                isfloor = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!deathflag)
        {
            if (collision.gameObject.tag.Equals("floor") || collision.gameObject.tag.Equals("enemy"))
            //부딪힌 객체의 태그를 비교해서 적인지 판단합니다.
            {
                isfloor = false; //점프 중일시 점프 불가능
            }
        }
    }



    //Polygon Colider 부분 istrigger 되어있음
    private void OnTriggerEnter2D(Collider2D other)
    //rigidBody가 무언가와 충돌할때 호출되는 함수 입니다.
    //Collider2D other로 부딪힌 객체를 받아옵니다.
    {

        if (Hp < 0)
        {
            if (!deathflag)
            {
                animator.SetBool("death", true);
                soundmanager.instance.pdeath();                              
                
                deathflag = true;
            }
            else
            {
                animator.SetBool("death", false);               
            }
        }
    }
    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!deathflag)
        {
            if (other.gameObject.tag.Equals("enemy") && !isUnBeatTime)
            {
                Vector2 attackedVelocity = Vector2.zero;
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    attackedVelocity = new Vector2(-1.5f, 3f);
                }
                else attackedVelocity = new Vector2(1.5f, 3f);
                rigid.AddForce(attackedVelocity, ForceMode2D.Impulse); //플레이어 밀어내기

                Hp--;
                isUnBeatTime = true;
                StartCoroutine("UnBeatTime"); //코루틴 이용하여 무적효과
            }
        }
    }

   

    //총알 생성
    void ShootControl()
    {
        if (!deathflag)
        {

            if (firstflag == false)//버튼을 처음 누른 상태라면 바로 발사
            {
                firstflag = true;
                shootTimer = 0.3f;
            }

            if (shootTimer > shootDelay)
            {
                Vector3 pos = transform.position;
                if (cur == "L")
                {
                    pos.x -= 0.8f;
                    Instantiate(bullet, pos, Player.transform.rotation);
                    soundmanager.instance.gunsound();
                }
                else if (cur == "R")
                {
                    pos.x += 0.8f;
                    Instantiate(bullet, pos, Player.transform.rotation);
                    soundmanager.instance.gunsound();
                }

                shootTimer = 0;
            }
            shootTimer += Time.deltaTime;
        }
    }

    IEnumerator UnBeatTime()
    {
        int countTime = 0;
        while (countTime < 10) //피격당하면 총 2초동안 무적상태
        {
            if (countTime % 2 == 0)
                spriteRenderer.color = new Color32(255, 255, 255, 90);
            else
                spriteRenderer.color = new Color32(255, 255, 255, 100);

            yield return new WaitForSeconds(0.2f);
            countTime++;
        }

        spriteRenderer.color = new Color32(255, 255, 255, 255);
        isUnBeatTime = false;

        yield return null;
    }


}
