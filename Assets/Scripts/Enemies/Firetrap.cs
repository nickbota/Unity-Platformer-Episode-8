using System.Collections;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    private SpriteRenderer rend;
    private Animator anim;
    private bool activated;
    private bool triggered;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        anim.SetBool("activated", activated);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!activated && !triggered)
                StartCoroutine(ActivateTrap());
            else
                collision.GetComponent<Health>().TakeDamage(1);
        }
    }
    private IEnumerator ActivateTrap()
    {
        rend.color = Color.red;
        triggered = true;
        yield return new WaitForSeconds(1);
        rend.color = Color.white;
        activated = true;
        yield return new WaitForSeconds(2);
        activated = false;
        triggered = false;
    }
}