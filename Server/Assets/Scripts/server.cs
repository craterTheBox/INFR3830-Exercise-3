using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class server : MonoBehaviour
{

    public static GameObject yourCube;
    private byte[] buffer;
    private IPHostEntry host;
    private IPAddress ip = IPAddress.Parse("10.0.0.50");
    private IPEndPoint localEP;
    private Socket serverSocket;
    private IPEndPoint client;
    private EndPoint remoteClient;

    void RunServer()
    {
        buffer = new byte[512];

        host = Dns.GetHostEntry(Dns.GetHostName());

        localEP = new IPEndPoint(ip, 11111);

        serverSocket = new Socket(ip.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

        client = new IPEndPoint(IPAddress.Any, 0); //0 for any available port
        remoteClient = (EndPoint)client;

        serverSocket.Bind(localEP);

        //Console.WriteLine("Waiting for data...");
        //Console.Read();

    }

    // Start is called before the first frame update
    void Start()
    {
        yourCube = GameObject.Find("Cube");
        RunServer();
    }

    // Update is called once per frame
    void Update()
    {
        int rec = serverSocket.ReceiveFrom(buffer, ref remoteClient);

        //Console.WriteLine("Received: {0}    from Client: {1}", Encoding.ASCII.GetString(buffer, 0, rec), remoteClient.ToString());

        float networkX = float.Parse(Encoding.ASCII.GetString(buffer, 0, rec));
        Vector3 networkTransform = new Vector3(networkX, 1.0f, 0.0f);
        yourCube.transform.position = networkTransform;
        Debug.Log("Updated Cube's x to: " + yourCube.transform.position.x);
    }
}
