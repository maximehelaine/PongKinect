using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private int[] _scorePlayers = new int[2];
    [SerializeField]
    private int _currentScore = 0;

    [SerializeField]
    private string _tagToSetScore = "DefendZone";
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setScorePlayer(int idPlayer, int score) { _scorePlayers[idPlayer] = score; }
    public void setCurrentScore(int score) { _currentScore = score; }
    public void addScorePlayer(int idPlayer, int value) { _scorePlayers[idPlayer] += value; }
    public void addCurrentScore(int value) { _currentScore += value; }
    public int getScorePlayer(int idPlayer) { return _scorePlayers[idPlayer]; }
    public int getCurrentScore() { return _currentScore; }
    public string getTagToSetScore() { return _tagToSetScore; }
}
