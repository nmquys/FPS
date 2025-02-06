using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TakeDamage : MonoBehaviour
{

    public float intensity = 0;

    PostProcessVolume PPVolume;
    Vignette vignette;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PPVolume = GetComponent<PostProcessVolume>();
        PPVolume.profile.TryGetSettings<Vignette>(out vignette);

        if(!vignette)
        {
            print("error, vignette empty");
        }
        else
        {
            vignette.enabled.Override(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(TakeDamageEffect());
        }
    }

    private IEnumerator TakeDamageEffect()
    {
        intensity = 0.4f;

        vignette.enabled.Override(true);
        vignette.intensity.Override(0.4f);

        yield return new WaitForSeconds(0.4f);

        while(intensity > 0)
        {
            intensity -= 0.01f;

            if(intensity < 0)
            {
                intensity = 0;
            }

            vignette.intensity.Override(intensity);
            yield return new WaitForSeconds(0.1f);
        }

        vignette.enabled.Override(false);
        yield break;
    }

    public void TakeDamageScreen()
    {
        TakeDamageEffect();
    }
}
