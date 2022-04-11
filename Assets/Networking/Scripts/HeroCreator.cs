using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCreator : MonoBehaviour
{
	private static GameManager gameManager;

	// Start is called before the first frame update
	void Start()
	{
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		// gameManager.CreateHeroes();
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
