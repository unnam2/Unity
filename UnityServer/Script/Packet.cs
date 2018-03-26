using System.Runtime.InteropServices;
using UnityEngine;

[StructLayout(LayoutKind.Sequential)]
public class Packet
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    private string m_what;

    public string What { get { return m_what; } }

    public Packet()
    {
        m_what = ToString();
    }
}

[StructLayout(LayoutKind.Sequential)]
public class PacketTransform : Packet
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string id;

    public Vector3 position;
    public Vector3 rotation;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public class PacketMessage : Packet
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
    public string data;
}

[StructLayout(LayoutKind.Sequential)]
public class PacketString : Packet
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string key;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string value;
}

[StructLayout(LayoutKind.Sequential)]
public class PacketMove : Packet
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
    public string id;

    public Vector3 forward;
}
