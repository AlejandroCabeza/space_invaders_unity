using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class EnemyBullet : MonoBehaviour
{
    public float Speed;
    public static List<EnemyBullet> EnemyBulletsInScreen = new List<EnemyBullet>();

    private AudioSource audio;
    private ParticleSystem particle;

    // Use this for initialization
    void Start()
    {
        EnemyBulletsInScreen.Add(this);
        this.GetComponent<Rigidbody>().velocity = -transform.up * Speed;

        GameObject options = GameObject.Find("Options").transform.Find("OptionsScreen").gameObject;

        Speed = options.transform.Find("InvaderBulletOptionsGroup").gameObject.transform.Find("InvaderBulletSpeed").gameObject.GetComponent<Slider>().value;

        audio = GetComponentInChildren<AudioSource>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 cPosition = Camera.main.WorldToViewportPoint(this.transform.position);
        if (cPosition.y <= 0) Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            particle.Play();
            audio.PlayOneShot(audio.clip);
            gameObject.GetComponent<Renderer>().enabled = false;
            Destroy(gameObject, audio.clip.length);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        EnemyBulletsInScreen.Remove(this);
    }

    public void SetSpeed(float newValue)
    {
        Speed = newValue;
    }
}