using UnityEngine;

public class Player_Bullet : MonoBehaviour 
{
    [SerializeField] int damage; // Daño del proyectil del jugador. (Para el ataque especial)
    private void OnBecameInvisible() // Para desactivar el proyectil cuando no esté en pantalla.
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Para desactivar el proyectil cuando choca con algún objeto que no sea el propio jugador y el Confiner2D.
        if (collision.gameObject.layer != 6 && collision.gameObject.layer != 2)
        {
            gameObject.SetActive(false);
        }

        // Para acceder a la función Hurt() del enemigo golpeado por este objeto.
        if (collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponent<EnemyLife>().Hurt(damage); 
        }

        // Para acceder a la función Hurt() del CombatDummy golpeado por este objeto.
        if (collision.gameObject.layer == 10)
        {
            collision.gameObject.GetComponent<Enemy_CombatDummy>().Hurt(damage);
        }
    }
}
