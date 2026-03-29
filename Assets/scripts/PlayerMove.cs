using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private CharacterController cc;

    [Header("Movimento")]
    public float walkSpeed = 8f;
    public float runSpeed = 14f;
    public float airControl = 0.5f;
    public float jumpForce = 8f;
    public float gravity = 20f;
    private bool canMove = true;

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float stamina;
    public float staminaDrainRate = 25f;
    public float staminaRecoverRate = 20f;
    public float zeroStaminaRecoverRate = 5f;
    private bool canRun = true;
    private bool zeroStamina = false;

    [Header("HUD")]
    public Image staminaImage;
    public Color fullColor = Color.green;
    public Color halfColor = Color.yellow;
    public Color lowColor = Color.red;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 inputDirection = Vector3.zero;
    private bool isRunning = false;

    private IMovementStrategy currentStrategy;
    private WalkStrategy walkStrategy = new WalkStrategy();
    private RunStrategy runStrategy = new RunStrategy();

    void Start()
    {
        cc = GetComponent<CharacterController>();
        stamina = maxStamina;
        currentStrategy = walkStrategy;
        UpdateHUD();
    }

    void Update()
    {
        if (!canMove)
        {
            moveDirection.x = 0f;
            moveDirection.z = 0f;
            moveDirection.y -= gravity * Time.deltaTime;
            cc.Move(moveDirection * Time.deltaTime);
            return;
        }

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0f; right.y = 0f;
        forward.Normalize(); right.Normalize();

        inputDirection = (forward * v + right * h).normalized;

        isRunning = (Mathf.Abs(v) > 0.1f || Mathf.Abs(h) > 0.1f)
                    && Input.GetKey(KeyCode.LeftShift)
                    && canRun;

        currentStrategy = isRunning ? runStrategy : walkStrategy;

        if (cc.isGrounded)
        {
            Vector3 horizontalMove = currentStrategy.Move(inputDirection, transform);

            moveDirection.x = horizontalMove.x;
            moveDirection.z = horizontalMove.z;

            if (Input.GetButtonDown("Jump"))
                moveDirection.y = jumpForce;
        }
        else
        {
            Vector3 airMove = currentStrategy.Move(inputDirection, transform) * airControl;

            moveDirection.x = Mathf.Lerp(moveDirection.x, airMove.x, airControl * Time.deltaTime * 5f);
            moveDirection.z = Mathf.Lerp(moveDirection.z, airMove.z, airControl * Time.deltaTime * 5f);
        }

        moveDirection.y -= gravity * Time.deltaTime;
        cc.Move(moveDirection * Time.deltaTime);

        if (inputDirection.magnitude > 0.1f)
            transform.rotation = Quaternion.LookRotation(inputDirection);

        if (isRunning)
        {
            stamina -= staminaDrainRate * Time.deltaTime;
            if (stamina <= 0f)
            {
                stamina = 0f;
                canRun = false;
                zeroStamina = true;
            }
        }
        else
        {
            if (zeroStamina)
            {
                stamina += zeroStaminaRecoverRate * Time.deltaTime;
                if (stamina >= maxStamina)
                {
                    stamina = maxStamina;
                    zeroStamina = false;
                    canRun = true;
                }
            }
            else
            {
                stamina += staminaRecoverRate * Time.deltaTime;
                if (stamina > maxStamina) stamina = maxStamina;
            }
        }

        UpdateHUD();
    }
    public void SetMovementEnabled(bool enabled)
    {
        canMove = enabled;
    }
    void UpdateHUD()
    {
        if (staminaImage != null)
        {
            float fill = stamina / maxStamina;
            staminaImage.fillAmount = fill;

            if (fill > 0.5f)
                staminaImage.color = fullColor;
            else if (fill > 0.2f)
                staminaImage.color = halfColor;
            else
                staminaImage.color = lowColor;
        }
    }
    public void ResetPlayer()
    {
        stamina = maxStamina;
        canRun = true;
        zeroStamina = false;
        UpdateHUD();
    }

}