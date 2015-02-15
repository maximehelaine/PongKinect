using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private int _scorePlayers = 0;
    [SerializeField]
    private int _currentScore = 0;
    [SerializeField]
    private int _lifePlayer = 3;

    [SerializeField]
    private string _tagToSetScore = "DefendZone";


    //Ball Parameter
    [SerializeField]
    private GameObject _ballObject;
    [SerializeField]
    private Transform _ballTransform;
    [SerializeField]
    private Rigidbody _ballRigibody;
    [SerializeField]
    private Transform _startBallTransform;
    [SerializeField]
    private float _startImpulsion = 10.0f;

    //Gui Parameter
    [SerializeField]
    private Text _textCurrentScore;
    [SerializeField]
    private Text _textGameScore;
    [SerializeField]
    private Text _textFinalScore;
    [SerializeField]
    private Text _textLifePlayer;
    [SerializeField]
    private GameObject _panelFinishGame;

    //private Members
    private bool _isFinish = false;

    // Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {

    }
    public void setScorePlayer(int score) { _scorePlayers = score; }
    public void setCurrentScore(int score) { _currentScore = score; }
    public void setLifePlayer(int life) { _lifePlayer = life; }
    public void setCurrentScoreDisplay(int score) { _textCurrentScore.text = "CurrentScore : " + score + " pts"; }
    public void setGameScoreDisplay(int score) { _textGameScore.text = "GameScore : " + score + " pts"; }
    public void setFinalScoreDisplay(int score) { _textFinalScore.text = score + " pts"; }
    public void setLifeDisplay(int life) { _textLifePlayer.text = "Life : " + life; }
    public void setIsFinish(bool value) { _isFinish = value; }
    
    public void addGameScorePlayer(int value) { _scorePlayers += value; }
    public void addCurrentScore(int value) { _currentScore += value; }
    public void addLifePlayer(int value) { _lifePlayer += value; }

    public int getScorePlayer() { return _scorePlayers; }
    public int getCurrentScore() { return _currentScore; }
    public int getLifePlayer() { return _lifePlayer; }
    public string getCurrentScoreDisplay() { return _textCurrentScore.text; }
    public string getGameScoreDisplay() { return _textGameScore.text; }
    public string getFinalScoreDisplay() { return _textFinalScore.text; }
    public string getLifeDisplay() { return _textLifePlayer.text; }
    public string getTagToSetScore() { return _tagToSetScore; }
    public bool isFinish() { return _isFinish; }

    public void startNewLap()
    {
        addGameScorePlayer(_currentScore);
        setCurrentScore(0);
        setCurrentScoreDisplay(0);
        setGameScoreDisplay(_scorePlayers);
        setLifeDisplay(_lifePlayer);
        resetPositionBall();
        if (_lifePlayer == 0)
        {
            _ballRigibody.velocity = new Vector3(0, 0, 0);
            setFinalScoreDisplay(_scorePlayers);
            _panelFinishGame.SetActive(true);
            _isFinish = true;
            return;
        }
        addLifePlayer(-1);
        impulseBall(Vector3.forward, _startImpulsion);
    }

    public void startNewGame()
    {
        _isFinish = false;
        _panelFinishGame.SetActive(false);
        setScorePlayer(0);
        setGameScoreDisplay(0);
        setLifePlayer(3);
        startNewLap();
    }

    public void exitGame()
    {
        Application.Quit();
    }
    //Ball Functions
    public void impulseBall(Vector3 direction, float value)
    {
        _ballRigibody.AddForce(direction * value, ForceMode.Impulse);
    }
    public void resetPositionBall()
    {
        _ballTransform.position = new Vector3(_startBallTransform.position.x, _startBallTransform.position.y, _startBallTransform.position.z); 
    }
}
