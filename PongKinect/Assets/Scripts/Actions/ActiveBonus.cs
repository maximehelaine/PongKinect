using UnityEngine;
using System.Collections;

public class ActiveBonus : MonoBehaviour {

    [SerializeField]
    private GameManager GM;
    [SerializeField]
    private bool _bonusX2;
    [SerializeField]
    private bool _bonusSlow;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (_bonusX2)
            GM.addCurrentScore(GM.getCurrentScore());
        if (_bonusSlow)
        {
            Vector3 vel = other.rigidbody.velocity;
            other.rigidbody.velocity = vel.normalized * vel.magnitude/2;
                
        }
        GM.setoneBonusIsAction(false);
        gameObject.SetActive(false);
    }
}
