using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player, hole;
    Vector3 offset, negOffset;  // offset between camera and player
    Vector3 rotation;  // absolute transform rotation values of camera

    // Use this for initialization
    void Start () {
        offset = transform.position - player.transform.position;
        negOffset = Vector3.Scale(offset, new Vector3(-1f, 1f, 1f));  // offset on opposite side of player
        rotation = transform.eulerAngles;
    }

    // Update is called once per frame. Guaranteed to run after all objects updated.
    void LateUpdate () {
        // If player moving
        if (player.GetComponent<Rigidbody>().velocity.x > 0.005f ||
            player.GetComponent<Rigidbody>().velocity.y > 0.005f ||
            player.GetComponent<Rigidbody>().velocity.z > 0.005f) {
            if ((player.transform.position.x + offset.x) < (hole.transform.position.x + 15)) { // if camera would be beyond hole
                transform.position = player.transform.position + negOffset;  // follow player 
                transform.eulerAngles = new Vector3(rotation.x, rotation.y + 180, rotation.z);  // turn camera south 
            }
            else {
                transform.position = player.transform.position + offset;  // follow player 
                transform.eulerAngles = new Vector3(rotation.x, rotation.y, rotation.z);  // turn camera north 
            }
        }  // If Input
        else if (Input.GetKey("left") || Input.GetKey("a"))
            transform.RotateAround(player.transform.position, Vector3.up, 20 * Time.deltaTime);  // turn camera clockwise 
        else if (Input.GetKey("right") || Input.GetKey("d"))
            transform.RotateAround(player.transform.position, Vector3.up, -20 * Time.deltaTime);  // turn camera counter-clockwise
    }
}
