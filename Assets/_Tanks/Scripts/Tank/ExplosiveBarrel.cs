using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public float m_ExplosionRadius = 5f;
    public float m_ExplosionForce = 10f; 
    public float m_Damage = 50f;
    public GameObject m_ExplosionPrefab;

    private bool m_Exploded = false;

    public AudioClip m_ExplosionAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (m_Exploded) return;

        bool isShell = other.gameObject.name.Contains("Shell");
        bool isTank = other.GetComponent<Rigidbody>() != null;

        if (isShell || isTank)
        {
            Explode();
        }
    }

    private void Explode()
    {
        m_Exploded = true;

        AudioSource.PlayClipAtPoint(m_ExplosionAudio, transform.position);

        if (m_ExplosionPrefab != null)
        {
            GameObject explosionInstance = Instantiate(m_ExplosionPrefab, transform.position + Vector3.up, Quaternion.identity);
            Destroy(explosionInstance, 3f);
        }

        ApplyPhysicsAndDamage();

        Destroy(gameObject);
    }

    private void ApplyPhysicsAndDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius);

        foreach (Collider hit in colliders)
        {
            hit.SendMessage("TakeDamage", m_Damage, SendMessageOptions.DontRequireReceiver);

            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);
            }
        }
    }
}