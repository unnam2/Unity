using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIDamage : UIWorld
{
    private const float TIME = 1f;
    private const float SPEED = 1f;

    public Text m_damage;

    private Vector3 m_up;

    protected override void OnEnableAfterFrame()
    {
        StartCoroutine(Run());
        m_up = transform.position;
        m_up.y += 1f;
        m_up = m_up - transform.position;
        m_up.Normalize();
    }
    protected override void OnUpdate()
    {
        transform.position = transform.position + m_up * Time.deltaTime * SPEED;
    }

    private IEnumerator Run()
    {
        yield return new WaitForSeconds(TIME);
        Remove();
    }
}
