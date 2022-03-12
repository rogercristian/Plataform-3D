using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeBehavior : MonoBehaviour
{
    public int size;
    public float offset;
    public float mass;
    public int gripNodeAdjust;

    private Transform m_Node;
    private PlayerRopeSwingging player;
    private Vector3 gripOffset;

    Transform gripNode;
    Transform playerNode;
    // Start is called before the first frame update
    void Start()
    {
        m_Node = transform.GetChild(0);
        Vector3 position = transform.position;

        Rigidbody lastRb = m_Node.GetComponent<Rigidbody>();

        for (int i = 0; i < size; i++)
        {
            position.y -= offset;
            GameObject newNode = Instantiate(m_Node.gameObject, position, transform.rotation);
            newNode.transform.SetParent(transform);

            newNode.GetComponent<FixedJoint>().connectedBody = lastRb;
            lastRb = newNode.GetComponent<Rigidbody>();            
        }

        lastRb.GetComponent<FixedJoint>().massScale = mass;
        gripNode = transform.GetChild(transform.childCount - gripNodeAdjust);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            player.transform.position = playerNode.position + gripOffset;
        }
        else
        {
            gripNode.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public void AttachPlayer(PlayerRopeSwingging player, GameObject node)
    {        
        this.player = player;
        player.transform.SetParent(gripNode);
        player.GetComponentInChildren<Collider>().enabled = false;
        playerNode = node.transform;
    }

    public void DetachPlayer()
    {
        player.GetComponentInChildren<Collider>().enabled = true;
        player.transform.SetParent(null);
        player = null;

    }

    public void ApplySwing(Vector3 force)
    {
        if(gripNode != null)
        {
            gripNode.GetComponent<Rigidbody>().AddForce(force);
        }
    }
}
