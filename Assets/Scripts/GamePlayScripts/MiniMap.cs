using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public GameObject MiniMapObject1;
    public GameObject MiniMapObject2;


    void Update() {
        if(GameManager.currentPlayer == 1)
        {
            MiniMapObject1.SetActive(true);
            MiniMapObject2.SetActive(false);
            
        }
        if(GameManager.currentPlayer == 2)
        {
            MiniMapObject2.SetActive(true);
            MiniMapObject1.SetActive(false);
        }

    }
}
