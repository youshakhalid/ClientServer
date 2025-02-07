
using System.Net;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Text;
using ClientSide;
using static System.Runtime.InteropServices.JavaScript.JSType;



ExecuteServer();
static void ExecuteServer()
{

    //Establishing and setting up Connection
    IPHostEntry iPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
    IPAddress iPAddress = iPHostEntry.AddressList[0];
    IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 11111);

    Socket listner = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

    try
    {
        listner.Bind(iPEndPoint);
        listner.Listen(10);

        while (true)
        {
            Console.WriteLine("Waiting for Connection...");
            //Accept Client Connection
            Socket clientSocket = listner.Accept();
            Console.WriteLine("Connected:" + clientSocket.LocalEndPoint);
            //Recieve Message
            byte[] bytesCount = new byte[1024];
            MemoryStream memoryStream = new MemoryStream();
            int byteReceive;

            while ((byteReceive = clientSocket.Receive(bytesCount)) > 0)
            {
                memoryStream.Write(bytesCount, 0, byteReceive);
                if (byteReceive < bytesCount.Length)
                    break;
            }
            // Console.WriteLine($"Message Recieved: {message}");

            memoryStream.Seek(0, SeekOrigin.Begin);
            var users = ByteToList(memoryStream.ToArray());

            foreach (var user in users)
            {
                Console.WriteLine($"Name: {user.name}\nDesignation: {user.designation}\n------------------------------------");
            }

            //Send Message
            byte[] sendMessage = Encoding.ASCII.GetBytes("Users Recieved");
            clientSocket.Send(sendMessage);

            //Shutdown Connection

            clientSocket.Shutdown(SocketShutdown.Both);
            Console.ReadKey();
            clientSocket.Close();
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }

    List<Users> ByteToList(byte[] arr)
    {
        var users = new List<Users>();
        MemoryStream memoryStream = new MemoryStream(arr);
        BinaryReader binaryReader = new BinaryReader(memoryStream);
        using (memoryStream)
        using (binaryReader)
        {
            int userCount = binaryReader.ReadInt32();
            for (int i = 0; i < userCount; i++)
            {
                string name = binaryReader.ReadString();
                string designation = binaryReader.ReadString();
                users.Add(new Users { name = name, designation = designation });
            }
        }
        return users;
    }
}