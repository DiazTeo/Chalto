using DG.Tweening;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    protected Action DoAction = default;
    protected Action DoActionFixed = default;
    [SerializeField]
    protected Cat player;

    private void Awake()
    {
        DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(200, 10);
        SetModeMenu();
    }

    private void Update()
    {
        DoAction();
        DoActionFixed();

    }

    private void FixedUpdate()
    {
    }

    public void SetModeMenu()
    {
        UIManager.OnGame += StartGame;
        DoAction = DoMenu;
        DoActionFixed = DoMenuFixed;
    }

    protected void DoMenu()
    {

    }

    protected void DoMenuFixed()
    {

    }

    public void StartGame()
    {
        UIManager.OnGame -= StartGame;

        player.OnFlip += Player_OnFlip;
        player.OnGameOver += Player_OnGameOver;
        player.OnWin += Player_OnWin;

        player.transform.DOJump(player.transform.position + Vector3.back * 2, 1, 1, 0.5f).OnComplete(() =>
        {
            DoAction = DoGame;
            DoActionFixed = DoFixedGame;
        }).SetEase(Ease.Linear);
    }

    private void Player_OnWin()
    {
        Debug.Log("Win");
        EndGame();
    }

    private void Player_OnGameOver()
    {
        Debug.Log("Lose");
        EndGame();
    }

    private void Player_OnFlip()
    {

    }

    protected void DoGame()
    {
        player.CheckInput();
    }

    protected void DoFixedGame()
    {
        player.Fall();
    }

    protected void EndGame()
    {
        player.OnFlip -= Player_OnFlip;
        player.OnGameOver -= Player_OnGameOver;
        player.OnWin -= Player_OnWin;
    }

}
