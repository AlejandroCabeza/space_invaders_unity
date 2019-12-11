using UnityEngine;
using System.Collections;

public class LimitMovementBorder : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    checkBorderCollision();
	}

    void checkBorderCollision()
    {
        Vector3 cPosition = Camera.main.WorldToViewportPoint(this.transform.position);

        if (cPosition.x <= 0) cPosition.x = 0;
        if (cPosition.x >= 1) cPosition.x = 1;
        if (cPosition.y <= 0) cPosition.y = 0;
        if (cPosition.y >= 1) cPosition.y = 1;

        transform.position = Camera.main.ViewportToWorldPoint(cPosition);
    }
}
