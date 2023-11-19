using System.Collections;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    #region Script References
    [SerializeField] private Player_Healthbar playerhealthbar;
    #endregion

    [Header("Variables")]
    public int maxPlayerLife;
    public int currentPlayerLife;

    private void Start()
    {
        currentPlayerLife = maxPlayerLife;
    }
    public void Hurt(int damage) // Función heredara de la interfaz para recibir daño.
    {
        currentPlayerLife -= damage;

        playerhealthbar.UpdateHealthbar(maxPlayerLife, currentPlayerLife);
        AudioManager.AudioInstance.HurtClip();

        //Animación de daño (Cambiar el Sprite Renderer a rojo y volver a blanco con una corrutina).
        StartCoroutine(HurtAnimation());

        if (currentPlayerLife <= 0)
        {
            //Desctiva al jugador para que no pueda hacer nada
            gameObject.SetActive(false);

            //Añadir particulas de sangre para dar a entender que el jugador ha sido eliminado
            GameManager.Instance.BloodParticleController(transform.position);

            //Gameover.
            GameManager.Instance.ShowGameOverPanel(1.5f);
        }
    }
    private IEnumerator HurtAnimation() // Corrutina para animar al personaje cuando es herido.
    {
        Debug.Log("rojo");
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.1f);

        Debug.Log("blanco");
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
