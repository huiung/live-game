using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgrade : MonoBehaviour
{
    private GameObject panel;
    private GameObject panel2;
    private static int cost;
    private static string curtype;

    public Text text1;
    public Text text2;
    public Text text3;
    public Text text1cost;
    public Text text2cost;
    public Text text3cost;
    public Text gold;


    private void Awake()
    {        
        panel = GameObject.Find("Canvas").transform.Find("Panel").gameObject;
        panel2 = GameObject.Find("Canvas").transform.Find("Panel2").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //upgrade 눌렀을때
    public void btn_click(string type) //type을 받아 각경우 나눔
    {
        soundmanager.instance.btnsound();
        curtype = type;

        if (type == "damage")        
            cost = database.damage * 500;                    
        else if (type == "Hp")
            cost = database.Hp * 250;
        else if (type == "jumpcnt")
            cost = database.jumpcnt * 500;

        Debug.Log(cost);
        //업그레이드 비용이 없으면 비용이 부족하다는 panel을 띄움
        if (database.gold < cost)
        {
            panel2.SetActive(true);
        }
        else
        {
            //업그레이드 비용이 골드보다 적을때
            panel.SetActive(true);
        }

    }

    //panel뜬 이후 창에서 yes / no 선택시
    public void choose_yes()
    {        
        int money = database.gold - cost;        
        int value = 0;        
        database.gold = money;        
        if (curtype == "damage") //값을 갱신하고 db에 update
        {
            value = database.damage + 1;
            database.damage = value;
        }
        else if (curtype == "Hp")
        {
            value = database.Hp + 1;
            database.Hp = value;
        }
        else if (curtype == "jumpcnt")
        {
            value = database.jumpcnt + 1;
            database.jumpcnt = value; 
        }

        database d = new database();
        d.DatabaseUpdate("gold", money.ToString());
        d.DatabaseUpdate(curtype, value.ToString());

        text1.text = "CURRENT DAMAGE: " + database.damage.ToString();
        text2.text = "CURRENT HP: " + database.Hp.ToString();
        text3.text = "upgrade jumpcnt: " + database.jumpcnt.ToString();
        text1cost.text = (database.damage * 500).ToString();
        text2cost.text = (database.Hp * 250).ToString();
        text3cost.text = (database.jumpcnt * 500).ToString();
        gold.text = "Gold: " + database.gold.ToString();

        panel.SetActive(false);
        soundmanager.instance.btnsound();
    }

    public void choose_no()
    {
        soundmanager.instance.btnsound();
        panel.SetActive(false);
    }

    public void back()
    {
        soundmanager.instance.btnsound();
        panel2.SetActive(false);
    }
}
