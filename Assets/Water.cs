using UnityEngine;

public class Water : MonoBehaviour
{
    public GameObject electricEffectPrefab;
    private bool isElectrified = false;

    public void Electrify()
    {
        if (!isElectrified)
        {
            isElectrified = true;
            if (electricEffectPrefab != null)
            {
                Instantiate(electricEffectPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isElectrified && other.GetComponent<Throwable>() != null)
        {
            Destroy(other.gameObject);
        }
    }
}
