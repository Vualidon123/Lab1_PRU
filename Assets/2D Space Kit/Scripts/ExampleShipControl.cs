using UnityEngine;
using System.Collections;

public class ExampleShipControl : MonoBehaviour {

	public float acceleration_amount = 1f;
	public float rotation_speed = 1f;
	public GameObject turret;
	public float turret_rotation_speed = 3f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(KeyCode.Escape))
            Screen.lockCursor = !Screen.lockCursor;

        // Move up with Up Arrow
        if (Input.GetKey(KeyCode.UpArrow))
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * acceleration_amount * Time.deltaTime);
        }
        // Move down with Down Arrow
        if (Input.GetKey(KeyCode.DownArrow))
        {
            GetComponent<Rigidbody2D>().AddForce((-transform.up) * acceleration_amount * Time.deltaTime);
        }
        // Move left with Left Arrow (increased speed)
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            GetComponent<Rigidbody2D>().AddForce((-transform.right) * acceleration_amount * 1.5f * Time.deltaTime);
        }
        // Move right with Right Arrow (increased speed)
        if (Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<Rigidbody2D>().AddForce(transform.right * acceleration_amount * 1.5f * Time.deltaTime);
        }

        // Stop movement and rotation with C key
        if (Input.GetKey(KeyCode.C))
        {
            GetComponent<Rigidbody2D>().angularVelocity = Mathf.Lerp(GetComponent<Rigidbody2D>().angularVelocity, 0, rotation_speed * 0.06f * Time.deltaTime);
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.Lerp(GetComponent<Rigidbody2D>().linearVelocity, Vector2.zero, acceleration_amount * 0.06f * Time.deltaTime);
        }

        // Reset position to origin with H key
        if (Input.GetKey(KeyCode.H))
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
