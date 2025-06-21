using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public GameObject shoot_effect;
	public GameObject hit_effect;
	public GameObject firing_ship;
	
	// Use this for initialization
	void Start () {
		GameObject obj = (GameObject) Instantiate(shoot_effect, transform.position  - new Vector3(0,0,5), Quaternion.identity); //Spawn muzzle flash
		obj.transform.parent = firing_ship.transform;
		Destroy(gameObject, 5f); //Bullet will despawn after 5 seconds
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        // Optionally check for a tag or component to ensure it's a valid target
        if (other.CompareTag("Projectile")) // Make sure your target objects have the "Target" tag
        {
            Instantiate(hit_effect, transform.position, Quaternion.identity);
            Destroy(other.gameObject); // This makes the target disappear
            Destroy(gameObject); // Optionally destroy the projectile as well
        }
    }



}
