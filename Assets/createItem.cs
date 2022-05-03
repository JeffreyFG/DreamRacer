using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createItem : MonoBehaviour
{
    public GameObject car;
    public GameObject objectToSpawn;

    public GameManager manager;

    public bool isEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonUp("Jump") & isEnabled){
          SpawnItem();  
        }
    }
    public void SpawnItem(){

        //spawns Item
        GameObject plane = Instantiate(objectToSpawn, transform.position,transform.rotation);
        manager.CreateItem(plane);
        plane.GetComponent<PlaneItem>().car =  car;

        plane.SetActive(true);

    }
    public void SpawnItem(Vector3 position){
        print(position);
        //spawns Item
        GameObject plane = Instantiate(objectToSpawn, position, transform.rotation);
        manager.CreateItem(plane);
        plane.GetComponent<PlaneItem>().car =  car;

        plane.SetActive(true);

    }
}
