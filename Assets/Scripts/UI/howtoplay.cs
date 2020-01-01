using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class howtoplay : MonoBehaviour
{
    // Start is called before the first frame update    
    private GameObject panel;

    private void Awake()
    {
        panel = GameObject.Find("Canvas2").transform.Find("Panel").gameObject;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void show()
    {
        soundmanager.instance.btnsound();
        panel.SetActive(true);
    }

    public void exit()
    {
        panel.SetActive(false);
    }
}
