using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour {
    public static gameController instance;
    public Camera cam;
    public GameObject star;
    public GameObject meteorite;
    public GameObject sessionOver;
    public GameObject launchButton;
    public GameObject leaderboardButton;
    public GameObject menuText;
    public GameObject levelUpText;
    private float maxWidth;
    private float prevTime;
    private float cTime;
    public float timeLeft;
    private int levelCount;
    private float meteoriteRate;
    public Text timeText;
    public bool previousSpawn;
    public Vector3 previousSpawnPosition;
    private bool playing;
    public rocketControl rocketcontrol;
    public starController starControl;
    public meteoriteController meteoriteControl;
    public ScrollingObject scroller;
    public ScrollingObject scroller2;
    private float gameSpeed;
    private bool levelChange = false;
    private float startTime;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    // Use this for initialization
    void Start () {
        levelCount = 1;
        startTime = timeLeft;
        cTime = startTime / 8;
        playing = false;
        meteoriteRate = 0.2f;
        gameSpeed = 2.0f;
        SpriteRenderer myRenderer = star.GetComponent<SpriteRenderer>();
        if (cam == null)
            cam = Camera.main;
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        float starWidth = myRenderer.bounds.extents.x;
        maxWidth = targetWidth.x - starWidth;
        previousSpawn = false;
        prevTime = startTime;
        UpdateText();
	}
	
	// Update is called once per physics time instance
	void FixedUpdate () {
        if (playing)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
                timeLeft = 0;
            UpdateText();
        }

    }
    IEnumerator Spawn()
    {

        yield return new WaitForSeconds(2.0f);

        if (timeLeft > 0 && !previousSpawn)
            NewSpawn();
        while (timeLeft > 0 && previousSpawn)
        {
            if (timeLeft < (prevTime - cTime))
            {
                levelCount += 1;
                meteoriteRate += (float)levelCount*0.02f;
                Debug.Log(gameSpeed);
                prevTime -= cTime;
                levelChange = true;
                levelUpText.SetActive(true);
                yield return new WaitForSeconds(1.5f);
                levelUpText.SetActive(false);
                gameSpeed *= 1.10f;
            }

            scroller.SetScrollSpeed(-gameSpeed);
            scroller2.SetScrollSpeed(-gameSpeed);
            yield return new WaitForSeconds(Random.Range(1.0f/levelCount, 2.0f/levelCount));
            float xSpawn = previousSpawnPosition.x + Random.Range(-1.5f, 1.5f);
            if (xSpawn < -maxWidth)
                xSpawn = -maxWidth;
            else if (xSpawn > maxWidth)
                xSpawn = maxWidth;
            Vector3 spawnPosition = new Vector3(xSpawn, transform.position.y, 0.0f);
            Quaternion spawnRotation = Quaternion.identity;
            previousSpawnPosition = spawnPosition;
            if (Random.Range(0.0f, 1.0f) < meteoriteRate)
            {
                meteoriteControl.SetSpeed(gameSpeed);
                Instantiate(meteorite, spawnPosition, spawnRotation);
                
            }
            else
            {
                starControl.SetSpeed(gameSpeed);
                Instantiate(star, spawnPosition, spawnRotation);
                
            }
            if (Random.Range(0.0f, 1.0f)< 0.3f)
            {
                yield return new WaitForSeconds(Random.Range(0.0f, 1.0f/levelCount));
                NewSpawn();                
            }
        }
        rocketcontrol.CanControl(false);       
        yield return new WaitForSeconds(1.0f);
        sessionOver.SetActive(true);

    }
    void UpdateText()
    {
        timeText.text = "Time left:\n" + Mathf.RoundToInt(timeLeft);
    }
    void NewSpawn()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-maxWidth, maxWidth), transform.position.y, 0.0f);
        Quaternion spawnRotation = Quaternion.identity;
        previousSpawnPosition = spawnPosition;
        starControl.SetSpeed(gameSpeed);
        Instantiate(star, spawnPosition, spawnRotation);
        previousSpawn = true;
    }
    public void Launch()
    {
        launchButton.SetActive(false);
        leaderboardButton.SetActive(false);
        menuText.SetActive(false);
        StartCoroutine(Spawn());
        playing = true;
        rocketcontrol.CanControl(true);
    }

}
