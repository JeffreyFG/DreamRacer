// When t is pressed, look for closest track and reload stopped there

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReloadOnTrack : MonoBehaviour
{
    public GameObject car1;
    public GameObject car2;
    // public GameObject TrackItem;
    // Update is called once per frame
    void Update()
    {
        string trgt = "Track"; // Track of target
        if(Input.GetKeyDown("t")){
            if (GameManager.isSinglePlayer == true){
                Vector3 position = car1.transform.position;
                GameObject TrackItem = GameObject.FindGameObjectsWithTag(trgt)
                    .OrderBy(o => (o.transform.position - position).sqrMagnitude)
                    .FirstOrDefault();
                car1.GetComponent<CarController>().isStopped = true;
                car1.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
                car1.transform.position = new Vector3(TrackItem.transform.position.x - 6.7f , TrackItem.transform.position.y + 11.844913f, TrackItem.transform.position.z);
            }
            if (GameManager.currentPlayer == 1){
                Vector3 position = car1.transform.position;
                GameObject TrackItem = GameObject.FindGameObjectsWithTag(trgt)
                    .OrderBy(o => (o.transform.position - position).sqrMagnitude)
                    .FirstOrDefault();
                car1.GetComponent<CarController>().isStopped = true;
                car1.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
                car1.transform.position = new Vector3(TrackItem.transform.position.x - 6.7f , TrackItem.transform.position.y + 11.844913f, TrackItem.transform.position.z);
            }
            if (GameManager.currentPlayer == 2){
                Vector3 position = car2.transform.position;
                GameObject TrackItem = GameObject.FindGameObjectsWithTag(trgt)
                    .OrderBy(o => (o.transform.position - position).sqrMagnitude)
                    .FirstOrDefault();
                car2.GetComponent<CarController>().isStopped = true;
                car2.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
                car2.transform.position = new Vector3(TrackItem.transform.position.x - 13.9f , TrackItem.transform.position.y + 11.844913f, TrackItem.transform.position.z);
            }
        }
     }
}
