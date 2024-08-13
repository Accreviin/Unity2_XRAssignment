using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void ButtonPressed()
    {
        string OpenSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(OpenSceneName);
    }
}
