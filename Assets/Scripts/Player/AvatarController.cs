using UnityEngine;
using System.Collections;

public class AvatarController : MonoBehaviour
{
    public float speed = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private float groundCheckRadius = 0.5f;
    private float groundCheckNarrow = 0.2f;
    private bool _isInAir = false;
    private bool _isJumping = false;
    private Animator animator;
    private bool isFacingRight = true;
    
    void Awake()
    {
    }
    
    void Start()
    {
        animator = GetComponent<Animator> ();
    }
    
    void FixedUpdate()
    {
        float baseXVelocity = 0; 
        Collider[] colliders = Physics.OverlapSphere (groundCheck.position, groundCheckRadius, groundLayer);
        Collider[] narrow_colliders = Physics.OverlapSphere (groundCheck.position, groundCheckNarrow, groundLayer);
        if (colliders.Length > 0) 
        {
            if (colliders [0].tag == "MovingPlatform" && !_isJumping && narrow_colliders.Length > 0)
            {
                //rigidbody.velocity = colliders[0].GetComponent<MovingPlatform>().MovingSpeed;
                rigidbody.velocity = colliders[0].rigidbody.velocity;
                baseXVelocity = colliders[0].rigidbody.velocity.x;
            }
            _isInAir = false;
        } 
        else 
        {
            _isInAir = true;
            _isJumping = false;
        }
        InputMovement(baseXVelocity);
    }
    
    void Update()
    {
    }
    
    private void InputMovement(float baseXVelocity)
    {
        if (Input.GetKey (KeyCode.D)) {
            if(!isFacingRight)
            {
                isFacingRight  = true;
                ChangeFacing();
            }
            rigidbody.velocity = new Vector3 (speed + baseXVelocity, rigidbody.velocity.y,0);
            animator.SetFloat("Speed", 10);
        } else if (Input.GetKey (KeyCode.A)) {
            if(isFacingRight)
            {
                isFacingRight = false;
                ChangeFacing();
            }
            rigidbody.velocity = new Vector3 (-speed + baseXVelocity, rigidbody.velocity.y,0);
            animator.SetFloat("Speed", 10);
        } else {
            rigidbody.velocity = new Vector3 (baseXVelocity, rigidbody.velocity.y,0);
            animator.SetFloat("Speed", 0);
        }
        if (Input.GetKey (KeyCode.Space)) {
            if(!_isInAir)
            {
                rigidbody.AddForce(Vector3.up * 100);
                if(rigidbody.velocity.y > 5)
                {
                    rigidbody.velocity = new Vector3(rigidbody.velocity.x, 5, 0);
                }
                _isJumping = true;
            }
        }
    }
    
    private void ChangeFacing()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
