using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenemanager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    IEnumerator start(string scene)
    {        
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(scene);
    }
}
