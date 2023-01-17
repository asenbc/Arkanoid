using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int durability;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * Método para saber cuando algo choca contra el bloque, en nuestro caso
     * el único objeto que se mueve por la pantalla es la bola, luego solamente
     * puede ser ella la que choque contra los bloques
    */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Detectamos que sea la pelota el objeto contra el que hemos colisionado
        if (collision.gameObject.name == "Ball")
        {
            durability--;
            if (durability <= 0)
            {
                GameManager.sharedInstance.points += 5;
                Destroy(this.gameObject);
            }
        }
    }
}
