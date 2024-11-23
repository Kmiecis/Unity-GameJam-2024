using Game;
using UnityEngine;

public class Water : MonoBehaviour
{
    public GameObject electricEffectPrefab;
    public SoundShot electricShot;
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
                electricShot.ShotSound();
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
