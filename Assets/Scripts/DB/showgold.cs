using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showgold : MonoBehaviour
{
    public Text money;
    public Text damage;
    public Text Hp;
    public Text jumpcnt;
    // Start is called before the first frame update
    void Start()
    {
        money.text += " " + database.gold.ToString();
        damage.text += " " + database.damage.ToString();
        Hp.text += " " + database.Hp.ToString();
        jumpcnt.text += " " + database.jumpcnt.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
