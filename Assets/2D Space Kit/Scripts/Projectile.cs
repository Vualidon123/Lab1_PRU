using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public GameObject shoot_effect;
	public GameObject hit_effect;
	public GameObject firing_ship;

    private GameManager gameManager;

    void Awake()
    {
        // ✅ Correct: Safe to call here
        gameManager = FindObjectOfType<GameManager>();
    } // Reference to the GameManager script
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
        if (other.CompareTag("Projectile")) // Use the correct tag for your targets
        {
            if (gameManager != null)
            {
                gameManager.UpdateScore();
                // Increment score and update UI
            }
            Instantiate(hit_effect, transform.position, Quaternion.identity);
           
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }



}
