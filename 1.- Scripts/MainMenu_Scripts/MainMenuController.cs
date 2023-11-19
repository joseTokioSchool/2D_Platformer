using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    /*---------------------------------- VARIABLES ----------------------------------*/

    #region GameObjects References
    [SerializeField] GameObject level2, level3;
    #endregion

    #region TxT References
    [Header("Txt_References")]
    [SerializeField] TMP_Text RecordLvl1;
    [SerializeField] TMP_Text RecordLvl2;
    [SerializeField] TMP_Text RecordLvl3;
    #endregion

    /*---------------------------------- MÉTODOS ACTUALIZACIÓN ----------------------------------*/
    private void Update()
    {
        LevelManager();
    }

    /*--------------------------- FUNCIONES ---------------------------*/

    #region Other Functions
    /*---------------------------------- Para activar/desactivar los botones de los niveles en función de si hemos superado el anterior o no ----------------------------------*/
    private void LevelManager()
    {
        if (PlayerPrefs.GetInt("Levels") == 1)
        {
            level2.GetComponent<Button>().enabled = false;
            level3.GetComponent<Button>().enabled = false;
        }
        else if (PlayerPrefs.GetInt("Levels") == 2)
        {
            level2.GetComponent<Button>().enabled = true;
            level3.GetComponent<Button>().enabled = false;
        }
        else if (PlayerPrefs.GetInt("Levels") >= 3)
        {
            level2.GetComponent<Button>().enabled = true;
            level3.GetComponent<Button>().enabled = true;
        }
    }
    #endregion

    /*---------------------------------- Para los botones del Menú Principal ----------------------------------*/

    #region Button Functions
    public void ChangeScene(int n) // (BUTTON) Función para ir a una escena determinada.
    {
        SceneManager.LoadScene(n);
    }
    public void ExitGame(int n) // (BUTTON) Para salir del juego
    {
        Application.Quit(n);
    }
    public void CheckRecords() // (BUTTON) Para ver los récords y actualizarlos
    {
        RecordLvl1.text = "Level 1: Points: " + PlayerPrefs.GetInt("Level1Points").ToString() + " | Time: " + PlayerPrefs.GetFloat("Level1Time").ToString("F2");
        RecordLvl2.text = "Level 2: Points: " + PlayerPrefs.GetInt("Level2Points").ToString() + " | Time: " + PlayerPrefs.GetFloat("Level2Time").ToString("F2");
        RecordLvl3.text = "Level 3: Points: " + PlayerPrefs.GetInt("Level3Points").ToString() + " | Time: " + PlayerPrefs.GetFloat("Level3Time").ToString("F2");
    }
    #endregion
}
