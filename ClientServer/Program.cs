using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using ClientSide;

ClientServer();



static void ClientServer()
{
    List<Users> listUser = new List<Users>
{
    new Users {name="Yousha", designation="jr"},
    new Users {name="Urwa", designation="Sr"}
};

    try
    {
        //Establishing and setting up Connection
        IPHostEntry iPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress iPAddress = iPHostEntry.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(iPAddress, 11111);

        Socket sender = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            sender.Connect(localEndPoint);
            Console.WriteLine($"Socket Connected to {sender.RemoteEndPoint.ToString()}");

            //for sending messages

            byte[] messageSent = ListToByte(listUser);
            sender.Send(messageSent);
            
           

            //for recieving message
            byte[] messageRecieved = new byte[1024];
            int byteRecieve = sender.Receive(messageRecieved);
            var mStream = new MemoryStream(byteRecieve);
            Console.WriteLine($"Recived Message: {Encoding.ASCII.GetString(messageRecieved, 0, byteRecieve)}");

            sender.Shutdown(SocketShutdown.Both);
            Console.ReadKey();
            sender.Close();
        }

        catch (ArgumentException e)
        {
            Console.WriteLine(e);
        }
        catch (SocketException e) 
        {
            Console.WriteLine(e); 
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    catch(Exception e)
    {
        Console.WriteLine(e);
    }
}

static byte[] ListToByte(List<Users> users)
{
    MemoryStream memoryStream = new MemoryStream();
    BinaryWriter writer = new BinaryWriter(memoryStream);
    using (memoryStream)
    {

    }
    using (writer)
    {
        writer.Write(users.Count);
        foreach(var user in users)
        {
            writer.Write(user.name);
            writer.Write(user.designation);

        }
        return memoryStream.ToArray();
    }
}
