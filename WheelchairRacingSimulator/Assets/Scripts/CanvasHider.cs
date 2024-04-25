using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasHider : MonoBehaviour
{
    public static CanvasHider instance;
    private GameObject canvas;

    void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        canvas = gameObject; // Assign the canvas GameObject to the variable
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            if (canvas != null)
            {
                canvas.SetActive(true); // Ensure canvas is not null before accessing it
            }
            else
            {
                Debug.LogWarning("Canvas is null when trying to activate it.");
            }
        }
        else
        {
            if (canvas != null)
            {
                canvas.SetActive(false); // Ensure canvas is not null before accessing it
            }
            else
            {
                Debug.LogWarning("Canvas is null when trying to deactivate it.");
            }
        }
    }
}
