using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy_CombatDummy : MonoBehaviour, IAttackable
{
    #region Components References
    [Header("Components References")]
    [SerializeField] private Transform player;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject deadState;
    #endregion

    #region Variables
    [Header("Variables")]
    [SerializeField] int maxCombatDummyLife;
    [SerializeField] int currentCombatDummyLife;
    #endregion

    #region Events
    [Header("Events")]
    [SerializeField] UnityEvent onDead; // Evento de Unity para realizar el estado de muerte. 
    #endregion

    #region Bools
    [Header("Bools")]
    public bool isLeftHit; //Para saber la dirección en la que es golpeado
    public bool isHurt; // Para decidir cuando está herido
    #endregion


    /*-------------------- MÉTODOS PRINCIPALES --------------------*/
    void Start()
    {
        currentCombatDummyLife = maxCombatDummyLife;

        player = GameObject.Find("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    /*-------------------- MÉTODOS ACTUALIZACIÓN --------------------*/
    void Update()
    {
        CheckHit();
    }

    /*--------------------------- FUNCIONES ---------------------------*/
    #region Other Functions
    public void Hurt(int damage) // Función heredara de la interfaz para recibir daño.
    {
        currentCombatDummyLife -= damage;

        // El enemigo ha sido herido.
        isHurt = true;
        anim.SetBool("isLeftHit", isLeftHit);
        anim.SetBool("isHurt", isHurt);

        if (currentCombatDummyLife <= 0)
        {
            isHurt = false;

            //#TODO: Añadir animación de muerte.
            StartCoroutine(DeadState());

            //#TODO: Añadir CanWin()
            GameManager.Instance.CanWin();
        }
    }
    private void CheckHit() // Función para comprobar la dirección en la que es golpeado.
    {
        if (player.transform.position.x < transform.position.x)
        {
            isLeftHit = true;
        }
        else
        {
            isLeftHit = false;
        }
    }
    public void FinishHurtState() // Función para finalizar el estado de herido.
    {
        isHurt = false;
        anim.SetBool("isHurt", isHurt);
    }
    private IEnumerator DeadState() // Corrutina para la animación del estado de muerte.
    {
        onDead.Invoke();

        /* ---------- Otra forma de realizar el estado de muerte diferente sin usar UnityEvents ---------- */

        //gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //gameObject.GetComponent<Enemy_CombatDummy>().enabled = false;

        //deadState.SetActive(true);

        /* ----------------------------------------------------------------------------------------------- */

        yield return new WaitForSeconds(1.5f);

        gameObject.SetActive(false);
    }
    #endregion
}
