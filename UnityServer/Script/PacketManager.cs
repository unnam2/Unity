using System;
using System.Runtime.InteropServices;

public class PacketManager
{
    public static int PacketSize { get; private set; }

    public static void Init()
    {
        PacketSize = Marshal.SizeOf(new Packet());
    }

    public static Packet GetPacket(Type type, byte[] buff, int begin)
    {
        object obj = Activator.CreateInstance(type);
        if (!(obj is Packet))
        {
            return null;
        }

        Packet packet = (Packet)obj;
        int end = Marshal.SizeOf(packet);
        byte[] newBuff = new byte[end];
        Buffer.BlockCopy(buff, begin, newBuff, 0, end);

        unsafe
        {
            fixed (byte* fixed_buffer = newBuff)
            {
                Marshal.PtrToStructure((IntPtr)fixed_buffer, packet);
            }
        }
        return packet;
    }

    public static Type GetPacketType(byte[] buff, int begin)
    {
        Packet packet = new Packet();

        byte[] newBuff = new byte[Marshal.SizeOf(packet)];

        Buffer.BlockCopy(buff, begin, newBuff, 0, newBuff.Length);

        unsafe
        {
            fixed (byte* fixed_buffer = newBuff)
            {
                Marshal.PtrToStructure((IntPtr)fixed_buffer, packet);
            }
        }
        return Type.GetType(packet.What);
    }

    public static byte[] GetBuff(Packet packet)
    {
        byte[] buff = new byte[Marshal.SizeOf(packet)];

        unsafe
        {
            fixed (byte* fixed_buffer = buff)
            {
                Marshal.StructureToPtr(packet, (IntPtr)fixed_buffer, false);
            }
        }

        return buff;
    }
}
