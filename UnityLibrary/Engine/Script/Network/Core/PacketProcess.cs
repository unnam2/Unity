using System.Collections.Generic;
using UnityEngine;

public class PacketProcess
{
    public static void Process(Queue<Packet> queue, Client client)
    {
        while (queue.Count > 0)
        {
            Packet item = queue.Dequeue();

            if (item is PacketMessage)
            {
                PacketMessage packet = (PacketMessage)item;
            }
            if (item is PacketString)
            {
                PacketString packet = (PacketString)item;
                if (packet.key == "your id")
                {
                    Control con = (Control)PoolingManage.Pooling.Create("Control", Vector3.zero);
                    client.SetID(packet.value);
                    con.id = client.ID;
                }
                if (packet.key == "other id")
                {
                    if (packet.value == client.ID || GlobalData.Instance.m_list.Find(rhs => rhs.id == packet.value) != null)
                    {
                        continue;
                    }

                    Control con = (Control)PoolingManage.Pooling.Create("Control", Vector3.zero);
                    con.id = packet.value;
                    GlobalData.Instance.m_list.Add(con);
                }
                if (packet.key == "other ex")
                {
                    Control con = GlobalData.Instance.m_list.Find(rhs => rhs.id == packet.value);
                    GlobalData.Instance.m_list.Remove(con);
                    con.Remove();
                }
                if (packet.key == "ping")
                {
                    UIPing[] ping = PoolingManage.UIHud.GetList<UIPing>();
                    if (ping.Length > 0)
                    {
                        ping[0].Check();
                    }
                    Client.Instance.Send(packet);
                }
            }
            if (item is PacketTransform)
            {
                PacketTransform packet = (PacketTransform)item;
                if (packet.id == client.ID)
                {
                    continue;
                }

                Control con = GlobalData.Instance.m_list.Find(rhs => rhs.id == packet.id);
                if (con != null)
                {
                    con.transform.position = packet.position;
                    con.transform.eulerAngles = packet.rotation;
                }
            }
            if (item is PacketMove)
            {
                PacketMove packet = (PacketMove)item;
                if (packet.id == client.ID)
                {
                    continue;
                }
                Control con = GlobalData.Instance.m_list.Find(rhs => rhs.id == packet.id);
                if (con != null)
                {
                    con.BeforeForward = packet.forward;
                }
            }
        }
    }
}
