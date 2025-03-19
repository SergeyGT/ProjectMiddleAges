using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {

#if UNITY_EDITOR
        // Остановить воспроизведение в редакторе
        UnityEditor.EditorApplication.isPlaying = false;

#else
            // Выход из приложения
            Application.Quit();

#endif
    }
}
