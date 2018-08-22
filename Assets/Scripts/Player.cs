using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character {

	private static Player instance;

	public static Player Instance
	{
		get
		{
			if (instance == null) {

				instance = GameObject.FindObjectOfType<Player> ();
			}

			return instance;
		}

		/*set
		{ 
			instance = value;
		}*/
	}
		
	//private bool attack;
	//private bool slide;

	[SerializeField]
	private Transform[] groundPoints;

	[SerializeField]
	private float groundRadius;

	[SerializeField]
	private LayerMask whatIsGround;

	//private bool isGrounded;

	//private bool jump;

	//private bool jumpAttack;

	[SerializeField]
	private float jumpForce;

	[SerializeField]
	private bool airControl;

	public Rigidbody2D MyRigidbody { get; set;}

	public bool Slide { get; set;}

	public bool Jump { get; set;}

	public bool OnGround { get; set;}

	private Vector2 startPos;


	// Use this for initialization
	public override void Start () 
	{
		//facingRight = true;
		base.Start();
		MyRigidbody = GetComponent<Rigidbody2D> ();	
		//myAnimator = GetComponent<Animator> ();
	}

	void Update()
	{
		handleInput ();
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		float horizontal = Input.GetAxis ("Horizontal");

		OnGround = isGroundeed ();
		//isGrounded = isGroundeed ();
		handleMovement (horizontal);

		flip (horizontal);
		//handleAttcks ();

		handleLayers ();
		//resetValues ();
	}


	private void handleMovement(float horizontal)
	{

		if (MyRigidbody.velocity.y < 0) {
		
			myAnimator.SetBool ("land", true);
		}

		if (!Attack && !Slide && (OnGround || airControl)) {
		
			MyRigidbody.velocity = new Vector2 (horizontal * movementSpeed, MyRigidbody.velocity.y);
		}

		if (Jump && MyRigidbody.velocity.y == 0) {
		
			MyRigidbody.AddForce (new Vector2 (0, jumpForce));
		}
			
		myAnimator.SetFloat ("speed", Mathf.Abs (horizontal));

		/*if (myRigidbody.velocity.y < 0) {
		
			myAnimator.SetBool ("land", true);
		}

		if (!myAnimator.GetBool("slide") && !this.myAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Attack") && (isGrounded || airControl)) {
		
			//myRigidbody.velocity = Vector2.left; // x=-1, y=0
			myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);

		}

		if (isGrounded && jump) {
		
			isGrounded = false;
			myRigidbody.AddForce (new Vector2 (0, jumpForce));
			myAnimator.SetTrigger ("jump");
		}

		if (slide && !this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Slide")) {
		
			myAnimator.SetBool ("slide", true);
		} 

		else if (!this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Slide")) 
		{
		
			myAnimator.SetBool ("slide", false);
		}

		myAnimator.SetFloat ("speed", Mathf.Abs(horizontal));*/
	}


	/*private void handleAttcks()
	{
		if (attack && isGrounded)// && !this.myAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Attack")) 
		{
			myAnimator.SetTrigger ("attack");
			myRigidbody.velocity = Vector2.zero;
		}

		if (jumpAttack && !isGrounded && this.myAnimator.GetNextAnimatorStateInfo(1).IsName("JumpAttack"))
		{
			myAnimator.SetBool ("jumpAttack", true);	
		}

		if (!jumpAttack && !this.myAnimator.GetNextAnimatorStateInfo (1).IsName ("JumpAttack")) {
			myAnimator.SetBool ("jumpAttack", false);
		}
	}*/

	private void handleInput()
	{
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
		
			//attack = true;
			//jumpAttack = true;
			myAnimator.SetTrigger("attack");
		}

		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			//slide = true;
			myAnimator.SetTrigger("slide");
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
		
			//jump = true;
			myAnimator.SetTrigger("jump");
		}

		if (Input.GetKeyDown (KeyCode.V)) {

			//jump = true;
			myAnimator.SetTrigger("throw");
			//ThrowKnife (0);
		}
	}


	public override void ThrowKnife(int value)
	{
		if (!OnGround && value == 1 || OnGround && value == 0) {
		
			base.ThrowKnife (value);

			/*if (facingRight) {

				GameObject tmp = (GameObject)Instantiate (knifePrefab, knifePosition.position, Quaternion.Euler(new Vector3(0, 0, - 90)));
				tmp.GetComponent<Knife> ().Initialize (Vector2.right);
			} else {

				GameObject tmp = (GameObject)Instantiate (knifePrefab, knifePosition.position, Quaternion.Euler(new Vector3(0, 0, 90)));
				tmp.GetComponent<Knife> ().Initialize (Vector2.left);
			}*/
		}
	}

	private void flip(float horizontal)
	{
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) 
		{
			//facingRight = !facingRight;

			//Vector3 theScale = transform.localScale;

			//theScale.x *= -1;

			//transform.localScale = theScale;

			ChangeDirection ();
		}
	}

	/*private void resetValues()
	{
		attack = false;
		slide = false;
		jump = false;
		jumpAttack = false;
	}*/

	private bool isGroundeed()
	{
		if (MyRigidbody.velocity.y <= 0) {

			foreach (Transform point in groundPoints) {
			
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);

				for (int i = 0; i < colliders.Length; i++) {
				
					if (colliders [i].gameObject != gameObject) {
					
						//myAnimator.ResetTrigger ("jump");  
						//myAnimator.SetBool ("land", false);
						return true;
					}
				}
			}
		}

		return false;
	}

	private void handleLayers()
	{
		if (!OnGround) {
		
			myAnimator.SetLayerWeight (1, 1);
		} else {
		
			myAnimator.SetLayerWeight (1, 0);
		}
	}
}
