using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    //Método cuando hagamos click en el botón
    public void OnButtonClick()
    {
        //Cargamos la escena que se llama así
        SceneManager.LoadScene("Level 1");
    }
}
