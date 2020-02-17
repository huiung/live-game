using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    // Start is called before the first frame update    
    private GameObject panel;
    private GameObject deathpanel;
    private void Awake()
    {
        panel = GameObject.Find("Canvas").transform.Find("Panel").gameObject;
        deathpanel = GameObject.Find("Canvas").transform.Find("deathPanel").gameObject;
    }
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Btnclick("Pause");
        }
    }

    public void Btnclick(string type)
    {        
        if (type == "Pause")
        {
            soundmanager.instance.btnsound();
            Time.timeScale = 0;
            panel.SetActive(true);            
        }
        else if(type == "Continue")
        {
            soundmanager.instance.btnsound();
            Time.timeScale = 1;
            panel.SetActive(false);            
        }
    }
    
    public void RetryBtn(string stage)
    {
        Time.timeScale = 1;
        soundmanager.instance.btnsound();        
        StartCoroutine(retry(stage)); //매개변수 있을시 코루틴 사용법 StartCoroutine( "함수", 매개변수 ); 도가능        
    }

    public void QuitBtn()
    {
        Time.timeScale = 1; //timeScale을 원래대로 돌려놓고 나가야함 이게 0이면 씬전환도 안됨
        soundmanager.instance.btnsound();        
        StartCoroutine(retry());        
    }

    IEnumerator retry(string stage="")
    {        
        yield return new WaitForSeconds(0.5f);
        deathpanel.SetActive(false);
        SceneManager.LoadScene("stage" + stage);        
    }
}
