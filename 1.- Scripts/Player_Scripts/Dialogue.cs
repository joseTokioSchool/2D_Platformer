using UnityEngine;

public class Dialogue : MonoBehaviour // Función para el texto del jugador.
{
    [SerializeField] Transform player;

    /*-------------------- MÉTODOS ACTUALIZACIÓN --------------------*/
    private void FixedUpdate() // Para que el texto siga al jugador independientemente de los fps
    {
        transform.position = player.position;
    }
}
