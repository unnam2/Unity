using UnityEngine;

public class Control : Pooling
{
    public string id;
    public Vector3 BeforeForward;

    protected override void OnStart()
    {
        base.OnStart();
        if (Client.Instance.ID == id)
        {
            PacketTransform pac = new PacketTransform();
            pac.id = id;
            pac.position = transform.position;
            pac.rotation = transform.eulerAngles;
        }
    }

    protected override void OnUpdate()
    {
        if (Client.Instance.ID != id)
        {
            if (BeforeForward != Vector3.zero)
            {
                transform.position += Time.deltaTime * BeforeForward;
            }
            return;
        }

        Vector3 currentForward = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            currentForward -= transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            currentForward += transform.right;
        }
        if (Input.GetKey(KeyCode.W))
        {
            currentForward += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            currentForward -= transform.forward;
        }

        if (currentForward != Vector3.zero)
        {
            currentForward.Normalize();
            transform.position += currentForward * Time.deltaTime;
            if (currentForward != BeforeForward)
            {
                BeforeForward = currentForward;
                SendMove();
            }
        }

        if (BeforeForward != Vector3.zero && currentForward == Vector3.zero)
        {
            SendStop();
            BeforeForward = currentForward;
        }
    }

    private void SendMove()
    {
        PacketMove pac = new PacketMove();
        pac.forward = BeforeForward;
        pac.id = id;
        Client.Instance.Send(pac);
    }
    private void SendStop()
    {
        PacketMove pac = new PacketMove();
        pac.forward = Vector3.zero;
        pac.id = id;
        Client.Instance.Send(pac);
    }

    protected override void OnRemove()
    {
        id = string.Empty;
        base.OnRemove();
    }
}
