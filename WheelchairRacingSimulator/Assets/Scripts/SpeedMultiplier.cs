using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpeedMultiplier : MonoBehaviour
{
    public Slider slider;
    public float speedMultiplier; // Static variable to retain slider value between scenes
    public static SpeedMultiplier instance;


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
        // Assign the stored slider value to the slider
        if (slider != null)
        {
            slider.value = speedMultiplier;
        }

        // Add a listener to the slider
        slider.onValueChanged.AddListener((v) =>
        {
            speedMultiplier = v;
        });
    }

   
    void Update()
    {
        if (slider == null && SceneManager.GetActiveScene().name == "MainMenu")
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag("Canvas");
            if (gameObject != null)
            {
                slider = gameObject.GetComponentInChildren<Slider>();
            }
        }
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
        // Reset slider value if returning to main menu
        if (scene.name == "MainMenu" && slider != null)
        {
            slider.value = speedMultiplier;
        }
    }

}
