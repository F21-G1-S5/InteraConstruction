using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorLift : MonoBehaviour
{
    private Animator anim;
    private float animFrame;
    private int goUp;
    private int goDown;
    private int topped;
    private int bottomed;

	[SerializeField][Range(0f, 1f)] float upperLimit = 1f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
		goDown = 2;
		goUp = 0;
		topped = 0;
		bottomed = 1;
	}

    // Update is called once per frame
    void Update()
    {
        animFrame = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

		//if (Input.GetKeyDown(KeyCode.UpArrow))
  //      {
		//	Lift();
  //      }
		//if (Input.GetKeyDown(KeyCode.DownArrow))
		//{
		//	Lower();
		//}

		if (animFrame > upperLimit) // Check if we reched the top
		{
			topMax();
		}
		if (animFrame < 0.0f) // Ceck if we reched the bottom
		{
			bottomMax();
		}
	}

	public void Lift()
    {
		switch (goUp)
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

	public void Lower()
    {
		switch (goDown)
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

	void Up() // Start raising the lift
	{
		anim.SetFloat("Direction", 1);
		anim.speed = 1.0f;
		anim.Play("move", -1, float.NegativeInfinity);
		goUp = 1;
		goDown = 2;
	}

	void Down() // Start lowering the lift
	{
		anim.SetFloat("Direction", -1);
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
		switch (topped)
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
		switch (bottomed)
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
