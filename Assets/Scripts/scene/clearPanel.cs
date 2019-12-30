using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clearPanel : MonoBehaviour
{
    public Text money;
    GameObject panel;
    bool flag;
    // Start is called before the first frame update

    private void Awake()
    {
        flag = false;
        panel = GameObject.Find("Canvas").transform.Find("clearPanel").gameObject;
    }
    void Start()
    {
        StartCoroutine("starttime");
    }

    // Update is called once per frame
    void Update()
    {
        if (null == GameObject.Find("fireball(Clone)") && flag)
        {

            StartCoroutine("cleartime");
        }
        
    }        

    IEnumerator starttime()
    {
        yield return new WaitForSeconds(10.0f);
        flag = true;
    }

    IEnumerator cleartime()
    {
        yield return new WaitForSeconds(0.5f);
        int score = movingenemy.score;
        panel.SetActive(true);
        money.text = "+" + score.ToString();
    }
}
