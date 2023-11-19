using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    [SerializeField] int damage;
    private void OnBecameInvisible() // Para desactivar el objeto cuando sale de la pantalla
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Para desactivar el proyectil cuando choca con algún objeto que no sea el propio enemigo y el Confiner2D.
        if (collision.gameObject.layer != 7 && collision.gameObject.layer != 2)
        {
            gameObject.SetActive(false);
        }

        if (collision.gameObject.layer == 6)
        {
            // Para acceder a la función Hurt() del jugador cuando es golpeado el proyectil enemigo.
            collision.gameObject.GetComponent<PlayerLife>().Hurt(damage);
        }
    }
}
