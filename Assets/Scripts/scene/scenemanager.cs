using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenemanager : MonoBehaviour
{

    static string prescene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MainScene")
        {
            if (SceneManager.GetActiveScene().name == "stage")
            {
                StartCoroutine("start", "MainScene");
            }
            else if (SceneManager.GetActiveScene().name == "Upgrade")
            {
                StartCoroutine("start", "stage");
            }
        }
    }

       

    public void homebtn()
    {
        soundmanager.instance.btnsound();
        StartCoroutine("start", "MainScene");
    }

    public void startbtn()
    {
        soundmanager.instance.btnsound();
        StartCoroutine("start", "stage");        
    }

    public void stagebtn(int stage)
    { 
        soundmanager.instance.btnsound();
        SceneManager.LoadScene("stage" + stage);
    }

    public void upgradebtn()
    {
        soundmanager.instance.btnsound();
        StartCoroutine("start", "Upgrade");
    }
    IEnumerator start(string scene)
    {        
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(scene);
    }
}
