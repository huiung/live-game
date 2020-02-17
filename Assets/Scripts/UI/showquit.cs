using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showquit : MonoBehaviour
{
    // Start is called before the first frame update    
    private GameObject panel;

    private void Awake()
    {
        panel = GameObject.Find("Canvas2").transform.Find("quit").gameObject;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            panel.SetActive(true);
        }
    }

    public void quit()
    {
        Application.Quit();
    }

    public void exit()
    {
        panel.SetActive(false);
    }
}
