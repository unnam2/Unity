using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

class Server
{
    private const int PORT = 7000;
    private const int BUFF_SIZ = 1024;

    public class DataClient
    {
        public Socket socket;
        public NetworkStream stream;
        public string id;
    }

    private Socket m_server;
    private List<DataClient> m_clientList;

    public Server(int port)
    {
        PacketManager.Init();

        m_server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        m_clientList = new List<DataClient>();

        m_server.Bind(new IPEndPoint(IPAddress.Any, PORT));

        new Thread(delegate () { Listen(); }).Start();
        //new Thread(delegate () { Stat(); }).Start();

        Console.CancelKeyPress += (sender, e) =>
        {
            m_server.Close();
        };

        Console.WriteLine("ready");
    }

    //private void Stat()
    //{
    //    while (true)
    //    {
    //        Thread.Sleep(1000 * 10);
    //    }
    //}

    public void SendBroad(Packet packet, DataClient client = null)
    {
        byte[] buff = PacketManager.GetBuff(packet);

        for (int i = 0; i < m_clientList.Count; ++i)
        {
            if (client != null && m_clientList[i].id == client.id)
            {
                continue;
            }
            m_clientList[i].stream.Write(buff, 0, buff.Length);
        }
    }

    public void Send(Packet packet, string id)
    {
        byte[] buff = PacketManager.GetBuff(packet);

        m_clientList.Find(rhs => rhs.id == id).stream.Write(buff, 0, buff.Length);
    }

    private void Listen()
    {
        while (m_server != null)
        {
            m_server.Listen(5);
            DataClient client = new DataClient();
            client.socket = m_server.Accept();
            client.id = client.socket.RemoteEndPoint.ToString();
            m_clientList.Add(client);

            new Thread(delegate () { Receive(client); }).Start();
        }
    }

    private void Receive(DataClient client)
    {
        Console.WriteLine(client.id + " connect");

        client.stream = new NetworkStream(client.socket);

        {
            PacketString packet = new PacketString();
            packet.key = "your id";
            packet.value = client.id;
            Send(packet, client.id);
        }
        {
            PacketString packet = new PacketString();
            packet.key = "ping";
            Send(packet, client.id);
        }

        for (int i = 0; i < m_clientList.Count; ++i)
        {
            PacketString packet = new PacketString();
            packet.key = "other id";
            packet.value = m_clientList[i].id;
            SendBroad(packet);
        }

        byte[] buff = new byte[BUFF_SIZ];

        while (true)
        {
            try
            {
                int length = client.stream.Read(buff, 0, buff.Length);
                if (length <= 0)
                {
                    ExitClient(client);
                    break;
                }

                int begin = 0;

                while (begin < length)
                {
                    Packet pac = PacketManager.GetPacket(PacketManager.GetPacketType(buff, begin), buff, begin);
                    begin += Marshal.SizeOf(pac);

                    if (pac is PacketTransform)
                    {
                        PacketTransform packet = (PacketTransform)pac;
                        SendBroad(packet, client);
                    }
                    if (pac is PacketMessage)
                    {
                        PacketMessage packet = (PacketMessage)pac;
                        Console.WriteLine(packet.data);
                    }
                    if (pac is PacketString)
                    {
                        PacketString packet = (PacketString)pac;
                        if (packet.key == "ping")
                        {
                            Send(packet, client.id);
                        }
                    }
                    if (pac is PacketMove)
                    {
                        PacketMove packet = (PacketMove)pac;
                        SendBroad(packet);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ExitClient(client);
                break;
            }
        }
    }
    private void ExitClient(DataClient client)
    {
        PacketString packet = new PacketString();
        packet.key = "other ex";
        packet.value = client.id;
        SendBroad(packet);

        Console.WriteLine(client.socket.RemoteEndPoint + " disconnect");
        client.socket.Close();
        m_clientList.Remove(client);
    }
}
