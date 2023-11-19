using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    /*---------------------------------- VARIABLES ----------------------------------*/

    [SerializeField] GameObject panelPause;

    public bool isPaused; // Para controlar si el juego está en pausa o no.

    /*-------------------- MÉTODOS PRINCIPALES --------------------*/
    void Start()
    {
        panelPause.SetActive(false);
    }

    /*---------------------------------- Para el input de Pausa.----------------------------------*/
    public void Pause(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            ShowPausePanel();
            UpdateGameState();
        }
    }

    /*---------------------------------- Para actualizar el estado de Pausa.----------------------------------*/
    public void UpdateGameState()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            AudioManager.AudioInstance.PauseClip();
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    /*---------------------------------- Para los mostrar el Menú de Pausa.----------------------------------*/
    private void ShowPausePanel()
    {
        if (!isPaused)
        {
            panelPause.SetActive(true);
        }
        else
        {
            panelPause.SetActive(false);
        }
    }

    /*---------------------------------- Para los botones del Menú de Pausa.----------------------------------*/

    #region Buttons Functions
    public void Continuar()
    {
        panelPause.SetActive(false);
        UpdateGameState();
    }
    public void MainMenu(int n)
    {
        SceneManager.LoadScene(n);
        Time.timeScale = 1f;
    }
    #endregion
}
