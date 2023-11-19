using UnityEngine;

public class LevelManager : MonoBehaviour // Para la colisión entre el jugador y la puerta de "YouWin". También controla que nivel hemos superado y desbloquea el siguiente
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6) // Colisión con el jugador
        {
            GameManager.Instance.YouWin(); // Función para ganar la partida
            GameManager.Instance.RecordSystem(); // Función para establecer el récord

            SetLevel();
        }
    }

    private static void SetLevel() // Mediante esta función podremos saber si hemos superado el nivel necesario para avancar al siguiente
    {
        if (PlayerPrefs.GetInt("Levels") == 1)
        {
            PlayerPrefs.SetInt("Levels", 2);
        }
        else if (PlayerPrefs.GetInt("Levels") == 2)
        {
            PlayerPrefs.SetInt("Levels", 3);
        }
    }
}
