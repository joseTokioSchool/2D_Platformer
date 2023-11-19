using UnityEngine;

public class Items : MonoBehaviour
{
    public enum Type { Diamond, Life, Arrow, Spike }

    public Type type;

    private void OnTriggerEnter2D(Collider2D collision) // Funci�n para detectar al jugador cuando choca con el �tem.
    {
        if (collision.CompareTag("Player"))
        {
            Collect(collision.gameObject); //collision.gameObject --> Player.
        }
    }

    private void Collect(GameObject player) // Funci�n para recolectar los items.
    {
        switch (type)
        {
            case Type.Diamond: // Para la puntuaci�n
                GameManager.Instance.AddDiamond();
                Destroy(gameObject);
                Debug.Log("puntos");
                break;
            case Type.Life: // Para la vida
                GameManager.Instance.AddLife();
                Destroy(gameObject);
                Debug.Log("vidas");
                break;
            case Type.Arrow: // Para las flechas
                GameManager.Instance.AddArrow();
                Destroy(gameObject);
                Debug.Log("flechas");
                break;
            case Type.Spike: // Para los pinchos
                player.GetComponent<PlayerLife>().Hurt(100);
                Debug.Log("spkie");
                break;
        }


    }
}
