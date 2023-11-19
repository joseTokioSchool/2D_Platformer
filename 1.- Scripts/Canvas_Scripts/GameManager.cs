using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour // Controlador principal del juego.
{
    /*--------------------------------------------------- SINGLETONS --------------------------------------------------- */

    #region SINGLETONS
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    /*---------------------------------- VARIABLES ----------------------------------*/

    #region Scripts References

    [Header("Script References")]

    [SerializeField] private Player_Healthbar player_Healthbar;
    [SerializeField] private PlayerLife playerLife;
    [SerializeField] public PauseGame pauseGame;
    [SerializeField] public Chronometer chronometer;

    #endregion

    #region GameObjects
    [Header("GameObjects References")]

    [Header("Panel")]
    [SerializeField] GameObject panelGameOver;
    [SerializeField] GameObject panelYouWin;

    [Header("Doors")]
    [SerializeField] GameObject OpenDoor;
    [SerializeField] GameObject ClosedDoor;

    [Header("Player")]
    [SerializeField] GameObject player;

    [Header("Others")]
    [SerializeField] ParticleSystem explosion;
    [SerializeField] GameObject alertArrow;


    #endregion

    #region TxT References
    [Header("Txt_References")]
    public TMP_Text diamondTXT;
    public TMP_Text arrowsTXT;
    #endregion

    #region Canvas Settings
    [Header("Canvas Settings")]
    public int diamondCount;
    public int arrowCount;
    #endregion

    #region Booleans
    [Header("Bools Controllers")]
    public bool isAlertAtcive; // Para controlar cuando la alerta de texto est� activada
    public bool canNextLevel; // Para controlar cuando se puede avanzar al siguiente nivel 
    #endregion

    /*-------------------- M�TODOS PRINCIPALES --------------------*/

    #region Initialization Functions
    void Start()
    {
        // Para cuando comienza el nivel.
        canNextLevel = false;
        ClosedDoor.SetActive(true);
        OpenDoor.SetActive(false);

        // Para mantener el panel del GameOver desactivado al principio de cada nivel
        panelGameOver.SetActive(false);

        // Conteo de las flechas iniciales y los puntos al comienzo de cada nivel
        arrowCount = 3;
        arrowsTXT.text = "x" + arrowCount.ToString();

        diamondCount = 0;
        diamondTXT.text = "x" + diamondCount.ToString();
    }
    #endregion

    /*--------------------------- FUNCIONES ---------------------------*/

    #region Score Functions
    /*---------------------------------- Para aumentar la puntuaci�n del jugador.----------------------------------*/
    public void AddDiamond() // Funci�n para a�adir puntuaci�n y actualizar el canvas.
    {
        diamondCount++;
        diamondTXT.text = "x" + diamondCount.ToString();

        AudioManager.AudioInstance.ItemClip();
    }
    #endregion

    #region Life Functions
    /*---------------------------------- Para aumentar la vida del jugador.----------------------------------*/
    public void AddLife() // Funci�n para a�adir vida y actualizar la barra de vida.
    {
        playerLife.currentPlayerLife = 100;
        player_Healthbar.UpdateHealthbar(playerLife.maxPlayerLife, playerLife.currentPlayerLife);

        AudioManager.AudioInstance.ItemClip();
    }
    #endregion

    #region Arrow Functions
    /*---------------------------------- Para aumentar las flechas del jugador.----------------------------------*/
    public void AddArrow() // Funci�n para a�adir flechas y actualizar el canvas.
    {
        arrowCount++;
        arrowsTXT.text = "x" + arrowCount.ToString();

        AudioManager.AudioInstance.ItemClip();
    }
    /*---------------------------------- Para mostar la alerta ("�No me quedan flechas!") del jugador.----------------------------------*/
    public IEnumerator ActivateAlertArrow()
    {
        alertArrow.SetActive(true);
        isAlertAtcive = true;

        yield return new WaitForSeconds(2f);

        alertArrow.SetActive(false);
        isAlertAtcive = false;

    }
    #endregion

    #region Particle Functions
    /*---------------------------------- Para mostar las part�culas de muerte.----------------------------------*/
    public void BloodParticleController(Vector3 gameObject)
    {
        explosion.transform.position = gameObject; // Igualamos la posici�n de las part�culas al objeto que deseamos.
        explosion.gameObject.SetActive(true); // Activamos las part�culas
        explosion.Play(); // Para dar comienzo a la animaci�n de las part�culas

        // NOTA: Solo existe un objeto de part�culas en la escena, por lo tanto, solo un objeto podr� utilizarlas. (Se podr�a arreglar creando una Piscina de Objetos de part�culas)

        AudioManager.AudioInstance.DeadStateClip();
    }
    #endregion

    #region GameState Functions

    /*----------------------------------------------- Para acabar la partida. -----------------------------------------------*/
    public void GameOver() // Funci�n para el Game Over del juego.
    {
        AudioManager.AudioInstance.GameOverClip();
        Time.timeScale = 0f;
        player.SetActive(false);
        panelGameOver.SetActive(true);
    }
    public void ShowGameOverPanel(float delay) // Funci�n para tener delay al resetear la partida.
    {
        //Invoke(nameof(ResetLevel), delay);
        Invoke(nameof(GameOver), delay);
    }

    /*----------------------------------------------- Para reiniciar la partida.-----------------------------------------------*/
    public void ResetLevel() // (BUTTON) Funci�n para resetear la partida.
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    /*----------------------------------------------- Para el YOUWIN de la partida. -----------------------------------------------*/

    public void CanWin() // Funci�n para cambiar los gameObjects de las puertas y poder pasar al siguiente nivel
    {
        canNextLevel = true;

        ClosedDoor.SetActive(false);
        OpenDoor.SetActive(true);

        AudioManager.AudioInstance.OpenDoorClip();

    }
    public void YouWin() // Funci�n para activar el panel de YouWin del juego.
    {
        AudioManager.AudioInstance.GameOverClip();
        Time.timeScale = 0f;
        player.SetActive(false);
        panelYouWin.SetActive(true);
    }
    public void NextLevel() // (BUTTON) Funci�n para cargar el siguiente nivel del juego
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }

    /*-----------------------------------------------Para los R�cords de la partida. -----------------------------------------------*/

    public void RecordSystem() // Funci�n para establecer el r�cord de cada nivel. �IMPORTANTE! Para establecer el r�cord, el jugador debe haber completado el nivel.
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (PlayerPrefs.GetFloat("Level1Time") >= chronometer.chronoTime && PlayerPrefs.GetInt("Level1Points") <= diamondCount)
            {
                PlayerPrefs.SetFloat("Level1Time", chronometer.chronoTime);
                PlayerPrefs.SetInt("Level1Points", diamondCount);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (PlayerPrefs.GetFloat("Level2Time") >= chronometer.chronoTime && PlayerPrefs.GetInt("Level2Points") <= diamondCount)
            {
                PlayerPrefs.SetFloat("Level2Time", chronometer.chronoTime);
                PlayerPrefs.SetInt("Level2Points", diamondCount);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (PlayerPrefs.GetFloat("Level3Time") >= chronometer.chronoTime && PlayerPrefs.GetInt("Level3Points") <= diamondCount)
            {
                PlayerPrefs.SetFloat("Level3Time", chronometer.chronoTime);
                PlayerPrefs.SetInt("Level3Points", diamondCount);
            }
        }
    }

    #endregion

    #region Menu System Functions
    /*---------------------------------- Para ir al Men� Principal.----------------------------------*/
    public void MainMenu(int n)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(n);
    }
    #endregion
}
