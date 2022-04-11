using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
	public Player Owner;
	public int Index;
	public float Speed = 5.0f;
	public float InteractionSpeed = 1.0f;
	public int x;
	public int y;

	private bool isMoving = false;
	private bool isInteracting = false;
	private bool isInteractedWith = false;
	private float interactionTime = 0.0f;
	private Vector3 target;

	private GameManager gameManager;
	
    // Start is called before the first frame update
    void Start()
    {
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

		target = transform.position;
		x = (int)transform.position.x;
		y = (int)transform.position.z;
	}

    // Update is called once per frame
    void Update()
    {
		if (isMoving)
		{
			Vector3 delta = target - transform.position;
			float lenDelta = delta.magnitude;
			if (lenDelta > Speed * Time.deltaTime)
			{
				delta = delta * Speed * Time.deltaTime / lenDelta;
			}
			else
			{
				isMoving = false;
				gameManager.EndMove(this);
			}
			transform.position = transform.position + delta;
		}
		if (isInteracting)
		{
			float scale = 1.0f + 0.3f * (1.0f - interactionTime) * interactionTime;
			transform.localScale = new Vector3(scale, scale, scale);
			interactionTime += Time.deltaTime * InteractionSpeed;
			if (interactionTime >= 1.0f)
			{
				isInteracting = false;
				gameManager.EndInteraction(this);
			}
		}
		if (isInteractedWith)
		{
			float scale = 1.0f - 0.3f * (1.0f - interactionTime) * interactionTime;
			transform.localScale = new Vector3(scale, scale, scale);
			interactionTime += Time.deltaTime * InteractionSpeed;
			if (interactionTime >= 1.0f)
			{
				isInteractedWith = false;
				gameManager.EndInteractedWith(this);
			}
		}
	}

	public void Move(int x, int y)
	{
		isMoving = true;
		target = new Vector3((float)x, 0, (float)y);
		this.x = x;
		this.y = y;
	}

	public void Interact(Hero target)
	{
		isInteracting = true;
		interactionTime = 0.0f;
		target.ReceiveInteraction(this);
		Debug.Log("Interacting!");
	}

	private void ReceiveInteraction(Hero interactor)
	{
		isInteractedWith = true;
		interactionTime = 0.0f;
	}

	public bool CanMoveTo(int x, int y)
	{
		return !isMoving;
	}
}
