using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deathPanel : MonoBehaviour
{
    public Text money;
    GameObject deathpanel;
    bool oneflag;
    // Start is called before the first frame update

    private void Awake()
    {
        oneflag = true;
        deathpanel = GameObject.Find("Canvas").transform.Find("deathPanel").gameObject;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BtnClick.deathflag == true && oneflag)
        {
            oneflag = false;
            StartCoroutine("deathtime");
        }

    }

    IEnumerator deathtime()
    {       
        database d = new database();
        int value = movingcamera.score + database.gold;
        d.DatabaseUpdate("gold", value.ToString());
        database.gold = value;
        yield return new WaitForSeconds(0.5f);
        int score = movingcamera.score;
        deathpanel.SetActive(true);
        money.text = "+" + score.ToString();
    }
}
