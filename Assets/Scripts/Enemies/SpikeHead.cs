using UnityEngine;

public class SpikeHead : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask playerLayer;
    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private float attackTimer = Mathf.Infinity;

    private void Start()
    {
        Stop();
    }
    private void CalculateDirections()
    {
        directions[0] = transform.position - transform.right * range;//Left direction
        directions[1] = transform.position + transform.right * range;//Right direction
        directions[2] = transform.position + transform.up * range;   //Up direction
        directions[3] = transform.position - transform.up * range;   //Down direction
    }
    private void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= 1) CheckForPlayer();
        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * speed);
    }
    private void CheckForPlayer()
    {
        print("checking");
        CalculateDirections();
        for (int i = 0; i < directions.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], Mathf.Infinity, playerLayer);

            if (hit.collider != null)
            {
                print(hit.collider.name);
                destination = directions[i];
                attackTimer = 0;
            }
        }
    }
    private void Stop()
    {
        destination = transform.position;
        attackTimer = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Stop();
        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<Health>().TakeDamage(1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            destination = collision.transform.position;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < directions.Length; i++)
            Gizmos.DrawLine(transform.position, directions[i]);
    }
}