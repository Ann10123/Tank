using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform m_Destination; 
    private bool m_IsActive = true;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb != null && m_IsActive && m_Destination != null)
        {
            m_Destination.GetComponent<Portal>().m_IsActive = false;

            other.transform.position = m_Destination.position + Vector3.up;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_IsActive = true;
    }
}