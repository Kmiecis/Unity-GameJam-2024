using UnityEngine;
using System.Collections;

public class SmokeControl : MonoBehaviour
{
    public ParticleSystem smoke;  // Przypisz system cz�steczkowy dymu
    public Animator windowAnimator;  // Przypisz animator okna
    public string windowOpenStateName = "WindowOpen";  // Nazwa stanu otwartego okna w Animatorze
    public float moveDistance = 5f;  // Dystans, o kt�ry dym zostanie przesuni�ty
    private bool windowWasOpened = false;  // Flaga sprawdzaj�ca, czy okno by�o otwarte

    void Update()
    {
        AnimatorStateInfo stateInfo = windowAnimator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(windowOpenStateName) && !stateInfo.loop)
        {
            if (!windowWasOpened)
            {
                windowWasOpened = true;
                StartCoroutine(MoveAndFadeSmoke());
            }
        }
        else if (!stateInfo.IsName(windowOpenStateName))
        {
            windowWasOpened = false;  // Reset flagi, gdy animacja okna si� zako�czy
        }
    }

    IEnumerator MoveAndFadeSmoke()
    {
        // Przesu� dym
        Vector3 startPosition = smoke.transform.position;
        Vector3 endPosition = startPosition + new Vector3(moveDistance, 0, 0);
        float moveTime = 1.0f;
        float elapsedTime = 0;

        while (elapsedTime < moveTime)
        {
            smoke.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        smoke.transform.position = endPosition;

        // Zanikanie dymu
        var emission = smoke.emission;
        emission.enabled = false;  // Wy��cz emisj� cz�steczek
        yield return new WaitForSeconds(2);  // Czekaj, a� cz�steczki znikn�

        smoke.gameObject.SetActive(false);  // Wy��cz obiekt dymu
    }
}
