using UnityEngine;

public class Toaster : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Water water = other.GetComponent<Water>();
        if (water != null)
        {
            water.Electrify();
        }
    }
}
