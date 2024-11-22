using UnityEngine;

public class SmokeControl : MonoBehaviour
{
    public ParticleSystem smoke;  // Przypisz system cz�steczkowy dymu
    public Animator windowAnimator;  // Przypisz animator okna
    public string windowOpenStateName = "WindowOpen";  // Nazwa stanu otwartego okna w Animatorze

    void Update()
    {
        AnimatorStateInfo stateInfo = windowAnimator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(windowOpenStateName) && stateInfo.normalizedTime < 1.0f)
        {
            if (!smoke.isPlaying)
                smoke.Play();  // Odtw�rz dym, gdy okno si� otwiera
        }
        else
        {
            if (smoke.isPlaying)
                smoke.Stop();  // Zatrzymaj dym, gdy okno jest zamkni�te
        }
    }
}
