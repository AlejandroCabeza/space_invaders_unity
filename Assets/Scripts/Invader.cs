using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Invader : MonoBehaviour
{
    public static int BulletChance           = 10;
    public static int MaximumBulletsInScreen = 5;
    public static float MaxTimeDescending    = 1;

    private float timeDescending;

    public GameObject InvaderBullet;
    public Animator Animator;

    private AudioSource audio;
    private ParticleSystem particle;

    private bool stopMovingDown = false;

    private float timeDown = 0;

	// Use this for initialization
	void Start ()
	{
        Animator.SetBool("MoveRight", true);
        Animator.SetBool("MoveDown", false);

	    timeDescending = 0;

        ++Manager.InvadersInScreen;

        GameObject options = GameObject.Find("Options").transform.Find("OptionsScreen").gameObject;

        BulletChance           = (int)options.transform.Find("InvaderOptionsGroup").gameObject.transform.Find("InvaderBulletChance")          .gameObject.GetComponent<Slider>().value;
        MaximumBulletsInScreen = (int)options.transform.Find("InvaderOptionsGroup").gameObject.transform.Find("InvaderMaximumBulletsInScreen").gameObject.GetComponent<Slider>().value;
        MaxTimeDescending      =      options.transform.Find("InvaderOptionsGroup").gameObject.transform.Find("InvaderMaximumTimeDescending") .gameObject.GetComponent<Slider>().value;

	    audio = GetComponentInChildren<AudioSource>();
	    particle = GetComponentInChildren<ParticleSystem>();
	}

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        if (Random.Range(0, 10000) < BulletChance && EnemyBullet.EnemyBulletsInScreen.Count < MaximumBulletsInScreen) Animator.SetBool("Shoot", true);
    }

    private void Shoot()
    {
        Instantiate(InvaderBullet, transform.position, InvaderBullet.transform.rotation);
        Animator.SetBool("Shoot", false);
    }

    void Move()
    {
        if (Animator.GetBool("MoveDown"))
        {
            timeDescending += Time.deltaTime;

            if (timeDescending >= MaxTimeDescending)
            {
                timeDescending = 0;
                Animator.SetBool("MoveDown", false);
                Animator.SetBool("MoveRight", !Animator.GetBool("MoveRight"));
                stopMovingDown = true;
            }
        }


        /*
        Mechanism to avoid overlapping of the Animation parameter "MoveDown" to be set Down again when the Invader has not left the collision.
        OnCollisionExit doesn't work as intended.
        */
        if (stopMovingDown)
        {
            timeDown += Time.deltaTime;

            if (timeDown >= 1)
            {
                stopMovingDown = false;
                timeDown = 0;
            }
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.gameObject.layer == LayerMask.NameToLayer("Player") ||
            coll.collider.gameObject.layer == LayerMask.NameToLayer("Bullet") ||
            coll.collider.gameObject.layer == LayerMask.NameToLayer("TDLimits"))
        {
            Animator.SetBool("Death", true);
            audio.Play();
            particle.Play();
        }

        if (!stopMovingDown && (coll.collider.gameObject.layer == LayerMask.NameToLayer("RLLimits")) && !Animator.GetBool("MoveDown"))
        {
            Animator.SetBool("MoveDown", true);
        }
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        ++Manager.InvadersKilled;
        --Manager.InvadersInScreen;
    }

    public void SetBulletChance(float newValue)
    {
        BulletChance = (int)newValue;
    }

    public void SetMaximumBulletsInScreen(float newValue)
    {
        MaximumBulletsInScreen = (int)newValue;
    }

    public void SetDescendingTime(float newValue)
    {
        MaxTimeDescending = newValue;
    }
}
