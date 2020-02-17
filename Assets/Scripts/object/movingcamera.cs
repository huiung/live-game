using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingcamera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target; // 카메라가 따라갈 대상
    public float movespeed; //카메라 속도
    public static int score; //돈
    private Vector3 targetPosition;
    void Start()
    {
        score = 0;
        targetPosition.Set(target.transform.position.x, this.transform.position.y, this.transform.position.z); // 타겟 좌표 저장
        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, movespeed * Time.deltaTime); // 카메라 이동
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {       
        if (target.gameObject != null)
        {
            if (target.transform.position.x < 2.6f || target.transform.position.x > 5.6f)
            {
                MoveLimit(2.6f, 5.6f);                
            }
            else
            {
                targetPosition.Set(target.transform.position.x, transform.position.y, transform.position.z); // 타겟 좌표 저장
                transform.position = Vector3.Lerp(transform.position, targetPosition, movespeed * Time.deltaTime); // 카메라 이동
            }
        }
    }


    void MoveLimit(float start, float end) //이동 불가제한
    {
        Vector3 temp;
        temp.x = Mathf.Clamp(transform.position.x, start, end);
        temp.y = transform.position.y;
        temp.z = transform.position.z;

        transform.position = temp;
    }
}
