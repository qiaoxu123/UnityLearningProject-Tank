using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    // 参数
    private int choice = 1;
    
    // 引用
    public GameObject posOne;
    public GameObject posTwo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) {
            choice = 1;
            transform.position = posOne.transform.position;
        } else if (Input.GetKeyDown(KeyCode.S)) {
            choice = 2;
            transform.position = posTwo.transform.position;
        }
        if (choice == 1 && Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene(1);
        } 
    }
}
