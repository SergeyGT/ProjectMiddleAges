using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [Header("Main Menu Track")]
    [SerializeField] private AudioClip _audioClip;

    public float rotationSpeed = 10f; 

    private void Start()
    {
        SoundManager.Instance.PlayMusic(_audioClip);
    }


    void Update()
    {
        float currentRotation = RenderSettings.skybox.GetFloat("_Rotation");

        currentRotation += rotationSpeed * Time.deltaTime;

        RenderSettings.skybox.SetFloat("_Rotation", currentRotation);
    }
    public void SceneChange(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1.0f;
    }
}
