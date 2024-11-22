using UnityEngine;
using System.Collections;

public class SmokeControl : MonoBehaviour
{
    public ParticleSystem smoke;  // Przypisz system cz¹steczkowy dymu
    public Animator windowAnimator;  // Przypisz animator okna
    public string windowOpenStateName = "WindowOpen";  // Nazwa stanu otwartego okna w Animatorze
    public float moveDistance = 5f;  // Dystans, o który dym zostanie przesuniêty
    private bool windowWasOpened = false;  // Flaga sprawdzaj¹ca, czy okno by³o otwarte

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
            windowWasOpened = false;  // Reset flagi, gdy animacja okna siê zakoñczy
        }
    }

    IEnumerator MoveAndFadeSmoke()
    {
        // Przesuñ dym
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
        emission.enabled = false;  // Wy³¹cz emisjê cz¹steczek
        yield return new WaitForSeconds(2);  // Czekaj, a¿ cz¹steczki znikn¹

        smoke.gameObject.SetActive(false);  // Wy³¹cz obiekt dymu
    }
}
