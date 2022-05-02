using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackzone : MonoBehaviour
{
    public float AttackSpeed;
    Rigidbody2D myBody;
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D> ();
        myBody.AddForce(new Vector2(1, 0) * AttackSpeed, ForceMode2D.Impulse);
    }
    void Start()
    {
    }
    void Update()
    {
        
    }
}
