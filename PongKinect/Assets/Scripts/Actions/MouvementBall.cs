using UnityEngine;
using System.Collections;

public class MouvementBall : MonoBehaviour {
    [SerializeField]
    private float MinVelocity = 1.0f;
	// Use this for initialization
	void Start () {
        rigidbody.AddForce(Vector3.forward , ForceMode.Acceleration);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        ClampMagnitude();
	}
    void ClampMagnitude()
    {
        Vector3 vel = rigidbody.velocity;
        if (vel.magnitude < MinVelocity)
            rigidbody.velocity = vel.normalized * MinVelocity;
    }

}
