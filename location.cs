using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Location
{
    public partial class location
    {
        public static string output = "";
        public static void runClient(string[] args)
        {
            try
            {
                string theHost = "";
                int thePort = 43;
                string httpType = "";
                int timeout = 2000;
                bool Debug = false;

                List<string> Args = new List<string>();
                for (int i = 0; i < args.Length; i++)
                {
                    Args.Add(args[i]);
                }
                if (Args.Contains("-d"))
                {
                    Debug = true;
                    Args.RemoveRange(Args.IndexOf("-d"), 1);

                    if (Args.Contains("-h"))
                    {
                        theHost = Args[Args.IndexOf("-h") + 1];
                        Args.RemoveRange(Args.IndexOf("-h"), 2);
                    }
                    if (Args.Contains("-t"))
                    {
                        timeout = int.Parse(Args[Args.IndexOf("-t") + 1]);
                        Args.RemoveRange(Args.IndexOf("-p"), 2);
                    }
                    if (Args.Contains("-p"))
                    {
                        thePort = int.Parse(Args[Args.IndexOf("-p") + 1]);
                        Args.RemoveRange(Args.IndexOf("-p"), 2);
                    }
                    if (Args.Contains("-h0"))
                    {
                        httpType = "h0";
                        Args.RemoveAt(Args.IndexOf("-h0"));
                    }
                    if (Args.Contains("-h1"))
                    {
                        httpType = "h1";
                        Args.RemoveAt(Args.IndexOf("-h1"));
                    }
                    if (Args.Contains("-h9"))
                    {
                        httpType = "h9";
                        Args.RemoveAt(Args.IndexOf("-h9"));
                    }

                    string[] newArgs = new string[Args.Count];
                    for (int i = 0; i < Args.Count; i++)
                    {
                        newArgs[i] = Args[i];
                    }
                    TcpClient client;
                    StreamReader sr;
                    StreamWriter sw;
                    NetworkStream stream;

                    if (Debug)
                    {
                        Console.WriteLine("Debug Mode");
                    }
                    if (newArgs[0].Contains("404") && httpType != "-h") //declares error if 1st line back contains "404".
                    {
                        Console.WriteLine("ERROR: no entries found");
                    }
                    else
                    {
                        switch (httpType)
                        {
                            case "h0":                              //HTTP 1.0
                                client = new TcpClient();
                                client.Connect(theHost, thePort);
                                stream = client.GetStream();
                                stream.WriteTimeout = timeout;
                                stream.ReadTimeout = timeout;
                                if (newArgs.Length == 1)
                                {
                                    byte[] send = Encoding.ASCII.GetBytes("GET /?" + newArgs[0] + " HTTP/1.0\r\n\r\n");
                                    stream.Write(send, 0, send.Length);
                                    stream.Flush();
                                    sr = new StreamReader(stream);
                                    List<string> words = new List<string>();
                                    string tempresponse;
                                    string response = "";
                                    try
                                    {
                                        while ((tempresponse = sr.ReadLine()) != null)
                                        {
                                            words.Add(tempresponse);
                                        }
                                        response = string.Join("\r\n", words);
                                    }
                                    catch (IOException)
                                    {
                                        if (words.Count > 0)
                                        {
                                            response = string.Join("\r\n", words);

                                        }
                                    }
                                    response = response.Substring(response.LastIndexOf("\r\n\r\n"));
                                    Console.WriteLine(newArgs[0] + " is " + response.Trim());
                                }
                                else
                                {
                                    byte[] send = Encoding.ASCII.GetBytes("POST /" + newArgs[0] + " HTTP/1.0\r\nContent-Length: " + newArgs[1].Length + "\r\n\r\n" + newArgs[1] + "\r\n");
                                    stream.Write(send, 0, send.Length);
                                    stream.Flush();
                                    sr = new StreamReader(stream);
                                    List<string> words = new List<string>();
                                    string tresponse;
                                    string response = "";
                                    try
                                    {
                                        while ((tresponse = sr.ReadLine()) != null)
                                        {
                                            words.Add(tresponse);
                                        }
                                        response = string.Join("\r\n", words);
                                    }
                                    catch (IOException)
                                    {
                                        if (words.Count > 0)
                                        {
                                            response = string.Join("\r\n", words);

                                        }
                                    }
                                    Console.WriteLine(newArgs[0] + " location changed to be " + newArgs[1]);
                                }
                                client.Close();
                                stream.Close();
                                break;

                            case "h1":
                                client = new TcpClient();
                                client.Connect(theHost, thePort);
                                stream = client.GetStream();
                                stream.WriteTimeout = timeout;
                                stream.ReadTimeout = timeout;
                                if (newArgs.Length == 1)
                                {
                                    byte[] send = Encoding.ASCII.GetBytes("GET /?name=" + newArgs[0] + " HTTP/1.1\r\nHost: " + theHost + "\r\n\r\n");
                                    stream.Write(send, 0, send.Length);
                                    stream.Flush();
                                    sr = new StreamReader(stream);
                                    if (theHost == "")
                                    {
                                        string words = "";
                                        try
                                        {
                                            int x;
                                            while ((x = sr.Read()) > 0)
                                            {
                                                words += (char)x;
                                            }
                                        }
                                        catch { }
                                        words = words.Substring(words.IndexOf("\r\n\r\n")).Trim();
                                        Console.Write(newArgs[0] + " is " + words);
                                    }
                                    else
                                    {
                                        List<string> words = new List<string>();
                                        string tresponse;
                                        string response = "";
                                        try
                                        {
                                            while ((tresponse = sr.ReadLine()) != null)
                                            {
                                                words.Add(tresponse);
                                            }
                                            response = string.Join("\r\n", words);
                                        }
                                        catch (IOException)
                                        {
                                            if (words.Count > 0)
                                            {
                                                response = string.Join("\r\n", words);
                                            }
                                        }
                                        response = response.Substring(response.LastIndexOf("\r\n\r\n"));
                                        Console.WriteLine(newArgs[0] + " is " + response.Trim());
                                    }
                                }
                                else
                                {
                                    byte[] send = Encoding.ASCII.GetBytes("POST / HTTP/1.1\r\nHost: " + theHost + "\r\nContent-Length: " + (newArgs[1].Length + newArgs[0].Length + 15) + "\r\n\r\nname=" + newArgs[0] + "&location=" + newArgs[1]);
                                    stream.Write(send, 0, send.Length);
                                    stream.Flush();
                                    sr = new StreamReader(stream);
                                    List<string> words = new List<string>();
                                    string tresponse;
                                    string response = "";
                                    try
                                    {
                                        while ((tresponse = sr.ReadLine()) != null)
                                        {
                                            words.Add(tresponse);
                                        }
                                        response = string.Join("\r\n", words);
                                    }
                                    catch (IOException)
                                    {
                                        if (words.Count > 0)
                                        {
                                            response = string.Join("\r\n", words);

                                        }
                                    }
                                    Console.WriteLine(newArgs[0] + " location changed to be " + newArgs[1]);
                                }
                                client.Close();
                                stream.Close();
                                break;

                            case "h9":
                                client = new TcpClient();
                                client.Connect(theHost, thePort);
                                stream = client.GetStream();
                                stream.WriteTimeout = timeout;
                                stream.ReadTimeout = timeout;
                                if (newArgs.Length == 1)
                                {
                                    byte[] send = Encoding.ASCII.GetBytes("GET /" + newArgs[0] + "\r\n");
                                    stream.Write(send, 0, send.Length);
                                    stream.Flush();
                                    sr = new StreamReader(stream);
                                    List<string> words = new List<string>();
                                    string tresponse;
                                    string response = "";
                                    try
                                    {
                                        while ((tresponse = sr.ReadLine()) != null)
                                        {
                                            words.Add(tresponse);
                                        }
                                        response = string.Join("\r\n", words);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                        if (words.Count > 0)
                                        {
                                            response = string.Join("\r\n", words);
                                        }
                                    }
                                    response = response.Substring(response.LastIndexOf("\r\n\r\n"));
                                    Console.WriteLine(newArgs[0] + " is " + response.Trim());
                                    output = newArgs[0] + " is " + response.Trim();
                                }
                                else
                                {
                                    byte[] send = Encoding.ASCII.GetBytes("PUT /" + newArgs[0] + "\r\n\r\n" + newArgs[1] + "\r\n");
                                    stream.Write(send, 0, send.Length);
                                    stream.Flush();
                                    sr = new StreamReader(stream);
                                    List<string> words = new List<string>();
                                    string tempresponse;
                                    string response = "";
                                    try
                                    {
                                        while ((tempresponse = sr.ReadLine()) != null)
                                        {
                                            words.Add(tempresponse);
                                        }
                                        response = string.Join("\r\n", words);
                                    }
                                    catch (IOException)
                                    {
                                        if (words.Count > 0)
                                        {
                                            response = string.Join("\r\n", words);
                                        }
                                    }
                                    Console.WriteLine(newArgs[0] + " location changed to be " + newArgs[1]);
                                }
                                client.Close();
                                stream.Close();
                                break;

                            default:
                                if (Debug)
                                {
                                    Console.WriteLine("WHOIS");
                                }
                                client = new TcpClient();
                                client.Connect(theHost, thePort);
                                sw = new StreamWriter(client.GetStream());
                                sr = new StreamReader(client.GetStream());
                                client.SendTimeout = timeout;
                                client.ReceiveTimeout = timeout;

                                if (newArgs.Length < 1 || newArgs.Length > 2)
                                {
                                    Console.WriteLine("Please enter a valid number of arguments.");
                                }
                                else
                                {
                                    if (newArgs.Length == 1)
                                    {
                                        sw.WriteLine(newArgs[0]);
                                        sw.Flush();
                                        if (Debug)
                                        {
                                            Console.WriteLine("Post sent: " + newArgs[0]);
                                        }
                                        List<string> words = new List<string>();
                                        string tempresponse;
                                        string response = "";
                                        try
                                        {
                                            while ((tempresponse = sr.ReadLine()) != null)
                                            {
                                                words.Add(tempresponse);
                                            }
                                            response = string.Join("\r\n", words);
                                        }
                                        catch (IOException)
                                        {
                                            if (words.Count > 0)
                                            {
                                                response = string.Join("\r\n", words);

                                            }
                                        }
                                        Console.WriteLine(newArgs[0] + " is " + response);
                                        output = newArgs[0] + " is " + response;
                                    }
                                    else
                                    {
                                        sw.WriteLine(newArgs[0] + " " + newArgs[1]);
                                        sw.Flush();
                                        List<string> words = new List<string>();
                                        string tresponse;
                                        string response = "";
                                        try
                                        {
                                            while ((tresponse = sr.ReadLine()) != null)
                                            {
                                                words.Add(tresponse);
                                            }
                                            response = string.Join("\r\n", words);
                                        }
                                        catch (IOException)
                                        {
                                            if (words.Count > 0)
                                            {
                                                response = string.Join("\r\n", words);
                                            }
                                        }
                                        Console.WriteLine(newArgs[0] + " location changed to be " + newArgs[1]);
                                        output = newArgs[0] + " location changed to be " + newArgs[1];
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
