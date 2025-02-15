using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatingSprite : MonoBehaviour
{
    public GameObject poopSprite;
    BoxCollider boxCol;
    MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Instantiate(poopSprite, new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z), poopSprite.transform.rotation);
            Destroy(gameObject);
        }
    }
}