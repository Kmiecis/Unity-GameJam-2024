using UnityEngine;

public class Water : MonoBehaviour
{
    public GameObject electricEffectPrefab;
    public Transform targetSocket;

    private bool isElectrified = false;

    public void Electrify()
    {
        if (!isElectrified)
        {
            isElectrified = true;
            if (electricEffectPrefab != null)
            {
                Instantiate(electricEffectPrefab, targetSocket, false);
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
