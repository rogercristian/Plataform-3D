using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructbleBeahavior : MonoBehaviour
{
    public int maxLife;
    public float timeTodDestroy;

    private int m_Life;

    private Rigidbody m_Rigidbody;
    // Start is called before the first frame update
    protected void Start()
    {
        m_Life = maxLife;
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ApplyDamage(int damage)
    {
        m_Life -= damage;
        if (m_Life <= 0)
        {
            Destroy();
        }

        OnDestroy();
    }

    public virtual void OnDestroy()
    {

    }
    public void Destroy()
    {
        m_Rigidbody.isKinematic = true;
        m_Rigidbody.GetComponent<Collider>().enabled = false;
        Destroy(gameObject,timeTodDestroy);
    }
}
