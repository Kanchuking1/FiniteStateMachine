using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

enum drunkenness
{
    Sober,
    Drunk,
    TooDrunk,
    BlackedOut
}

public class StoryManager : MonoBehaviour
{
    public TextMeshProUGUI currentState;
    public TextMeshProUGUI Message;

    public GameObject endGamePanel;
    public GameObject choicesPanel;

    private drunkenness drunkenness;

    string[] stateTextList = { "in your senses", "Drunk", "Too Drunk", "Blacked Out" };

    public void Start()
    {
        drunkenness = drunkenness.Sober;
        currentState.text = "You are " + stateTextList[(int)drunkenness];
    }

    public void Party()
    {
        StartCoroutine(GoToParty());
    }

    IEnumerator GoToParty()
    {
        ToggleChoices(false);
        SetMessage("You go to party with him and drink");
        drunkenness++;
        currentState.text =  "You are " + stateTextList[(int)drunkenness];

        if (drunkenness == drunkenness.BlackedOut)
        {
            StartCoroutine(EndGame());
        }
        yield return new WaitForSeconds(2);
        SetMessage("You come back and he insists you go to another party");
        ToggleChoices(true);
    }

    public void Punch()
    {
        StartCoroutine(PunchHim());
    }

    IEnumerator PunchHim()
    {
        ToggleChoices(false);
        if (drunkenness == drunkenness.Sober)
        {
            SetMessage("You punch him in your senses, you hold back, friend is still persistent");
        }
        if (drunkenness == drunkenness.Drunk)
        {
            SetMessage("You win the fight with alcohol in your system, Friend is Dead");
            StartCoroutine(EndGame());
        }
        if (drunkenness == drunkenness.TooDrunk)
        {
            SetMessage("Too drunk to fight, your friend kills you");
            StartCoroutine(EndGame());
        }
        yield return new WaitForSeconds(2);
        ToggleChoices(true);
    }

    public void Ignore()
    {
        StartCoroutine(IgnoreHim());
    }

    IEnumerator IgnoreHim()
    {
        ToggleChoices(false);
        SetMessage("You ignored him, he left");
        StartCoroutine(EndGame());
        yield return new WaitForSeconds(2);
        ToggleChoices(false);
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2);
        endGamePanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SetMessage(string msg) 
    {
        Message.text = msg;
    }

    void ToggleChoices (bool value)
    {
        choicesPanel.SetActive(value);
    }
}
