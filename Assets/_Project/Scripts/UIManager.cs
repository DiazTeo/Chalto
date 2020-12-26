using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public delegate void UIManagerEventHandler();
    public static event UIManagerEventHandler OnGame;

    [SerializeField]
    protected Menu menu;

    private void Awake()
    {
    }

    public void GoToMenu()
    {
        menu.OnPlay += GoToGame;
        menu.OnCredit += GoToCredit;
        menu.gameObject.SetActive(true);
        menu.Show();
    }

    public void GoToGame()
    {
        menu.OnPlay -= GoToGame;
        menu.OnCredit -= GoToCredit;
        menu.Hide();
        OnGame.Invoke();
    }

    public void GoToEndGame()
    {

    }

    public void GoToCredit()
    {
        menu.OnPlay -= GoToGame;
        menu.OnCredit -= GoToCredit;
        menu.Hide();
    }

}
