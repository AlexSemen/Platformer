using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(EnemyVision))]
[RequireComponent(typeof(EnemyMove))]
[RequireComponent(typeof(EnemyAttack))]
[RequireComponent(typeof(Animator))]

public class Samurai : MonoBehaviour
{
    [SerializeField] private Transform _castPoint;
    [SerializeField] private bool _isAttack;

    private SpriteRenderer _spriteRenderer;
    private EnemyVision _enemyVision;
    private EnemyMove _enemyMove;
    private EnemyAttack _enemyAttack;
    private Animator _animator;
    private Player _player;

    public Transform CastPoint => _castPoint;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemyVision = GetComponent<EnemyVision>();
        _enemyMove = GetComponent<EnemyMove>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (_isAttack == false)
        {
            if (_player == null)
            {
                _enemyMove.Move();
            }
            else
            {
                LookPlayer();
                if (Vector2.Distance(transform.position, _player.transform.position) > _enemyAttack.DistanceAttack)
                {
                    _enemyMove.Move();
                }
                else
                {
                    _enemyAttack.Attack();
                }
            }
        }
    }

    public void ChangeDirection()
    {
        _castPoint.localPosition = -_castPoint.localPosition;

        if (_spriteRenderer.flipX)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }

        _enemyVision.ChangeDirection();
        _enemyMove.ChangeDirection();
        _enemyAttack.ChangeDirection();
    }

    public void SetPlayer(Player player) 
    {
        _player = player;
    }
    
    private void LookPlayer()
    {
        if (_player.transform.position.x < transform.position.x)
        {
            if(_spriteRenderer.flipX != false)
            {
                ChangeDirection();
            }
        }
        else
        {
            if (_spriteRenderer.flipX == false)
            {
                ChangeDirection();
            }
        }
    }

    public void TakeDamage()
    {
        Debug.Log("������");
    }
}
