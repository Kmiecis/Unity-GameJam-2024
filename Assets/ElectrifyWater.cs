using UnityEngine;

public class ElectrifyWater : MonoBehaviour
{
    public GameObject electricEffectPrefab;  // Prefabrykat efektu pr¹du
    private bool isElectrified = false;  // Stan naelektryzowania wody

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Toaster") && !isElectrified)
        {
            Electrify();
        }
        else if (isElectrified && other.gameObject.CompareTag("Throwable"))
        {
            Destroy(other.gameObject);  // Zniszcz rzucalny obiekt
        }
    }

    void Electrify()
    {
        isElectrified = true;  // Ustaw stan wody na naelektryzowany
        if (electricEffectPrefab != null)
        {
            GameObject effectInstance = Instantiate(electricEffectPrefab, transform.position, Quaternion.identity);
            ParticleSystem particles = effectInstance.GetComponent<ParticleSystem>();
            particles.Play();
        }
    }
}
