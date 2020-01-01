using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradeText : MonoBehaviour
{
    public Text text1;
    public Text text2;
    public Text text3;
    public Text text1cost;
    public Text text2cost;
    public Text text3cost;
    public Text gold;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        text1.text = "CURRENT DAMAGE: " + database.damage.ToString();
        text2.text = "CURRENT HP: " + database.Hp.ToString();
        text3.text = "upgrade jumpcnt: " + database.jumpcnt.ToString();
        text1cost.text = (database.damage * 500).ToString();
        text2cost.text = (database.Hp * 250).ToString();
        text3cost.text = (database.jumpcnt * 500).ToString();
        gold.text = "Gold: " + database.gold.ToString();
    }
    
    void Update()
    {
        
    }
}
