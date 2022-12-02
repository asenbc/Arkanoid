using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //Librería para poder acceder a los TextMeshPro
using UnityEngine.UI; //Librería para acceder a los componentes de la UI (interfaz)
using UnityEngine.SceneManagement; //Para cambiar entre escenas

public class GameManager : MonoBehaviour
{
    //Creamos un Singleton
    public static GameManager sharedInstance = null;

    //Imágenes de las vidas
    public Image live1, live2, live3;

    //Empezamos el juego teniendo 3 vidas
    public int lives = 3;

    //Referenciamos la imagen de Game Over
    public TextMeshProUGUI gameOver, win;

    private void Update()
    {
        //Controlamos las imágenes de las vidas, dependiendo de cuantas nos quedan
        //Si nos quedan menos de 3 vidas
        //if (lives < 3)
        //{
        //    //Desactivamos la imagen de la vida 3
        //    live3.enabled = false;
        //}
        ////Si nos quedan menos de 2 vidas
        //if (lives < 2)
        //{
        //    live2.enabled = false;
        //}
        //if (lives < 1)
        //{
        //    live2.enabled = false;
        //}

        //Nos damos cuenta de que al ver el valor de una sola variable, podemos sustituir lo de arriba por un switch
        switch (lives)
        {
            //En el caso en el que las vidas sean 3
            case 3: //Activamos todas las imágenes
                live3.enabled = true;
                live2.enabled = true;
                live1.enabled = true;
                break;
            //En el caso en el que las vidas sean 2
            case 2:
                //Desactivamos la imagen de la vida 3
                live3.enabled = false;
                break;
            case 1:
                live2.enabled = false;
                break;
            case 0:
                live1.enabled = false;
                //Activamos la imagen de Game Over
                gameOver.gameObject.SetActive(true);
                break;
            default:
                live3.enabled = false;
                live2.enabled = false;
                live1.enabled = false;
                break;
        }

        //Vamos a contar cuantos bloques hay en esta partida
        //Creamos un array donde meter todos los bloques que tenemos en esta partida
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        //Si el tamaño del array es 0 (osea se ha quedado vacío)
        if (blocks.Length == 0)
        {
            //Aparece el letrero You Won
            win.enabled = true;
            win.gameObject.SetActive(true);
            Invoke("GoToNextScene", 1f);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    //Método para cambiar entre escenas
    private void GoToNextScene()
    {
        //Cambiamos de escena yendo a la siguiente que tengamos preparada en la Build
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
