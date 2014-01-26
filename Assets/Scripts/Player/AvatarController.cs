using UnityEngine;
using System.Collections;

public class AvatarController : MonoBehaviour
{
    public float speed = 5f;
	public Transform groundCheck;
	public LayerMask groundLayer;
	private float groundCheckRadius = 0.2f;
	private bool isFlying = false;
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
		Collider[] colliders = Physics.OverlapSphere (groundCheck.position, groundCheckRadius, groundLayer);
		if (colliders.Length > 0) 
		{
			isFlying = false;
		} 
		else 
		{
		   isFlying = true;
		}
		InputMovement();
	}

    void Update()
    {
    }

    private void InputMovement()
    {
        if (Input.GetKey (KeyCode.D)) {
						if(!isFacingRight)
						{
							isFacingRight  = true;
				ChangeFacing();
			}
				        rigidbody.velocity = new Vector3 (speed, rigidbody.velocity.y,0);
			            animator.SetFloat("Speed", 10);
				} else if (Input.GetKey (KeyCode.A)) {
			if(isFacingRight)
			{
			            isFacingRight = false;
				ChangeFacing();
			}
						rigidbody.velocity = new Vector3 (-speed, rigidbody.velocity.y,0);
						animator.SetFloat("Speed", 10);
		} else {
						rigidbody.velocity = new Vector3 (0, rigidbody.velocity.y,0);
						animator.SetFloat("Speed", 0);
		}
		if (Input.GetKey (KeyCode.Space)) {
			if(!isFlying)
			{
				rigidbody.AddForce(Vector3.up * 100);
			}
		}
    }

	private void ChangeFacing()
	{
		transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
