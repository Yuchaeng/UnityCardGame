using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool isDetected => detected;
    public Collider2D detected
    {
        get
        {
            return _detected && _ignorings.Contains(_detected) == false ? _detected : null;
        }
        private set
        {
            if (_detected == value)
                return;

            _isDetected = value;
            _detected = value;
            if (_isDetected)
                _latest = value;

        }
    }

    [SerializeField] private bool _isDetected;
    private Collider2D _detected;
    private Collider2D _latest;  //���������� detect�� collider�� ������ ������ ����
    private List<Collider2D> _ignorings = new List<Collider2D>();
    [SerializeField] private Vector2 _castOffset;
    [SerializeField] private Vector2 _castSize;  //���� ������ ���� �ɸ��� �� ����ִٰ� ����
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private Vector2 _belowCastOffset;
    [SerializeField] private float _belowCastLength = 2.0f;

    //�׽�Ʈ��
    [SerializeField] private bool _isGroundExistBelow;

    public bool IsGroundExistBelow()
    {
        //���� ��� ���� �ʴٸ� �׾Ʒ� ���� �������� �ʴ´�.
        if (isDetected == false)
            return false;

        RaycastHit2D hit = Physics2D.BoxCast(origin: (Vector2)transform.position + _castOffset + Vector2.down * _castSize.y + _belowCastOffset,
                                             size: _castSize, 
                                             angle: 0.0f, 
                                             direction: Vector2.down, 
                                             distance: _belowCastLength,
                                             layerMask: _groundMask);
        return hit.collider;
    }

    public void IgnoreLatest(CapsuleCollider2D collider)
    {
        StartCoroutine(E_IgnoreGroundUntilPassedIt(collider, _latest));
    }

    IEnumerator E_IgnoreGroundUntilPassedIt(CapsuleCollider2D collider, Collider2D ground)
    {
        _ignorings.Add(ground);
        Physics2D.IgnoreCollision(collider, ground, true);
        yield return new WaitUntil(() =>
        {
            Collider2D[] cols = 
            Physics2D.OverlapCapsuleAll(point: (Vector2)transform.position + collider.offset,
                                     size: collider.size,
                                     direction: collider.direction,
                                     angle: 0.0f,
                                     layerMask: _groundMask);
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i] == ground)
                    return true; 
            }
            return false;
        });

        yield return new WaitUntil(() =>
        {
            Collider2D[] cols =
            Physics2D.OverlapCapsuleAll(point: (Vector2)transform.position + collider.offset,
                                     size: collider.size,
                                     direction: collider.direction,
                                     angle: 0.0f,
                                     layerMask: _groundMask);
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i] == ground)   //��ģ���߿� ��� �����ϴ°� ������ false �����ؼ� ��� ���� 
                    return false;
            }
            return true;
        });

        Physics2D.IgnoreCollision(collider, ground, false);
        _ignorings.Remove(ground);
    }


    private void FixedUpdate()
    {
        _detected = Physics2D.OverlapBox((Vector2)transform.position + _castOffset, _castSize, 0.0f, _groundMask);
        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + (Vector3)_castOffset, _castSize);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireCube(transform.position + (Vector3)_castOffset + Vector3.down * _castSize.y + Vector3.down * _belowCastLength / 2.0f + (Vector3)_belowCastOffset,
                            new Vector3(_castSize.x, _belowCastLength + _castSize.y));
    }
}
