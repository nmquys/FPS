using UnityEngine;

public class TestIndicatorRegister : MonoBehaviour
{
    [SerializeField] float destroyTimer = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("Register", Random.Range(0, 8));
    }

    // Update is called once per frame
    void Register()
    {
        if(DI_System.CheckIfObjectInsight(this.transform))
        {
            DI_System.CreateIndicator(this.transform);
        }
        Destroy(this.gameObject, destroyTimer);
    }
}
