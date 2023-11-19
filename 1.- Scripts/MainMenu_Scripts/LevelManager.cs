using UnityEngine;

public class LevelManager : MonoBehaviour // Para la colisi�n entre el jugador y la puerta de "YouWin". Tambi�n controla que nivel hemos superado y desbloquea el siguiente
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6) // Colisi�n con el jugador
        {
            GameManager.Instance.YouWin(); // Funci�n para ganar la partida
            GameManager.Instance.RecordSystem(); // Funci�n para establecer el r�cord

            SetLevel();
        }
    }

    private static void SetLevel() // Mediante esta funci�n podremos saber si hemos superado el nivel necesario para avancar al siguiente
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
