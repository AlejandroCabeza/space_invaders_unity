using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class Bullet : MonoBehaviour
{
    public float Speed;
    public static List<Bullet> BulletsInScreen = new List<Bullet>();

	// Use this for initialization
	void Start ()
	{
        BulletsInScreen.Add(this);
	    this.GetComponent<Rigidbody>().velocity = transform.up * Speed;

        GameObject options = GameObject.Find("Options").transform.Find("OptionsScreen").gameObject;

        Speed = options.transform.Find("SpaceshipBulletOptionsGroup").gameObject.transform.Find("SpaceshipBulletSpeed").gameObject.GetComponent<Slider>().value;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector3 cPosition = Camera.main.WorldToViewportPoint(this.transform.position);
        if (cPosition.y >= 1) Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision coll)
    {
        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        BulletsInScreen.Remove(this);
    }

    public void SetSpeed(float newValue)
    {
        Speed = newValue;
    }
}