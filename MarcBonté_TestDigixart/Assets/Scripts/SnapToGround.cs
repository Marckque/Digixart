using UnityEngine;

public class SnapToGround : MonoBehaviour
{
    [SerializeField]
    private bool snapToGround;
    private Collider coll;

    protected void OnValidate()
    {
        if (snapToGround)
        {
            Ray ray = new Ray(transform.position, -transform.up);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                coll = GetComponent<Collider>();

                transform.position = new Vector3(transform.position.x, hit.point.y + coll.bounds.extents.y, transform.position.z); ;
                snapToGround = false;
            }
        }
    }
}