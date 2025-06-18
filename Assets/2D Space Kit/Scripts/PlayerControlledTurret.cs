using UnityEngine;
using System.Collections;

public class PlayerControlledTurret : MonoBehaviour
{

    public GameObject weapon_prefab;
    public GameObject[] barrel_hardpoints;
    public float shot_speed;
    public float fireRate = 2f; // Shots per second
    private float nextFireTime = 0f; // Tracks when the next shot can fire
    private int barrel_index = 0;

    // Use this for initialization
    void Start()
    {
        // Check if barrel_hardpoints is properly set up
        if (barrel_hardpoints == null || barrel_hardpoints.Length == 0)
        {
            Debug.LogError("barrel_hardpoints is not assigned or empty in " + gameObject.name);
        }
        if (weapon_prefab == null)
        {
            Debug.LogError("weapon_prefab is not assigned in " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Shoot when Space is pressed and enough time has passed
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime && barrel_hardpoints != null && barrel_hardpoints.Length > 0)
        {
            // Ensure barrel_index is valid
            if (barrel_index >= barrel_hardpoints.Length)
            {
                Debug.LogWarning("Resetting barrel_index: was " + barrel_index + ", array length: " + barrel_hardpoints.Length);
                barrel_index = 0;
            }

            // Check if the current barrel is valid
            if (barrel_hardpoints[barrel_index] != null)
            {
                GameObject bullet = (GameObject)Instantiate(weapon_prefab, barrel_hardpoints[barrel_index].transform.position, transform.rotation);
                bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * shot_speed);
                bullet.GetComponent<Projectile>().firing_ship = transform.parent.gameObject;
                barrel_index++; // Cycle sequentially through the barrels in the barrel_hardpoints array

                if (barrel_index >= barrel_hardpoints.Length)
                    barrel_index = 0;

                nextFireTime = Time.time + (1f / fireRate); // Set time for next shot
            }
            else
            {
                Debug.LogWarning("Barrel at index " + barrel_index + " is null in " + gameObject.name);
                barrel_index++; // Skip invalid barrel
                if (barrel_index >= barrel_hardpoints.Length)
                    barrel_index = 0;
            }
        }
    }
}