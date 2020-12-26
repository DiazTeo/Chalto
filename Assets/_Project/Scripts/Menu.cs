using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public delegate void MenuEventHandler();

    public event MenuEventHandler OnPlay;
    public event MenuEventHandler OnSound;
    public event MenuEventHandler OnCredit;

    [SerializeField]
    protected Button playBtn;
    [SerializeField]
    protected Button soundBtn;
    [SerializeField]
    protected Button creditBtn;

    public void Show()
    {
        GetComponent<CanvasGroup>().DOFade(1f, 1f);
    }

    public void Hide()
    {
        GetComponent<CanvasGroup>().DOFade(0f, 1f).OnComplete(()=> { gameObject.SetActive(false);});
    }

    public void Awake()
    {
        playBtn.onClick.AddListener(Play);
        soundBtn.onClick.AddListener(Sound);
        creditBtn.onClick.AddListener(Credit);

    }

    private void Credit()
    {
        OnCredit.Invoke();
    }

    private void Sound()
    {
        OnSound.Invoke();
    }

    private void Play()
    {
        OnPlay.Invoke();
    }
}
