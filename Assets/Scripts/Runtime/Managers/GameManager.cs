using Random = UnityEngine.Random;
using System.Collections;
using UnityEngine;
using System;


public class GameManager : GameStateManager
{
    private CameraShake cameraShake;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform cursor;
    [SerializeField] private float cursorOffSet;

    [SerializeField] private int offscreenOffSet = 15;

    [Header("Spawning"), Space(20)]
    [SerializeField] private int numberOfAsteroidsToSpawnInTotal = 5;

    #region Properties
    public Camera MainCamera => mainCamera;
    public Bounds bounds { get; private set; }
    public bool IsPaused { get; private set; }

    public int OffScreenOffset => offscreenOffSet;
    #endregion

    private UIManager uIManager;
    private Action OnRespawnPlayer;

    private Action<UIData> OnAddPoint;
    private int amountToWin;
    private int remainingAsteroids;
    private int totalPoints;
    private int numberOfPlayerLives = 3;

    private float timeElapsedToNextSpawn = 3f;
    private float cachedelapsed;
    private int astroidCounter;

    private GameObject player;

    [Header("Fx"), Space(20)] //TODO:: EXTRACT
    [SerializeField] private float stopTime = 0.2f;
    [SerializeField] private float slowTime = 0.2f;
    private bool stopping;

    #region Unity Calls

    private void Start()
    {
        bounds = new Bounds(Helpers.GetScreenCorners(mainCamera));


        cachedelapsed = timeElapsedToNextSpawn;

        cameraShake = GetComponent<CameraShake>();
        cameraShake.SetUpCameraTransform(mainCamera);

        //TODO:: ADD WITH EACH TIME PLAYER WINS
        amountToWin = (numberOfAsteroidsToSpawnInTotal * 7);
        remainingAsteroids = amountToWin;

        uIManager = GetComponent<UIManager>();

        StartCoroutine(CountDownToNewWave());
    }

    private void Update()
    {
        CurserMovement();

        TimeToSpawnNewAstroid();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsPaused)
            {
                IsPaused = true;
                Time.timeScale = 0f;
            }
            else
            {
                IsPaused = !IsPaused;
                Time.timeScale = 1f;
            }
            uIManager.ShowPauseScreen(IsPaused);
            //TODO:: OnPause
        }

        if (Input.GetKeyDown(KeyCode.Y)) NewWave();
    }

    #endregion

    #region Subscriptions
    public void RegisterPoints(Action<UIData> action)
    {
        OnAddPoint += action;
    }
    public void RegisterPlayerAndOnSpawnAction(Action RespawnAction, GameObject player)
    {
        this.player = player;
        OnRespawnPlayer += RespawnAction;
    }
    #endregion

    public void AddPoints(int points)
    {
        totalPoints += points;
        remainingAsteroids--;
        CollectAndSendGameData();
        HitFX();

        if (remainingAsteroids <= 0)
        {
            // TODO:: WIN STATE FEEDBACK
            NewWave();
        }
    }

    private void NewWave()
    {
        StartCoroutine(CountDownToNewWave());

        numberOfAsteroidsToSpawnInTotal += 2;
        numberOfPlayerLives = 3;
        amountToWin = (numberOfAsteroidsToSpawnInTotal * 7);
        remainingAsteroids = amountToWin;
        astroidCounter = 0;
        CollectAndSendGameData();
    }

    // TODO:: PLACE HOLDER SYSTEM
    private IEnumerator CountDownToNewWave()
    {
        uIManager.ShowCountDown(true);
        yield return new WaitForSeconds(1);
        uIManager.ChangeCountDownText("3");
        yield return new WaitForSeconds(1);
        uIManager.ChangeCountDownText("2");

        yield return new WaitForSeconds(1);
        uIManager.ChangeCountDownText("1");
        yield return new WaitForSeconds(1);
        uIManager.ChangeCountDownText("Go!");
        yield return new WaitForSeconds(1);

        CollectAndSendGameData();
        uIManager.ShowCountDown(false);
    }

    public void RestartLevel()
    {
        numberOfPlayerLives = 3;
        amountToWin = (numberOfAsteroidsToSpawnInTotal * 7);
        remainingAsteroids = amountToWin;
        astroidCounter = 0;
        CollectAndSendGameData();
    }

    private void CollectAndSendGameData()
    {
        UIData uIData;

        uIData.points = totalPoints;
        uIData.totalAstroidsToWin = amountToWin;
        uIData.reminderAstroids = remainingAsteroids;
        uIData.totalLives = numberOfPlayerLives;

        OnAddPoint?.Invoke(uIData);
    }


    #region FX 
    // TODO:: REFRACTER
    private void HitFX()
    {
        if (!stopping)
        {
            stopping = true;
            Time.timeScale = 0;

            StartCoroutine(CR_HitFx());
            cameraShake.ShakeCamera();
        }
    }

    private IEnumerator CR_HitFx()
    {
        yield return new WaitForSecondsRealtime(stopTime);

        Time.timeScale = 0.01f;

        yield return new WaitForSecondsRealtime(slowTime);

        Time.timeScale = 1;
        stopping = false;
    }
    #endregion

    public void PlayerDied()
    {
        numberOfPlayerLives -= 1;

        if (numberOfPlayerLives <= 0)
        {
            AudioManager.Instance.PlayOnce(SoundFX.GameOver);
            player.SetActive(false);
            uIManager.ShowPauseScreen(true);
            // TODO:: SPAWN PARTICLES
        }
        else
        {
            AudioManager.Instance.PlayOnce(SoundFX.LostLife);
            RespawnPlayer();
        }

        CollectAndSendGameData();
    }

    private void RespawnPlayer() => OnRespawnPlayer?.Invoke();

    private void CurserMovement()
    {
        var screenPoint = Input.mousePosition;
        screenPoint.z = mainCamera.transform.position.y - cursorOffSet;
        cursor.transform.position = mainCamera.ScreenToWorldPoint(screenPoint);
    }

    private void TimeToSpawnNewAstroid()
    {
        if (astroidCounter < numberOfAsteroidsToSpawnInTotal)
        {
            timeElapsedToNextSpawn -= Time.deltaTime;

            if (timeElapsedToNextSpawn <= 0)
            {
                AsteroidsPoolingSystem.Instance.SpawnAstroid(AsteroidsSize.Large);
                timeElapsedToNextSpawn = cachedelapsed;
                astroidCounter += 1;
            }
        }
    }

    // TODO:: MOVE INTO ASTROID SCRIPT
    public Vector3 GetRandomPositionOffScreen()
    {
        Direction positionOfSpawn = Helpers.GetRandomDirection();
        Vector3 position = Vector3.zero;

        switch (positionOfSpawn)
        {
            case Direction.Right:
                position = new Vector3(bounds.xMax + offscreenOffSet, 0f, Random.Range(bounds.zMin, bounds.zMax));
                break;
            case Direction.Left:
                position = new Vector3(bounds.xMin - offscreenOffSet, 0f, Random.Range(bounds.zMin, bounds.zMax));
                break;
            case Direction.Top:
                position = new Vector3(Random.Range(bounds.xMin, bounds.xMax), 0f, bounds.zMax + offscreenOffSet);
                break;
            case Direction.Bottom:
                position = new Vector3(Random.Range(bounds.xMin, bounds.xMax), 0f, bounds.zMin - offscreenOffSet);
                break;
        }
        return position;
    }
}
