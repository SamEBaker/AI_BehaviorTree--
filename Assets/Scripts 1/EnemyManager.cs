using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int _healthpoints;
    private Animator _animator;
    public bool isDead;

    private void Awake()
    {
        //_healthpoints = 30;
        _animator = transform.GetComponent<Animator>();
    }

    public bool TakeHit()
    {
        _healthpoints -= 10;
        isDead = _healthpoints <= 0;
        if (isDead) _Die();
        return isDead;
    }

    private void _Die()
    {
        StartCoroutine(Dead());
        //_animator.SetBool("Dead", true);
        //Destroy(gameObject);
    }

    IEnumerator Dead()
    {
        _animator.SetBool("Dead", true);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
