using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
	public int UserID { get; set; }

	public int car {get; set;}
	public string Name { get; set; }
	public Color Color { get; set; }
	
	public bool IsMouseControlled { get; set; }

	private int heroCount = 0;

	public Player(int userID, string name, Color color, bool isMouseControlled, int car)
	{
		UserID = userID;
		Name = name;
		Color = color;
		IsMouseControlled = isMouseControlled;
		car = car;
	}

}
