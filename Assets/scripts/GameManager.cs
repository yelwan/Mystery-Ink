using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Level[] levels;
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] GameObject player;
    private int currentLevelIndex = 0;


    public void NextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex == 4) SceneManager.LoadScene(2);
        if (currentLevelIndex >= levels.Length) return;
        currentLevelIndex = index;
        levels[currentLevelIndex].StartLevel(player);
    }
}
