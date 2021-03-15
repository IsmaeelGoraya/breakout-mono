using UnityEngine;
using Factories;
using DataModels;
using System.Collections.Generic;

public class BrickSpawner : MonoBehaviour
{
    public GameObject brickPrefab;

    private Level currentLevel;
    private List<Brick> currentLevelTotalBricks;
    private List<BrickBehaviour> currentLevelBrickBehaviours;
    private float brickSpacing = 0.1f;
    private Vector2 startSpawnPosition;
    public int rows;
    public int coloumns;

    private void Awake()
    {
        brickSpacing = 0.1f;
        Vector2 screenSize = new Vector2(0, Screen.height);
        startSpawnPosition = Camera.main.ScreenToWorldPoint(screenSize);
        startSpawnPosition.x += brickPrefab.transform.localScale.x / 2;
        startSpawnPosition.y -= brickPrefab.transform.localScale.y;
        //TODO: Will refactor this later to text/json based
        //Create bricks with only multiples of 3 at the moment
        currentLevel = new Level(1, coloumns, rows);
    }

    private void Start()
    {
        CreateBricks();
        PositionBricks();
    }

    private void CreateBricks()
    {
        currentLevelTotalBricks = BrickFactory.CreateBricks(currentLevel);
        currentLevelBrickBehaviours = new List<BrickBehaviour>(currentLevelTotalBricks.Count);

        foreach (var brick in currentLevelTotalBricks)
        {
            BrickBehaviour brickBehaviour = GameObject.Instantiate(brickPrefab, Vector3.zero, Quaternion.identity).GetComponent<BrickBehaviour>();
            brickBehaviour.BrickModel = brick;
            currentLevelBrickBehaviours.Add(brickBehaviour);
        }

    }
    Vector2 spwanPosition = Vector2.zero;
    private void PositionBricks()
    {
        for (int i = 0; i < currentLevel.Rows; i++)
        {
            for (int j = 0; j < currentLevel.Coloumns; j++)
            {
                spwanPosition = startSpawnPosition + new Vector2(
                    j * (currentLevelBrickBehaviours[i + (j + ((coloumns-1) * i))].transform.localScale.x + brickSpacing),
                    -i * (currentLevelBrickBehaviours[i + (j + ((coloumns - 1) * i))].transform.localScale.y + brickSpacing)
                    );

                currentLevelBrickBehaviours[i + (j + ((coloumns - 1) * i))].transform.position = spwanPosition;
            }
        }
    }
}
