using UnityEngine;

public class FrameRate : MonoBehaviour // Para establecer una cantidad m�xima de FPS en el juego.
{
    private int cantidadDeFPS = 60;
    private void Awake()
    {
        Application.targetFrameRate = cantidadDeFPS;
    }
}
