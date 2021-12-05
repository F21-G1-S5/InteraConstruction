using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorLift : MonoBehaviour
{
    private static Animator anim;
    private static float animFrame;
    private static int goUp;
    private static int goDown;
    private static int topped;
    private static int bottomed;

	public AudioSource scissorliftAudio;

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
    void FixedUpdate()
    {
        animFrame = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

        //if (Input.GetKeyDown(KeyCode.UpArrow)) {
        //    Lift();
        //}
        //if (Input.GetKeyDown(KeyCode.DownArrow)) {
        //    Lower();
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

	public static void Lift()
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

	public static void Lower()
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

	public static void Up() // Start raising the lift
	{
		scissorliftAudio.Play();
		anim.SetFloat("Direction", 1);
		anim.speed = 1.0f;
		anim.Play("move", -1, float.NegativeInfinity);
		goUp = 1;
		goDown = 2;
	}

	public static void Down() // Start lowering the lift
	{
		scissorliftAudio.Play();
		anim.SetFloat("Direction", -1);
		anim.speed = 1.0f;
		anim.Play("move", -1, float.NegativeInfinity);
		goDown = 1;
		goUp = 2;
	}

	public static void Pause() // Stop the lift where it is
	{
		goUp = 0;
		goDown = 0;
		topped = 0;
		bottomed = 0;
		anim.speed = 0.0f;
	}

	public static void topMax() // Stop if we reached the top
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

	public static void bottomMax() // Stop if we reached the bottom
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
