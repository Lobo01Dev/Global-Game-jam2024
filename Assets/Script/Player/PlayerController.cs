using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Weapons
{
    Scithe,
    Gun
}

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 150f;
    public float maxSpeed = 8f;

    public SwordAttack swordAttack;
    public GameObject gun;

    // Each frame of physics, what percentage of the speed should be shaved off the velocity out of 1 (100%)
    public float idleFriction = 0.9f;
    [SerializeField]
    Rigidbody2D rb;
    Animator animator;
    private bool isattacking = false;
    private bool canCombo = false;
    SpriteRenderer spriteRenderer;
    Vector2 moveInput = Vector2.zero;
    private PlayerInput inputActions;

    private Weapons equipedWeapon = Weapons.Scithe;
    private bool shooting = false;

    private bool canMove;
    [SerializeField]
    private float moveSpeedFactor = 1;

    public float MoveSpeedFactor { get => moveSpeedFactor; set => moveSpeedFactor = value; }

    void Start()
    {
        if(GameManager.gm.Player == null)
        {
            GameManager.gm.Player = gameObject;
        }
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        inputActions = new PlayerInput();
        inputActions.Player.Enable();
        inputActions.Player.Fire.performed += Fire_performed;
        inputActions.Player.Fire.canceled += Fire_canceled;
        inputActions.Player.Move.performed += Move_performed;
        inputActions.Player.Move.canceled += Move_canceled;
        inputActions.Player.EquipWeapon1.performed += EquipWeapon1_performed;
        inputActions.Player.EquipWeapon2.performed += EquipWeapon2_performed;
    }

    private void Fire_canceled(InputAction.CallbackContext obj)
    {
        shooting = false;
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        OnMove(Vector2.zero);
    }

    private void EquipWeapon2_performed(InputAction.CallbackContext obj)
    {
        OnWeaponEquipWeapon2();
    }

    private void EquipWeapon1_performed(InputAction.CallbackContext obj)
    {
        OnWeaponEquipWeapon1();
    }

    private void Move_performed(InputAction.CallbackContext obj)
    {
        Vector2 inputvalue = obj.ReadValue<Vector2>();
        OnMove(inputvalue);
    }

    private void Fire_performed(InputAction.CallbackContext obj)
    {
        OnFire();
        if (equipedWeapon == Weapons.Gun)
        {
            shooting = true;
        }
    }

    private void Inputs_onActionTriggered(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
       if(shooting && equipedWeapon == Weapons.Gun)
        {
            OnFire();
        } 
    }

    void FixedUpdate()
    {

        if (moveInput != Vector2.zero)
        {
            // Move animation and add velocity

            // Accelerate the player while run direction is pressed
            // BUT don't allow player to run faster than the max speed in any direction
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + (moveInput * moveSpeed * Time.deltaTime), maxSpeed*moveSpeedFactor);

            // Control whether looking left or right
            if (moveInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (moveInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }

            UpdateAnimatorParameters();
        }
        else
        {
            // No movement so interpolate velocity towards 0
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
            animator.SetBool("IsMoving", false);
        }
    }


    // Get input values for player movement
    public void OnMove(Vector2 value)
    {
        moveInput = value;
    }

     void UpdateAnimatorParameters()
     {
        animator.SetBool("IsMoving", true);         
     }

    public void OnWeaponEquipWeapon1()
    {
        ChangeWeapon(Weapons.Scithe);
    }
    public void OnWeaponEquipWeapon2()
    {
        ChangeWeapon(Weapons.Gun);
    }

    public void ChangeWeapon()
    {
        if (equipedWeapon == Weapons.Scithe)
        {
            equipedWeapon = Weapons.Gun;
        }
        else if(equipedWeapon == Weapons.Gun)
        {
            equipedWeapon = Weapons.Scithe;
        }
        validadeGun();
        ValitadeAnimator();
    }

    public void ChangeWeapon(Weapons weapon)
    {
        equipedWeapon = weapon;
        validadeGun();
        ValitadeAnimator();
    }

    private void validadeGun()
    {
        if(equipedWeapon == Weapons.Gun)
        {
            gun.SetActive(true);
        }
        else
        {
            gun.SetActive(false);
        }
    }

    private void ValitadeAnimator()
    {
        if (equipedWeapon == Weapons.Scithe)
        {
            animator.SetBool("HaveGun",false);
        }
        else if (equipedWeapon == Weapons.Gun)
        {
            animator.SetBool("HaveGun", true);
        }
    }

    public void OnFire()
    {
        if (equipedWeapon == Weapons.Scithe)
        {
            SwordAttack();
            if (canCombo)
            {
                SwordAttack();
                animator.SetBool("MeleeCombo", true);
            }
        }
        else if (equipedWeapon == Weapons.Gun)
        {
            gun.GetComponent<GunWeapon>().Fire();
        }
    }

    public void SwordAttack()
    {
        LockMovement();
        if (!isattacking)
        {
            isattacking = true;
            animator.SetTrigger("MeleeAttack");


            if (spriteRenderer.flipX == true)
            {
                swordAttack.AttackLeft();
            }
            else
            {
                swordAttack.AttackRight();
            }
        }
    }

    public void EndSwordAttack()
    {
        isattacking = false;
        canCombo = false;
        animator.SetBool("MeleeCombo", false);
        UnlockMovement();
        swordAttack.StopAttack();
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    public void EnableCombo()
    {
        canCombo = true;
    } 


}
