using UnityEngine;
using System.Collections;

public class BumperPower : MonoBehaviour {

    [SerializeField]
    private GameManager GM;
    
    [SerializeField]
    private float MultiplyPower = 1.0f;
    [SerializeField]
    private string TagToBump = "Ball";
    [SerializeField]
    private int ScoreBonus = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision collision) 
    {
        if(GM.isFinish())
            return;
        GameObject go = collision.gameObject;
        if (go.tag.Equals(TagToBump))
        {
            if (gameObject.tag.Equals(GM.getTagToSetScore()))
            {
                GM.startNewLap();
            }
            else
            {
                GM.addCurrentScore(ScoreBonus);
                GM.setCurrentScoreDisplay(GM.getCurrentScore());
            }

            Vector3 vel = go.rigidbody.velocity;
            go.rigidbody.velocity = vel.normalized * MultiplyPower;
        }
        
	}
}
