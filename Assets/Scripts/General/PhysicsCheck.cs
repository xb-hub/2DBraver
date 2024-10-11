using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D capsuleCollider;
    public bool isGround;
    public float radius;
    public LayerMask groundLayer;
    public Vector3 bottomOffest;
    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }

    public void Check()
    {
        isGround = Physics2D.OverlapCircle(transform.position + bottomOffest, radius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + bottomOffest, radius);
    }
}
