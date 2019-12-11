using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject Invader;
    public static bool hasSpaceshipDied = false;
    public static bool hasGameEnded = false;

    public static int InvadersInScreen = 0;
    public static int InvadersKilled = 0;

    public bool Spawn;
    public float TimeBetweenSpawns;
    public int MaxInvadersInScreen;
    public int InvadersToWin = 1;

    private float timeSinceSpawn = 0;
    private GameObject uiLifesBackground;
   
    // Use this for initialization
    void Start ()
    {
        uiLifesBackground = GameObject.Find("GUI").transform.Find("LifesBackground").gameObject;

        GameObject options  = GameObject.Find("Options").transform.Find("OptionsScreen").gameObject;

        Spawn               =      options.transform.Find("MothershipOptionsGroup").gameObject.transform.Find("MothershipSpawn")              .gameObject.GetComponent<Toggle>().isOn;
        TimeBetweenSpawns   =      options.transform.Find("MothershipOptionsGroup").gameObject.transform.Find("MothershipTimeBetweenSpawns")  .gameObject.GetComponent<Slider>().value;
        MaxInvadersInScreen = (int)options.transform.Find("MothershipOptionsGroup").gameObject.transform.Find("MothershipMaxInvadersInScreen").gameObject.GetComponent<Slider>().value;
        InvadersToWin       = (int)options.transform.Find("MothershipOptionsGroup").gameObject.transform.Find("MothershipInvadersToWin")      .gameObject.GetComponent<Slider>().value;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (!hasGameEnded && !hasSpaceshipDied && InvadersKilled >= InvadersToWin)
	    {
            hasGameEnded = true;
            GameObject.Find("GUI").transform.Find("WinBackground").gameObject.SetActive(true);
            uiLifesBackground.SetActive(false);
        }
	    else if (!hasGameEnded && hasSpaceshipDied)
	    {
	        hasGameEnded = true;
            GameObject.Find("GUI").transform.Find("LoseBackground").gameObject.SetActive(true);
            uiLifesBackground.SetActive(false);
        }

	    timeSinceSpawn += Time.deltaTime;
	    if (timeSinceSpawn >= TimeBetweenSpawns && InvadersInScreen < MaxInvadersInScreen && Spawn && !hasGameEnded)
	    {
	        timeSinceSpawn = 0.0f;
	        Instantiate(Invader, transform.position, Invader.transform.rotation);
	    }
    }

    public void SetSpawn(bool newValue)
    {
        Spawn = newValue;
    }

    public void SetInvadersToWin(float newValue)
    {
        InvadersToWin = (int)newValue;
    }

    public void SetMaxInvaders(float newValue)
    {
        MaxInvadersInScreen = (int)newValue;
    }

    public void SetTimeBetweenSpawns(float newValue)
    {
        TimeBetweenSpawns = newValue;
    }
}
