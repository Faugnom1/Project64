using Newtonsoft.Json.Bson;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class StalkerAttack : MonoBehaviour
{
    [SerializeField] private float _immobilizeTime;
    [SerializeField] private float _releaseToChaseDelay;

    private StalkerNav _nav;
    private BoxCollider2D _boxCollider;

    private bool _aggressive;

    private void Start()
    {
        _aggressive = true;
        _nav = GetComponent<StalkerNav>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && _aggressive)
        {
            IPlayerAttackable player = collision.collider.GetComponent<IPlayerAttackable>();
            if (player != null)
            {
                AttackPlayer(player);
            }
        }
    }

    public void DisableAggression()
    {
        _aggressive = false;
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
