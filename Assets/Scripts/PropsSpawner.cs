using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsSpawner : MonoBehaviour
{
    public GameObject prefabs;

    public int amount;    

    public List<Vector3> positions;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < positions.Count; i++)
        {
            int sectionAmount = amount / (positions.Count - 1);
            for(int j = 0; j < sectionAmount; j++)
            {
                Vector3 initPosition = positions[i];
                Vector3 nextPosition;
                if(i < positions.Count -1)
                {
                    nextPosition = positions[i + 1];
                    Vector3 position = transform.position;
                    float percent = (float)j / (float)sectionAmount;

                    position = Vector3.Lerp(position + initPosition, position + nextPosition, percent);

                    GameObject prop = Instantiate(prefabs, position, transform.rotation);
                    prop.transform.SetParent(transform);
                }                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        foreach(Vector3 pos in positions)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position + pos, 1);
        }
        // Draw a yellow sphere at the transform's position
       
    }
}
