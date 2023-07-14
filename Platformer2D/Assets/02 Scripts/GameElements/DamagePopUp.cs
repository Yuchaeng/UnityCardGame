using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    private TMP_Text _damageAmount;
    private float _fadeSpeed = 1.0f;
    private float _moveSpeedY = 0.5f;
    private Color _color;

    public static DamagePopUp Create(LayerMask layer, Vector3 pos, int damageAmount)
    {
        DamagePopUp damagePopUp = Instantiate(DamagePopUpAssets.instance[layer],
                                              pos,
                                              Quaternion.identity);
        damagePopUp._damageAmount.text = damageAmount.ToString();
        return damagePopUp;
    }

    private void Awake()
    {
        _damageAmount = GetComponent<TMP_Text>();
        _color = _damageAmount.color;
    }

    private void Update()
    {
        transform.position += Vector3.up * _moveSpeedY * Time.deltaTime;
        _color.a -= _fadeSpeed * Time.deltaTime;
        _damageAmount.color = _color;

        if (_color.a <= 0.0f)
            Destroy(gameObject);
    }
}
