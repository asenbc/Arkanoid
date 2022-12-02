using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            //Le quitamos una vida al jugador
            GameManager.sharedInstance.lives--;

            //Llamamos al método que resetea la bola que está dentreo del script Ball asociado al objeto de colisión
            collision.gameObject.GetComponent<Ball>().ResetBall();

            //Reproducimos el sonido de perder una vida
            GetComponent<AudioSource>().Play();
        }
    }
}
