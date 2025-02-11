using System;
using System.Collections.Generic;
using UnityEngine;

public class DI_System : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private DamageIndicator indicatorPrefab = null;
    [SerializeField] private RectTransform holder = null;
    [SerializeField] private new Camera camera = null;
    [SerializeField] private Transform player = null;

    public bool isAttackingPlayer = false;

    private Dictionary<Transform, DamageIndicator> Indicators = new Dictionary<Transform, DamageIndicator>();

    #region Delegates
    public static Action<Transform> CreateIndicator = delegate {};
    public static Func<Transform, bool> CheckIfObjectInsight = null;
    #endregion

    

    private void OnEnable()
    {
        //CreateIndicator += Create;
        PlayerHealth.OnTakeDamge += Create;
        CheckIfObjectInsight += InSight;
    }

    private void OnDisable()
    {
        //CreateIndicator -= Create;
        PlayerHealth.OnTakeDamge += Create;
        CheckIfObjectInsight -= InSight;
    
    }

    public void Create(Transform target)
    {
        if(Indicators.ContainsKey(target))
        {
            Indicators[target].Restart();
            return;
        }
        DamageIndicator newIndicator = Instantiate(indicatorPrefab, holder);
        newIndicator.Register(target, player, new Action(() => { Indicators.Remove(target); }));

        Indicators.Add(target, newIndicator);
    }

    bool InSight(Transform t)
    {
        Vector3 screenPoint = camera.WorldToViewportPoint(t.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}
