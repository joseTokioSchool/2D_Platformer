using TMPro;
using UnityEngine;

public class Chronometer : MonoBehaviour // Para el tiempo que tarda el jugador en superar el nivel.
{
    [SerializeField] TMP_Text chronoText;
    public float chronoTime;

    void Start()
    {
        chronoTime = 0;
    }
    void Update()
    {
        Chrono();
    }

    private void Chrono() // Función para actualizar el tiempo y el canvas.
    {
        chronoTime += Time.deltaTime;
        chronoText.text = "TIME: " + chronoTime.ToString("F2");
    }
}
