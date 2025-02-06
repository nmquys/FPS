using System;
using System.Collections;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    private const float maxTimer = 8f;
    private float timer = maxTimer;

    private CanvasGroup canvasGroup = null;
    protected CanvasGroup CanvasGroup
    {
        get
        {
            if(canvasGroup == null)
            {
                canvasGroup =GetComponent<CanvasGroup>();
                if(canvasGroup == null)
                {
                    canvasGroup = gameObject.AddComponent<CanvasGroup>();
                }
            }
            return canvasGroup;
        }
    }

    private RectTransform rect = null;
    protected RectTransform Rect
    {
        get
        {
            if(rect == null)
            {
                rect = GetComponent<RectTransform>();
                if(rect == null)
                {
                    rect = gameObject.AddComponent<RectTransform>();
                }
            }
            return rect;
        }
    }
    public Transform Target { get; protected set; } = null;
    private Transform player = null;

    private IEnumerator IE_Countdown = null;
    private Action unRegister = null;

    private Quaternion tRotation = Quaternion.identity;
    private Vector3 tPosition = Vector3.zero;

    public void Register(Transform target, Transform player, Action unRegister)
    {
        this.Target = target;
        this.player = player;
        this.unRegister = unRegister;

        StartCoroutine(RotateToTheTarger());
        StartTimer();
    }
    
public void Restart()
{
    timer = maxTimer;
    StartTimer();
}

    private void StartTimer()
    {
        if(IE_Countdown != null)
        {
            StopCoroutine(IE_Countdown);
        }
            IE_Countdown = Countdown();
            StartCoroutine(IE_Countdown);
    }

    IEnumerator RotateToTheTarger()
    {
        while(enabled)
        {
            if(Target)
            {
                tPosition = Target.position;
                tRotation = Target.rotation;
            }
            Vector3 direction = player.position - tPosition;

            tRotation = Quaternion.LookRotation(direction);
            tRotation.z = -tRotation.y ;
            tRotation.x = 0;
            tRotation.y = 0;

            Vector3 northDirection = new Vector3(0, 0, 180 + player.eulerAngles.y);
            Rect.localRotation = tRotation * Quaternion.Euler(northDirection);

            yield return null;
        }
    } 

    private IEnumerator Countdown()
    {
        while(CanvasGroup.alpha < 1.0f)
        {
            CanvasGroup.alpha += 4 * Time.deltaTime;
            yield return null;
        }
        while(timer > 0)
        {
            timer--;
            yield return new WaitForSeconds(1);
        }
        while(CanvasGroup.alpha > 0.0f)
        {
            CanvasGroup.alpha -= 2 * Time.deltaTime;
            yield return null;
        }
        unRegister();
        Destroy(gameObject);
    }
}
