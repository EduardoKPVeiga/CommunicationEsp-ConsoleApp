using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.InteropServices;

public class Principal
{
    public static bool keyPressed = false;
    public static bool keyReleased = false;

    public static bool threadState = true;

    //IPAddress espIP = IPAddress.Parse("192.168.1.158");
    ConsoleKeyInfo keyinfo = new ConsoleKeyInfo();
    string ESP8266IP = "";
    string port = "";

    public Principal()
    {
        Run();
    }

    private void Run()
    {
        while (!Verifications.Check.ipCheck(ESP8266IP))
        {
            Console.Write("IP ESP8266: ");
            ESP8266IP = Console.ReadLine();
            if (!Verifications.Check.ipCheck(ESP8266IP))
                Console.WriteLine("IPAddress wrong!");
        }
        Console.Clear();
        while (!Verifications.Check.portCheck(port))
        {
            Console.Write("ESP8266 port: ");
            port = Console.ReadLine();
            if (!Verifications.Check.portCheck(port))
                Console.WriteLine("Port wrong!");
        }
        Console.Clear();

        Console.WriteLine("Running...");
        char before = Convert.ToChar(27); // Esc
        char current = Convert.ToChar(32); // Space

        do
        {
            if (Console.KeyAvailable)
            {
                keyinfo = Console.ReadKey();
                if (keyinfo.KeyChar == 'w' || keyinfo.KeyChar == 's' || keyinfo.KeyChar == 'a' || keyinfo.KeyChar == 'd' || keyinfo.KeyChar == 32)
                    UDP.UDPClient.SendData(IPAddress.Parse(ESP8266IP), Convert.ToInt32(port), keyinfo.KeyChar);
                else
                    Console.Clear();
                Thread.Sleep(20);
            }
            UDP.UDPClient.SendData(IPAddress.Parse(ESP8266IP), Convert.ToInt32(port), Convert.ToChar(32));
            Thread.Sleep(10);
        }
        while (keyinfo.Key != ConsoleKey.Escape);
        threadState = false;

    }
}

