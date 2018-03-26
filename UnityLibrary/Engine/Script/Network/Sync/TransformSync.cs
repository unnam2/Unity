using System.Collections;
using UnityEngine;

public class TransformSync
{
    private Pooling m_my;

    public TransformSync(Pooling poo)
    {
        m_my = poo;
    }

    public void Begin(float tick = 1f)
    {
        m_my.StartCoroutine(IBegin(tick));
    }

    private IEnumerator IBegin(float tick)
    {
        WaitForSeconds wait = new WaitForSeconds(tick);
        while (true)
        {
            PacketTransform pac = new PacketTransform();
            pac.id = m_my.ID;
            pac.position = m_my.transform.position;
            pac.rotation = m_my.transform.eulerAngles;
            Client.Instance.Send(pac);

            if (tick > 0f)
            {
                yield return wait;
            }
            else
            {
                yield return null;
            }
        }
    }
}
