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
    private int whoSetScore = 0;
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
        GameObject go = collision.gameObject;
        if (go.tag.Equals(TagToBump))
        {
            if (gameObject.tag.Equals(GM.getTagToSetScore()))
            {
                GM.addScorePlayer(whoSetScore, GM.getCurrentScore());
                GM.setCurrentScore(0);
            }
            else
                GM.addCurrentScore(ScoreBonus);

            Vector3 vel = go.rigidbody.velocity;
            go.rigidbody.velocity = vel.normalized * MultiplyPower;
        }
        
	}
}
