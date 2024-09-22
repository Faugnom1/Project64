using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class StalkerAttack : MonoBehaviour
{
    [SerializeField] private float _immobilizeTime;
    [SerializeField] private float _releaseToChaseDelay;

    private StalkerNav _nav;
    private BoxCollider2D _boxCollider;

    private void Start()
    {
        _nav = GetComponent<StalkerNav>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            IPlayerAttackable player = collision.collider.GetComponent<IPlayerAttackable>();
            if (player != null)
            {
                AttackPlayer(player);
            }
        }
    }

    private void AttackPlayer(IPlayerAttackable player)
    {
        player.Immobilize();
        player.Bleed();
        _nav.StopChase();

        StartCoroutine(ReleasePlayer(player));
    }

    private IEnumerator ReleasePlayer(IPlayerAttackable player)
    {
        yield return new WaitForSeconds(_immobilizeTime);
        player.Release(transform.position, OnReleaseComplete);
    }

    private void OnReleaseComplete()
    {
        StartCoroutine(ResumeChase());
    }

    private IEnumerator ResumeChase()
    {
        yield return new WaitForSeconds(_releaseToChaseDelay);
        _nav.ChasePlayer();
    }
}
