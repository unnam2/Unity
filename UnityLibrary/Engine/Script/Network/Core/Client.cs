using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using UnityEngine;

public class Client : MonoSingleTon<Client>
{
    private const int PORT = 7000;
    private const int BUFF_SIZ = 1024;

    private Socket m_socket;
    private byte[] m_receiveBuffer;
    private NetworkStream m_stream;
    private Queue<Packet> m_packetQueue;
    private float m_before;

    public string ID { get; private set; }

    public void Init() { }

    private void Awake()
    {
        m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        m_packetQueue = new Queue<Packet>();
        m_receiveBuffer = new byte[BUFF_SIZ];
    }

    private void Start()
    {
        try
        {
            m_socket.Connect(new IPEndPoint(IPAddress.Loopback, 7000));
            m_stream = new NetworkStream(m_socket);
            m_socket.BeginReceive(m_receiveBuffer, 0, m_receiveBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            m_before = Time.time;
        }
        catch (Exception e)
        {
            Debug.LogError("catch " + e);
        }
    }

    private void ReceiveCallback(IAsyncResult AR)
    {
        if (!m_socket.Connected)
        {
            return;
        }

        try
        {
            int begin = 0;
            int length = m_socket.EndReceive(AR);

            while (begin < length)
            {
                Packet pac = PacketManager.GetPacket(PacketManager.GetPacketType(m_receiveBuffer, begin), m_receiveBuffer, begin);
                begin += Marshal.SizeOf(pac);
                m_packetQueue.Enqueue(pac);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            m_socket.Close();
            return;
        }
        m_socket.BeginReceive(m_receiveBuffer, 0, m_receiveBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
    }

    public void Send(Packet pac)
    {
        byte[] buff = PacketManager.GetBuff(pac);
        if(m_socket.Connected)
        {
            m_stream.Write(buff, 0, buff.Length);
        }
    }

    private void OnApplicationQuit()
    {
        m_socket.Close();
    }

    private void Update()
    {
        PacketProcess.Process(m_packetQueue, Instance);
    }

    public void SetID(string id)
    {
        ID = id;
    }
}
