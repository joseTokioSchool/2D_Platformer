using UnityEngine;

public class InitializePlayerPrefs : MonoBehaviour // Sistema de inicialización de datos del juego. (Puntuación, Tiempo y Niveles)
{
    private void Awake()
    {
        #region Levels
        // PlayerPrefs para la gestión de niveles: Valor 1 para nivel 1, Valor 2 para niveles 1 y 2, Valor 3 para niveles 1, 2 y 3.
        if (PlayerPrefs.HasKey("Levels") == false)
        {
            PlayerPrefs.SetInt("Levels", 1);
        }
        #endregion

        #region LevelPoints
        // PlayerPrefs para la gestión de los puntos del nivel 1.
        if (PlayerPrefs.HasKey("Level1Points") == false)
        {
            PlayerPrefs.SetInt("Level1Points", 0);
        }

        // PlayerPrefs para la gestión de los puntos del nivel 2.
        if (PlayerPrefs.HasKey("Level2Points") == false)
        {
            PlayerPrefs.SetInt("Level2Points", 0);
        }

        // PlayerPrefs para la gestión de los puntos del nivel 3.
        if (PlayerPrefs.HasKey("Level3Points") == false)
        {
            PlayerPrefs.SetInt("Level3Points", 0);
        }
        #endregion

        #region LevelTime
        // PlayerPrefs para la gestión del récord de tiempo del nivel 1:
        if (PlayerPrefs.HasKey("Level1Time") == false)
        {
            PlayerPrefs.SetFloat("Level1Time", 999f);
        }

        // PlayerPrefs para la gestión del récord de tiempo del nivel 2:
        if (PlayerPrefs.HasKey("Level2Time") == false)
        {
            PlayerPrefs.SetFloat("Level2Time", 999f);
        }

        // PlayerPrefs para la gestión del récord de tiempo del nivel 3:
        if (PlayerPrefs.HasKey("Level3Time") == false)
        {
            PlayerPrefs.SetFloat("Level3Time", 999f);
        }
        #endregion
    }
}
