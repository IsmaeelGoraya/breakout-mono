using UnityEngine;
using Factories;
using DataModels;
using System.Collections.Generic;
using System;

public class BrickSpawner : MonoBehaviour
{
    private Level currentLevel;
    private List<Brick> currentLevelTotalBricks;
    private List<BrickBehaviour> currentLevelBrickBehaviours;
    private float brickSpacing = 0.1f;
    private Vector2 startSpawnPosition;
    private Vector2 nextSpwanPosition;
    [SerializeField]
    private BallController ballController;
    private int bricksDestroyed;
    private SpriteRenderer brickPrefabSprtRndr;

    public GameObject brickPrefab;

    private void Awake()
    {
        brickPrefabSprtRndr = brickPrefab.GetComponent<SpriteRenderer>();
        brickSpacing = 0.1f;
        nextSpwanPosition = Vector2.zero;
        ballController.OnBallDropped += ResetCurrentLevel;
    }

    private void Start()
    {
        //TODO: Will refactor this later to text/json based
        //Create bricks with only multiples of 3 at the moment
        currentLevel = new Level(1, UnityEngine.Random.Range(3,5),UnityEngine.Random.Range(1,4)*3);
        CreateNewLevel(currentLevel);
    }

    private void CreateBrickObjects(Level level)
    {
        
        currentLevelBrickBehaviours = new List<BrickBehaviour>(currentLevelTotalBricks.Count);

        foreach (var brick in currentLevelTotalBricks)
        {
            BrickBehaviour brickBehaviour = GameObject.Instantiate(brickPrefab, Vector3.zero, Quaternion.identity).GetComponent<BrickBehaviour>();
            brickBehaviour.BrickModel = brick;
            brickBehaviour.OnBrickDestroyed  += OnBrickDestroyed;
            currentLevelBrickBehaviours.Add(brickBehaviour);
        }

    }
    
    private void PositionBricks()
    {
        CalculateStartBrickSpawnPosition();
        for (int i = 0; i < currentLevel.Rows; i++)
        {
            for (int j = 0; j < currentLevel.Columns; j++)
            {
                nextSpwanPosition = startSpawnPosition + new Vector2(
                    j * (currentLevelBrickBehaviours[i + (j + ((currentLevel.Columns-1) * i))].transform.localScale.x + brickSpacing),
                    -i * (currentLevelBrickBehaviours[i + (j + ((currentLevel.Columns - 1) * i))].transform.localScale.y*0.5f + brickSpacing)
                    );

                currentLevelBrickBehaviours[i + (j + ((currentLevel.Columns - 1) * i))].transform.position = nextSpwanPosition;
            }
        }
    }

    private void OnBrickDestroyed(BrickBehaviour destroyedBrick)
    {
        bricksDestroyed--;
        if(bricksDestroyed < 1)
        {
            DestroyCurrentLevel();
            //Create new level
            currentLevel = new Level(1, UnityEngine.Random.Range(3,5),UnityEngine.Random.Range(1,4)*3);
            CreateNewLevel(currentLevel);
        }
    }

    private void CreateNewLevel(Level level)
    {
        bricksDestroyed = level.BricksCount;
        //Create brick models
        currentLevelTotalBricks = BrickFactory.CreateBricks(level);
        //Create brick objects
        CreateBrickObjects(level);
        //Position bricks in row and column
        PositionBricks();
    }

    private void DestroyCurrentLevel()
    {
        if(currentLevel != null)
        {
            //Destroy brick models
            for (int i =0 ; i < currentLevelTotalBricks.Count ; i++)
            {
                currentLevelTotalBricks[i] = null;
            }

            //Destroy brick objects
            foreach (var brickGO in currentLevelBrickBehaviours)
            {
                brickGO.OnBrickDestroyed -= OnBrickDestroyed;
                Destroy(brickGO.gameObject);
            }

            currentLevelBrickBehaviours.Clear();
            currentLevelTotalBricks.Clear();
            currentLevel = null;
        }
        else
        {
            Debug.LogError("currentLevel is null in brickSpawner");
        }
    }

    private void ResetCurrentLevel()
    {
        if(currentLevel != null)
        {
            //Renable destroyed bricks
            foreach (var brick in currentLevelBrickBehaviours)
            {
                brick.gameObject.SetActive(true);   
            }
            bricksDestroyed = currentLevel.BricksCount;
        }
        else
        {
            Debug.LogError("currentLevel is null in brickSpawner");
        }
    }

    private void CalculateStartBrickSpawnPosition()
    {
        startSpawnPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width/2, Screen.height));
        startSpawnPosition.y -= 1.0f;
        startSpawnPosition.x -= (brickPrefabSprtRndr.bounds.max.x * currentLevel.Columns);
        startSpawnPosition.x += currentLevel.Columns * brickSpacing;
        nextSpwanPosition = Vector2.zero;
    }
}
