using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //Velocidad de la bola
    public float speed = 10;

    //Referencia a la posición inicial
    public Vector2 ballInit;

    // Start is called before the first frame update
    void Start()
    {
        //Recogemos la posición inicial de la pelota
        ballInit = transform.position;
        
        //La bola se mueve hacia arriba
        //GetComponent<Rigidbody2D>().velocity = Vector2.up * speed; (comento esto para llamar al método de abajo ya que tiene la misma línea
        RestartBallMovement();

        //Llamamos a la corrutina
        StartCoroutine(UpgradeDifficulty());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * El objeto collision del paréntesis contiene la información del choque
     * En particular, nos interesa saber cuando choca con una pala.
     * -collision.gameObject: tiene información del objeto contra el cual he colisionado (raqueta)
     * -collision.transform.position: tiene información de la posición (raqueta)
     * -collision.collider: tiene información del collider (raqueta)
     */
    /*Es un método de Unity que detecta colisión entre dos GO. 
     * Al chocar el objeto contra el que choca le pasa su Colisión por parámetro */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Cada vez que choca la pelota contra algo reproduce su sonido
        GetComponent<AudioSource>().Play();
        //Si la pelota ha colisionado con la pala izquierda
        if (collision.gameObject.name == "Racket")
        {
            //Obtenemos el factor de golpeo, pasándole la posición de la pelota, la posición de la pala, y lo que mide de alto el collider de la pala (es decir, lo que mide la pala)
            float xF = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.x);
            /*Le damos una nueva dirección a la pala
             * En este caso con una Y a la derecha
             * Y nuestro factor de golpeo calculado
             * Normalizado todo el vector a 1, para que la bola no acelere*/
            Vector2 direction = new Vector2(xF, 1).normalized;
            //Le decimos a la bola que salga con esa velocidad previamente calculada
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }

    /*
     * 1 - La bola choca contra la parte más a la derecha de la raqueta
     * 0 - La bola choca contra el centro de la raqueta
     * -1 - La bola choca contra la parte más a la izquierda de la raqueta
    */
    /*Es un método de tipo 3. En este caso le pasamos 3 parámetros:
     * - posición actual de la pelota
     * - posición actual de la pala
     * - ancho de la pala
     * Y el método tal y como le indicamos nos devuelve un variable de tipo float */
    private float HitFactor(Vector2 ballPosition, Vector2 racketPosition, float racketWidth)
    {
        return (ballPosition.x - racketPosition.x) / racketWidth;
    }

    public void ResetBall()
    {
        //Reseteamos la bola a la velocidad inical que tenía 
        speed = 10f;

        //Paramos la velocidad de la pelota
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        //Ponemos a la pelota en su posición original
        transform.position = ballInit;

        //Si aún nos quedan vidas, resetamos el movimiento de la bola y sino no
        if (GameManager.sharedInstance.lives > 0)
        {
            //Esperamos unos segundos y volvemos a decirle a la bola que se mueva
            Invoke("RestartBallMovement", 2.0f);
        }
    }

    //Método para lanzar de nuevo la bola
    void RestartBallMovement()
    {
        //Le volvemos a dar una velocidad hacia arriba a la bola
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
    }

    //Corrutina para aumentar la dificultad del juego acelerando la bola
    //La corrutina se establece fuera del tiempo del bucle de juego, se usa en acciones que deben hacerse en un momento puntual, independientes de lo demás
    private IEnumerator UpgradeDifficulty()
    {
        //Hacemos un bucle siempre verdadero, para que cada segundo aumente un poco la velocidad de la bola
        //Mientras la condición del bucle se cumpla, lo hace. Así le indicamos que la condición siempre será verdad
        while (true)
        {
            //Hace que la corrutina se espere (xf) segundos
            yield return new WaitForSeconds(1.0f);

            //Aumento la velocidad de la bola
            //speed += 0.5f;
            speed *= 1.005f; //Un 0,5% cada vez
        }
    }
}
