using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]

public class Spaceship : MonoBehaviour
{
    private float left;
    private float right;
    private bool shoot;

    private GameObject uiLifesBackground;
    private Text uiLifesDisplay;
    private AudioSource audio;
    private ParticleSystem particle;

    public bool CanShoot = true;
    public static int MaximumAmountOfBullets = 3;

    private int Lifes = 5;
    public GameObject bullet;
    public static Spaceship _instance ;
    public Animator Animator;
    

    // Use this for initialization
    void Start ()
    {
        _instance = this;
        shoot = false;

        uiLifesDisplay = GameObject.Find("GUI").transform.Find("LifesBackground").transform.Find("LifesDisplay").GetComponent<Text>();

        uiLifesDisplay.text = "Lifes: " + Lifes;

        GameObject options = GameObject.Find("Options").transform.Find("OptionsScreen").gameObject;

        CanShoot               =      options.transform.Find("SpaceshipOptionsGroup").gameObject.transform.Find("SpaceshipCanShoot")              .gameObject.GetComponent<Toggle>().isOn;
        MaximumAmountOfBullets = (int)options.transform.Find("SpaceshipOptionsGroup").gameObject.transform.Find("SpaceshipMaximumAmountOfBullets").gameObject.GetComponent<Slider>().value;

        audio = GetComponentInChildren<AudioSource>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    void FixedUpdate()
    {
        Animator.SetFloat("Direction", Input.GetAxis("Horizontal"));
        if (!Animator.GetBool("Shoot") && shoot && Bullet.BulletsInScreen.Count < MaximumAmountOfBullets) Animator.SetBool("Shoot", true);
        resetInput();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    checkInput();   
	}

    void Shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        audio.Play();
        particle.Play();
        Animator.SetBool("Shoot", false);
    }

    void checkInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanShoot) shoot = true;
    }

    void resetInput()
    {
        if (!shoot && Animator.GetBool("Shoot")) Animator.SetBool("Shoot", shoot);
        shoot = false;
    }

    void OnCollisionEnter(Collision coll)
    {
        if (!Manager.hasGameEnded && coll.collider.gameObject.layer != LayerMask.NameToLayer("RLLimits"))
        {
            --Lifes;
            uiLifesDisplay.text = "Lifes: " + Lifes;

            if (Lifes <= 0)
            {
                GetComponent<Rigidbody>().detectCollisions = false;
                Manager.hasSpaceshipDied = true;
                Animator.SetBool("Death", true);
            }
        }
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }

    public void SetCanShoot(bool newValue)
    {
        CanShoot = newValue;
    }

    public void SetMaximumBulletsInScreen(float newValue)
    {
        MaximumAmountOfBullets = (int)newValue;
    }
}