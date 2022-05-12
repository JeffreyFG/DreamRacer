// When t is pressed, look for closest track and reload stopped there

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReloadOnTrack2 : MonoBehaviour
{
    public GameObject car1;
    public GameObject car2;
    // public GameObject TrackItem;
    // Update is called once per frame
    void Update()
    {
        string trgt = "Reload"; // Track of target
        if(Input.GetKeyDown("t")){
            if (GameManager.isSinglePlayer == true){
                Vector3 position = car1.transform.position;
                GameObject TrackItem = GameObject.FindGameObjectsWithTag(trgt)
                    .OrderBy(o => (o.transform.position - position).sqrMagnitude)
                    .FirstOrDefault();
                car1.GetComponent<CarController>().isStopped = true;
                car1.transform.rotation = new Quaternion(0.0f, 90.0f, 0.0f, 90.0f);
                // car1.transform.position = new Vector3(TrackItem.transform.position.z, TrackItem.transform.position.x, TrackItem.transform.position.y);
                car1.transform.position = new Vector3(TrackItem.transform.position.x, TrackItem.transform.position.y, TrackItem.transform.position.z);

                Destroy(TrackItem);
            }
            else if (GameManager.currentPlayer == 1 && GameManager.isSinglePlayer == false){
                Vector3 position = car1.transform.position;
                GameObject TrackItem = GameObject.FindGameObjectsWithTag(trgt)
                    .OrderBy(o => (o.transform.position - position).sqrMagnitude)
                    .FirstOrDefault();
                car1.GetComponent<CarController>().isStopped = true;
                car1.transform.rotation = new Quaternion(0.0f, 90.0f, 0.0f, 90.0f);
                car1.transform.position = new Vector3(TrackItem.transform.position.x, TrackItem.transform.position.y, TrackItem.transform.position.z);
                Destroy(TrackItem);
            }
            else if (GameManager.currentPlayer == 2){
                Vector3 position = car2.transform.position;
                GameObject TrackItem = GameObject.FindGameObjectsWithTag(trgt)
                    .OrderBy(o => (o.transform.position - position).sqrMagnitude)
                    .FirstOrDefault();
                car2.GetComponent<CarController>().isStopped = true;
                car2.transform.rotation = new Quaternion(0.0f, 90.0f, 0.0f, 90.0f);
                car1.transform.position = new Vector3(TrackItem.transform.position.x, TrackItem.transform.position.y, TrackItem.transform.position.z);
                Destroy(TrackItem);
            }
        }
     }
}
