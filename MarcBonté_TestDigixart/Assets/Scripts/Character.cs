using UnityEngine;

public class Character : MonoBehaviour
{
    private const float NORMAL_MULTIPLIER = 2.5f;

    [Range(0f, 20f)]
    public float speed;
    [Range(0f, 20f)]
    public float lerpSpeed = 10f;
    public Transform graphics;

    [Range(0f, 0.5f)]
    public float timer = 0.5f;
    private float time;
    private float angle;
    private bool angleAdjustement;

    protected void Start()
    {
        angle = transform.eulerAngles.z;
    }

    protected void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Vector3 movement = new Vector3(input.x, 0f, input.y);
        transform.Translate(movement * speed * Time.deltaTime);

        if (!angleAdjustement)
        {
            graphics.transform.position = Vector3.Lerp(graphics.transform.position, transform.position, lerpSpeed * Time.deltaTime);
        }
        else
        {
            time += Time.deltaTime;
            if (time > timer)
            {
                angleAdjustement = false;
            }
        }

        if (movement != Vector3.zero)
        {
            Ray ray = new Ray(transform.position, -transform.up);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.white);

                Vector3 normal = hit.normal;
                Debug.DrawRay(hit.point, normal, Color.red);

                transform.position = hit.point + (normal * NORMAL_MULTIPLIER);

                float newAngle = hit.collider.transform.eulerAngles.z;
                if (angle != newAngle)
                {
                    transform.eulerAngles = new Vector3(0f, 0f, newAngle);
                    angle = newAngle;
                    angleAdjustement = true;
                    time = 0f;
                }
            }
        }
    }
}