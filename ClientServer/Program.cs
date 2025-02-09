using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using ClientSide;

ClientServer();



static void ClientServer()
{
    string hostName = Dns.GetHostName();
    string iPAddress = Dns.GetHostEntry(hostName).ToString();
    int port = 42101;
    List<Users> listUser = new List<Users>
{
    new Users {name="Yousha", designation="jr"},
    new Users {name="Urwa", designation="Sr"}
};

    try
    {
        using (TcpClient client = new TcpClient(hostName, port))
        using (NetworkStream stream = client.GetStream())
        {
 
            foreach (var user in listUser)
            {
                string xml = user.ToXML();
                byte[] data = Encoding.UTF8.GetBytes(xml);
                stream.Write(data, 0, data.Length);
                Console.WriteLine($"Sent: Name:{user.name} Designation: {user.designation}");

                Thread.Sleep(1000); // Delay for clarity
                Console.ReadKey();
            }
        }
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