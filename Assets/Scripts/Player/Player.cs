using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumppower;
    public float mouseSensitivity;
    public float health = 100f;
    public float climbSpeed = 10f;
    public float rayDistance = 1.5f; 

    private Vector2 curMovementInput;
    public Vector3 startLocation;

    private float hAxis;
    private float vAxis;
    private bool wDown;
    private bool jDown;

    private bool isJump;
    private bool isClimbing; 

    private Rigidbody rigid;
    private Animator anim;

    private float rotationY = 0f;

    public Text durationText;
    private Coroutine speedBoostCoroutine;
    private Coroutine jumpBoostCoroutine;
    private float totalEffectDuration;
    private bool isEffectActive = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        durationText.gameObject.SetActive(false);
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Climb();

        if (isEffectActive)
        {
            UpdateEffectDurationUI();

            if (totalEffectDuration <= 0)
            {
                EndEffect();
            }
        }

        if (transform.position.y <= 0)
        {
            TpPlayer();
        }
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
    }

    void Move()
    {
        Vector3 moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (moveVec != Vector3.zero && !isClimbing)
        {
            moveVec = transform.TransformDirection(moveVec);
            transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

            anim.SetBool("isRun", true);
            anim.SetBool("isWalk", wDown);
        }
        else
        {
            anim.SetBool("isRun", false);
            anim.SetBool("isWalk", false);
        }
    }

    void Turn()
    {
        rotationY += Input.GetAxis("Mouse X") * mouseSensitivity;
        Quaternion rotation = Quaternion.Euler(0, rotationY, 0);
        transform.rotation = rotation;
    }

    void Jump()
    {
        if (jDown && !isJump && !isClimbing)
        {
            rigid.AddForce(Vector3.up * jumppower, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }

    void Climb()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    isClimbing = true;
                    rigid.useGravity = false;

                    Vector3 climbVelocity = new Vector3(0, climbSpeed, 0);
                    rigid.velocity = climbVelocity;
                }
                else
                {
                    isClimbing = false; 
                }
            }
        }
        else
        {
            isClimbing = false; 
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("플레이어 체력: " + health);

        if (health <= 0)
        {
            Debug.Log("플레이어 사망!");
        }
    }

    void FixedUpdate()
    {
        if (!isClimbing)
        {
            rigid.useGravity = true; 
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isClimbing = false;
            rigid.useGravity = true; 
            rigid.velocity = Vector3.zero;
        }
    }

    void TpPlayer()
    {
        transform.position = startLocation;
    }

    public void ApplyJumpBoost(float multiplier, float duration)
    {
        if (jumpBoostCoroutine != null)
        {
            StopCoroutine(jumpBoostCoroutine);
        }
        jumppower *= multiplier;
        totalEffectDuration = duration;
        isEffectActive = true;
        durationText.gameObject.SetActive(true);
        jumpBoostCoroutine = StartCoroutine(ResetJumpPower(duration));
    }

    private IEnumerator ResetJumpPower(float duration)
    {
        yield return new WaitForSeconds(duration);
        jumppower /= 2;
        jumpBoostCoroutine = null;
    }

    public void ApplySpeedBoost(float multiplier, float duration)
    {
        if (speedBoostCoroutine != null)
        {
            StopCoroutine(speedBoostCoroutine);
        }
        speed *= multiplier;
        totalEffectDuration = duration;
        isEffectActive = true;
        durationText.gameObject.SetActive(true);
        speedBoostCoroutine = StartCoroutine(ResetSpeed(duration));
    }

    private IEnumerator ResetSpeed(float duration)
    {
        yield return new WaitForSeconds(duration);
        speed /= 2;
        speedBoostCoroutine = null;
    }

    private void UpdateEffectDurationUI()
    {
        totalEffectDuration -= Time.deltaTime;
        durationText.text = "버프 지속 시간: " + Mathf.Max(totalEffectDuration, 0).ToString("F1") + "초";
    }

    private void EndEffect()
    {
        isEffectActive = false;
        durationText.gameObject.SetActive(false);
    }
}
