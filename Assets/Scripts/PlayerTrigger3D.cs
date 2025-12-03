using UnityEngine;

public class PlayerTrigger3D : MonoBehaviour
{
    public PlayerBoost boostScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boost"))
        {
            boostScript.ActivateBoost();
            Destroy(other.gameObject);
            Debug.Log("Boost collected!");
        }
    }
}
