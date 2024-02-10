using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Objective objective;

    private bool isGameOver;

    private void Awake()
    {
        objective = FindObjectOfType<Objective>();
        if (objective == null)
        {
            Debug.LogError("Objective not found in the scene.");
        }
    }

    public void EndLevel()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            LoadNextLevel();
            Debug.Log("Level Complete");
        }
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Update()
    {
        if (objective != null)
        {
            if (objective.IsComplete)
            {
                EndLevel();
            }
        }
        else
        {
            Debug.LogError("Objective is null. Make sure the Objective script is attached to the appropriate object in the scene.");
        }
    }

    
}
