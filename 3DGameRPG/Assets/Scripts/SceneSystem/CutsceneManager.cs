using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public void SkipCutscene()
    {
        SceneManager.LoadScene("Chap0.5");
    }
}
