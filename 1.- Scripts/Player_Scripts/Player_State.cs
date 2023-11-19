using UnityEngine;

public class Player_State : MonoBehaviour
{
    #region Scripts References

    [Header("Script References")]

    [SerializeField] private Player_HorizontalMovement player_horizontalMovement;
    [SerializeField] private Player_Jump player_Jump;
    [SerializeField] private Player_Attack player_Attack;
    [SerializeField] private Player_SpecialAttack player_SpecialAttack;

    #endregion

    #region Components

    [Header("Components References")]

    [SerializeField] private Animator anim;

    #endregion

    /*-------------------- MÉTODOS PRINCIPALES --------------------*/

    #region Initialization Functions
    private void Awake()
    {
        player_horizontalMovement = GetComponent<Player_HorizontalMovement>();
        player_Jump = GetComponent<Player_Jump>();
        player_Attack = GetComponent<Player_Attack>();
        player_SpecialAttack = GetComponent<Player_SpecialAttack>();

        anim = GetComponent<Animator>();
    }
    #endregion

    /*-------------------- MÉTODOS ACTUALIZACIÓN --------------------*/

    #region Update Functions
    void Update()
    {
        if (!GameManager.Instance.pauseGame.isPaused) // Si el juego no está en pausa, se pueden actualizar los estados del jugador.
        {
            // Si el jugador se está moviendo y está tocando el suelo --> Estado Run
            RunState();

            // Si el jugador está saltando y no está tocando el suelo --> Estado Jump
            JumpState();

            // Si el jugador ya ha saltado una vez, no está tocando el suelo y vuelve a saltar --> Estado DoubleJump
            DoubleJumpState();

            // Si el jugador está atacando --> Estado Attack
            AttackState();

            // Si el jugador está atacando --> Estado SpecialAttack
            AttackSpecialState();
        }
    }
    #endregion

    /*--------------------------- FUNCIONES ---------------------------*/

    #region Other Functions
    private void RunState() // Función para actualizar al estado de Run
    {
        if (Mathf.Abs(player_horizontalMovement.input.x) != 0 && player_Jump.IsGrounded())
        {
            player_horizontalMovement.isRun = true;
        }
        else
        {
            player_horizontalMovement.isRun = false;
        }

        anim.SetBool("isRun", player_horizontalMovement.isRun);
    }
    private void JumpState() // Función para actualizar al estado de Jump
    {
        if (player_horizontalMovement.rb2D.velocity.y > 0 && !player_Jump.IsGrounded())
        {
            player_Jump.isJump = true;
        }
        else if (player_Jump.IsGrounded())
        {
            player_Jump.isJump = false;
        }
        anim.SetBool("isJump", player_Jump.isJump);
    }
    private void DoubleJumpState() // Función para actualizar al estado de DoubleJump
    {
        // Para hacer un reset al doble salto.
        if (player_Jump.doubleJumpUsed && player_Jump.IsGrounded())
        {
            player_Jump.doubleJumpUsed = false;
        }
        anim.SetBool("isDoubleJump", player_Jump.doubleJumpUsed);
    }
    private void AttackState() // Función para actualizar al estado de Attack
    {
        if (player_Attack.isAttack)
        {
            player_SpecialAttack.canSpecialAttack = false;

            anim.SetBool("isAttack", player_Attack.isAttack);
        }
        else
        {
            player_SpecialAttack.canSpecialAttack = true;

            anim.SetBool("isAttack", player_Attack.isAttack);
        }
    }
    private void AttackSpecialState() // Función para actualizar al estado de SpecialAttack
    {
        if (player_SpecialAttack.isSpecialAttack)
        {
            player_Attack.canAttack = false;
            player_horizontalMovement.canMove = false;

            anim.SetBool("isSpecialAttack", player_SpecialAttack.isSpecialAttack);
        }
        else
        {
            player_Attack.canAttack = true;
            player_horizontalMovement.canMove = true;

            anim.SetBool("isSpecialAttack", player_SpecialAttack.isSpecialAttack);
        }
    }
    #endregion
}
