using System;

class Program
{
    static void Main(string[] args)
    {
        Server server = new Server(7000);

        int i = 0;

        while (true)
        {
            string data = Console.ReadLine();

            PacketMessage message = new PacketMessage();
            message.data = data;

            if(i%2 == 0)
            {
                server.SendBroad(message);
            }
            else
            {
                server.SendBroad(message);
                server.SendBroad(message);
            }
            ++i;
        }
    }
}
