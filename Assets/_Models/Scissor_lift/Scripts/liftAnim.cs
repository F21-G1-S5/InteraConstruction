/*///////////////////////////////////////////////////////
/														/
/					BVG TEAM - Vik						/
/														/
/				 Scissor Lift Control					/
/														/
/														/
///////////////////////////////////////////////////////*/

/* 
The script raises the lift up when the up arrow is pressed. It stops when it reaches the top or if the up arrow is pressed again.
It also lowers the lift, if the down arrow is pressed. It stops when it reaches the bottom, or when the down arrow is pressed again.
*/

using UnityEngine;
using System.Collections;
 
public class liftAnim : MonoBehaviour // liftAnim and C# file name must be the same
{
	private Animator anim;
	private int goUp;
	private int goDown;
	private int topped;
	private int bottomed;
	private float animFrame;
	
	void Start() // Sets up Starting conditions
	{
		anim = GetComponent<Animator>();
		goDown = 2;
		goUp = 0;
		topped = 0;
		bottomed = 1;
	}
	
	void Update() // Runs once every frame
	{	
		animFrame = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
	
		if(Input.GetKeyDown(KeyCode.UpArrow)) // Modify UpArrow to any other button on the keyboard to raise the lift.
		{
			switch(goUp)
			{
				case 0:
				Up();
				break;
				
				case 1:
				Pause();
				break;
				
				default:
				break;
			}
		}
		
		if(Input.GetKeyDown(KeyCode.DownArrow)) // Modify DownArrow to any other button on the keyboard to lower the lift.
		{
			switch(goDown)
			{
				case 0:
				Down();
				break;
				
				case 1:
				Pause();
				break;
				
				default:
				break;
			}
		}
		
		if(animFrame > 1.0f) // Check if we reched the top
		{
			topMax();
		}
		if(animFrame < 0.0f) // Ceck if we reched the bottom
		{
			bottomMax();
		}
	}
	
	void Up() // Start raising the lift
	{
		anim.SetFloat ("Direction", 1);
		anim.speed = 1.0f;
		anim.Play("move", -1, float.NegativeInfinity);
		goUp = 1;
		goDown = 2;
	}

	void Down() // Start lowering the lift
	{
		anim.SetFloat ("Direction", -1);
		anim.speed = 1.0f;
		anim.Play("move", -1, float.NegativeInfinity);
		goDown = 1;
		goUp = 2;
	}
	
	void Pause() // Stop the lift where it is
	{
		goUp = 0;
		goDown = 0;
		topped = 0;
		bottomed = 0;
		anim.speed = 0.0f;
	}
	
	void topMax() // Stop if we reached the top
	{
		switch(topped)
		{
			case 0:
			goUp = 2;
			anim.speed = 0.0f;
			goDown = 0;
			topped = 1;
			bottomed = 0;
			break;
			
			default:
			topped = 1;
			break;
		}
	}
	
	void bottomMax() // Stop if we reached the bottom
	{
		switch(bottomed)
		{
			case 0:
			goDown = 2;
			anim.speed = 0.0f;
			goUp = 0;
			bottomed = 1;
			topped = 0;
			break;
			
			default:
			bottomed = 1;
			break;
		}
	}
}