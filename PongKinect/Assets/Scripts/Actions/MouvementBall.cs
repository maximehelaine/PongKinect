using UnityEngine;
using System.Collections;

public class MouvementBall : MonoBehaviour {
    [SerializeField]
    private float MinVelocity = 5.0f;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        ClampMagnitude();
	}
    //Permet de garder une vitesse minimum pour la balle
    void ClampMagnitude()
    {
        Vector3 vel = rigidbody.velocity;
        if (vel.magnitude < MinVelocity)
            rigidbody.velocity = vel.normalized * MinVelocity;
    }

}
