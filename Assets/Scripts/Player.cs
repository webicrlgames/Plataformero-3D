using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private Rigidbody m_Rigidbody;

    private PlayerInput m_playerInput;

    public float WalkSpeed = 5;
    public float JumpForce = 5;
    public float RotateSpeed = 250f;
    public GameObject PlayerCameraPrefab;
    private Camera playerCamera;

    private static System.Collections.Generic.List<Camera> playerCameras = new System.Collections.Generic.List<Camera>();

    public bool Jump;

    private Vector2 m_moveAmt;
    private Vector2 m_lookAmt;

    public Inventory Inventory;

    public int playerNumber = 0;
    private static int playerCount = 0;

    public void OnMove(InputAction.CallbackContext ctx)
    {
        m_moveAmt = ctx.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        m_lookAmt = ctx.ReadValue<Vector2>();
    }

    private void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
        m_Rigidbody = GetComponent<Rigidbody>();
        playerCount++;
        playerNumber = playerCount;
    }

    private void Start()
    {
        Jump = true;
        Inventory = FindObjectOfType<Inventory>();
        if (PlayerCameraPrefab != null)
        {
            playerCamera = Instantiate(PlayerCameraPrefab, transform.position + new Vector3(0, 5, -5), Quaternion.identity).GetComponent<Camera>();

            m_playerInput.camera = playerCamera;

            playerCamera.transform.SetParent(transform);
            playerCamera.transform.localPosition = new Vector3(0, 5, -5);
            playerCamera.transform.localRotation = Quaternion.Euler(30, 0, 0);

            playerCameras.Add(playerCamera);

            AdjustAllCameras();
        }
    }

    private void AdjustAllCameras()
    {
        for (int i = 0; i < playerCameras.Count; i++)
        {
            Camera cam = playerCameras[i];
            AdjustCameraViewport(cam, i);
        }
    }

    private void AdjustCameraViewport(Camera camera, int playerIndex)
    {
        if (playerCameras.Count == 1)
        {
            camera.rect = new Rect(0f, 0f, 1f, 1f);
        }
        else if (playerCameras.Count == 2)
        {
            camera.rect = new Rect(playerIndex == 0 ? 0f : 0.5f, 0f, 0.5f, 1f);
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {

        Vector3 move = new Vector3(m_moveAmt.x, 0, m_moveAmt.y);
        move = transform.TransformDirection(move);
        m_Rigidbody.MovePosition(transform.position + move * WalkSpeed * Time.deltaTime);

        




    }

    private void HandleRotation()
    {
        if (m_lookAmt.sqrMagnitude > 0)
        {
            float rotAmount = m_lookAmt.x * RotateSpeed * Time.deltaTime;
            transform.Rotate(0, rotAmount, 0);
        }
    }

    public void OnTeleport()
    {
        transform.position = new Vector3(Random.Range(-75, 75), 0.5f, Random.Range(-75, 75));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Money"))
        {
            Destroy(collision.gameObject);
            Inventory.ActMoney();
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Jump = true;
        }
    }

    public void OnJump(InputAction.CallbackContext callbackContext) {

        if (callbackContext.performed) {

            Debug.Log("JUMP ");

            if (Jump)
            {
                m_Rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
                Jump = false;
            }
        
    }
    
    }
}