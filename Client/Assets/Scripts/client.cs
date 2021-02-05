using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Lec4
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;


public class client : MonoBehaviour
{
    public GameObject myCube;

    private static byte[] outBuffer = new byte[512];
    private static IPEndPoint remoteEP;
    private static Socket client_socket;    

    public static void RunClient() {
        IPAddress ip = IPAddress.Parse("10.0.0.50");
        remoteEP = new IPEndPoint(ip, 11111);

        client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        try {
            outBuffer = Encoding.ASCII.GetBytes("Testing network......");
            client_socket.SendTo(outBuffer, remoteEP);

            //client_socket.Shutdown(SocketShutdown.Both);
            //client_socket.Close();
        }
        catch (Exception e) {
            Debug.LogException(e);
        }
    }

    public static void CloseClient() {
        client_socket.Shutdown(SocketShutdown.Both);
        client_socket.Close();
    }

    // Start is called before the first frame update
    void Start()
    {
        myCube = GameObject.Find("Cube");

        RunClient();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Cube x: " + myCube.transform.position.x + "\ty: " + myCube.transform.position.y + "\tz: " + myCube.transform.position.z);

        //string cubeTransform = "x: " + myCube.transform.position.x.ToString() + " y: " + myCube.transform.position.y.ToString() + " z: " + myCube.transform.position.z.ToString();
        //outBuffer = Encoding.ASCII.GetBytes(cubeTransform);
        //outBuffer = BitConverter.GetBytes(myCube.transform.position.x);

        outBuffer = Encoding.ASCII.GetBytes(myCube.transform.position.x.ToString());

        client_socket.SendTo(outBuffer, remoteEP);

    }
}
