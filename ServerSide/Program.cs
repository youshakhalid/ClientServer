
using System.Net;
using System.Net.Sockets;
using System.Text;
using ClientSide;




ExecuteServer();
static void ExecuteServer()
{


    //Establishing and setting up Connection
    IPHostEntry iPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
    IPAddress iPAddress = iPHostEntry.AddressList[0];
    int port = 42101;
    TcpListener listener = new TcpListener(iPAddress, port);
    ISerialize serializable = new Users();
  
    Users user = new Users();
    

    try
    {
        listener.Start();
        Console.WriteLine("Listening to port 42101");

        while (true)
        {
            using (TcpClient client = listener.AcceptTcpClient())
            using (NetworkStream stream = client.GetStream())
            {
                byte[] buffer = new byte[1024];

                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break; // Client disconnected

                    string receivedXml = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    serializable.FromXML<Users>(receivedXml);

                    Console.WriteLine($"Received\n Name: {user.name} Designation: {user.designation} " );
                }
            }
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }

}