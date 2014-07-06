 using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    public float Speed = 100;
    public float LifeTime = 1.5f;
    public float Distance = 20;
    public int Damage = 1;

    private float SpawnTime = 0.0f;

	// Use this for initialization
	void Start ()
    {
        this.SpawnTime = Time.time;
	}
	
	// Update is called once per frame
    void Update()
    {
        this.transform.position += this.transform.forward * this.Speed * Time.deltaTime;
        this.Distance -= this.Speed * Time.deltaTime;
        if (Time.time > this.SpawnTime + this.LifeTime || this.Distance < 0)
            Destroy(this.gameObject);
	}

    // Collision
    void OnTriggerEnter(Collider collision)
    {
        //if (collision.gameObject.tag == "Cop")
        //{
        //    collision.gameObject.GetComponent<PolicemanScript>().Touch(this.Damage);
        //    Destroy(this.gameObject);
        //}
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyControllerScript>().Touch(this.Damage);
            Destroy(this.gameObject);
        }
    }
}
