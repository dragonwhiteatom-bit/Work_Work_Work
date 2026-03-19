using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gravi : MonoBehaviour
{
    public static List<Gravi> otherObject;
    private Rigidbody rb;
    const float G = 0.006673f;

    [SerializeField] bool planet = false;
    [SerializeField] int orbitSpeed = 1000;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (otherObject == null)
        {
            otherObject = new List<Gravi>(); 
        }
        otherObject.Add(this);

        if (!planet)
        {
            rb.AddForce(Vector3.left * orbitSpeed);
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Gravi obj in otherObject)
        {
            if (obj != this)
            {
                AttractionForce(obj);
            }
        }
    }

    void AttractionForce(Gravi other)
    {
        Rigidbody otherRb = other.rb;
        Vector3 direction = rb.position - otherRb.position;
        float distance = direction.magnitude;
        if (distance == 0f) return;
        float forceMagnitude = G * (rb.mass * otherRb.mass / Mathf.Pow(distance, 2));
        Vector3 gravitationalForce = forceMagnitude * direction.normalized;
        otherRb.AddForce(gravitationalForce);
    }
}
