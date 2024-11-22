using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{

    [SerializeField] GameObject play;
    [SerializeField] GameObject controls;
    [SerializeField] GameObject quit;
    [SerializeField] GameObject controlText;
    [SerializeField] GameObject BackButton;
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnControls()
    {
        play.SetActive(false);
        controls.SetActive(false);
        quit.SetActive(false);
        BackButton.SetActive(true);
        controlText.SetActive(true);

    }

    public void Back()
    {
        play.SetActive(true);
        controls.SetActive(true);
        quit.SetActive(true);
        controlText.SetActive(false);
        BackButton.SetActive(false);

    }
}
