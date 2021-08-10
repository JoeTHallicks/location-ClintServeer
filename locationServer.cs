using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Collections.Concurrent;
using System.Windows.Forms;
using Location;

namespace LocationServer
{
    public class locationserver
    {
        public static ConcurrentDictionary<string, string> userDetails;
        public static StreamWriter stw;
        public static DateTime now;
        public static void Save()
        {
            StreamWriter stw = File.AppendText("details.txt");
            foreach (KeyValuePair<string, string> data in userDetails)
            {
                stw.WriteLine(data.Key + " " + data.Value);  
                stw.Flush();
            }
            stw.Close();
        }
        public static void Load()   
        {
            string[] lines = File.ReadAllLines("UserData.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(' ');
                userDetails.TryAdd(parts[0], parts[1]);                                                                      //exception 
            }
        }
        public static void runServer(List<string> args)
        {
            string filePath = "SLog.txt";      
            stw = File.AppendText(filePath);     
            now = DateTime.Now;     
            bool debug = false;    
            int timeout = 2000;                                                                                                 //default timeout
            int thePort = 43;      
            if (args.Contains("-l"))                             
            {
                filePath = args[args.IndexOf("-l") + 1];        
            }
            if (args.Contains("-d"))
            {
                debug = true;       
            }
            if (args.Contains("-t"))
            {
                timeout = int.Parse(args[args.IndexOf("-t") + 1]);                                                               //check timeout 
            }
            if (args.Contains("-p"))
            {
                thePort = int.Parse(args[args.IndexOf("-p") + 1]);                                                                 //port check
            }
            if (debug)
            {
                Console.WriteLine("Debug Mode");
                stw.WriteLine("Debug Mode");
            }
            stw.WriteLine("Server ran at: " + now);      
            Console.WriteLine("Timeout set to: " + timeout);
            Console.WriteLine("Listening on port: " + thePort);
            stw.WriteLine("Timeout set to: " + timeout + "\r\n" + "Listening on port: " + thePort);     //repeating the previous output for the benefit of the log
            stw.Flush();
            TcpListener listener; Socket connection; Handler handler;
            userDetails = new ConcurrentDictionary<string, string>();                                                     //details container
            try
            {
                listener = new TcpListener(IPAddress.Any, thePort); listener.Start();
                Console.WriteLine("I'm listening...");
                while (true)
                {
                    connection = listener.AcceptSocket();
                    handler = new Handler();
                    Thread thread = new Thread(() => handler.doRequest(connection, userDetails, timeout, ref stw));    //calls ref from Do request
                    thread.Start();
                }
            }
            catch (Exception e)  {
                Console.WriteLine("Error: " + e);                                                                   //error exceptions thrown
                stw.WriteLine("Error: " + e);
            }
            finally {
                stw.Close();
            }
        }
    }
    public class Handler
    { public void doRequest(Socket connection, ConcurrentDictionary<string, string> details, int timeout, ref StreamWriter fs)       //Do request sends parameters
        {
            NetworkStream socketStream;
            socketStream = new NetworkStream(connection);
            Console.WriteLine("Connection detected");
            fs.WriteLine("Connection detected");
            fs.Flush();
            try {
                StreamWriter sw = new StreamWriter(socketStream);
                StreamReader sr = new StreamReader(socketStream);
                socketStream.WriteTimeout = timeout;
                socketStream.ReadTimeout = timeout;
                string response = null;
                try {
                    int x;
                    while ((x = sr.Read()) > 0)
                    {
                        response += (char)x;        
                    }
                }
                catch { }
                fs.WriteLine("Recieved: " + response); fs.Flush();

                if (response.StartsWith("GET " + "/") || response.StartsWith("PUT " + "/") || response.StartsWith("POST " + "/"))
                {
                    if (response.Contains("HTTP/1.0"))                                                                                                        //HTTP 1.0
                    {
                        if (response.StartsWith("GET"))                                                                                                         //get request
                        {
                            int endOfNameIndex = 0;
                            for (int i = response.IndexOf('?'); i < response.Length; i++)                                                                    //when name ends indeex of reponse
                            {
                                if (response[i] == ' ')
                                {
                                    endOfNameIndex = i - response.IndexOf('?');     
                                    break;
                                }
                            }
                            string name = response.Substring(response.IndexOf('?') + 1, endOfNameIndex).Trim(); 
                            if (details.ContainsKey(name))
                            {
                                Console.Write("HTTP/1.0 200 OK\r\nContent-Type: text/plain\r\n\r\n" + details[name] + "\r\n");
                                sw.Write("HTTP/1.0 200 OK\r\nContent-Type: text/plain\r\n\r\n" + details[name] + "\r\n");      //responses written to console, client, and log file
                                sw.Flush();
                                fs.WriteLine("HTTP / 1.0 200 OK\r\nContent - Type: text / plain\r\n\r\n" + details[name] + "\r\n");
                                fs.Flush();
                            }
                            else
                            {
                                Console.Write("HTTP/1.0 404 Not Found\r\nContent-Type: text/plain\r\n\r\n");  //responses written to console, client, and log file
                                sw.Write("HTTP/1.0 404 Not Found\r\nContent-Type: text/plain\r\n\r\n");   
                                sw.Flush();
                                fs.WriteLine("HTTP/1.0 404 Not Found\r\nContent-Type: text/plain\r\n\r\n");
                                fs.Flush();
                            }
                        }
                        else                                                                                                                    //POST 
                        {
                            int endOfNameIndex = 0;
                            for (int i = response.IndexOf('/'); i < response.Length; i++)
                            {
                                if (response[i] == ' ')
                                {
                                    endOfNameIndex = i - response.IndexOf('/');     
                                    break;
                                }
                            }
                            string name = response.Substring(response.IndexOf('/') + 1, endOfNameIndex).Trim();
                            string location = response.Substring(response.LastIndexOf("\r\n\r\n")).Trim(); 
                            int length = location.Trim().Length;
                            if (!details.ContainsKey(name))
                            {
                                details.TryAdd(name, location);
                                Console.Write("HTTP/1.0 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                                sw.Write("HTTP/1.0 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                                sw.Flush();
                                fs.WriteLine("HTTP/1.0 200 OK\r\nContent-Type: text/plain\r\n\r\n");       
                                fs.Flush();
                            }
                            else
                            {
                                details[name] = location;                                                                   //updates the location
                                Console.Write("HTTP/1.0 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                                sw.Write("HTTP/1.0 200 OK\r\nContent-Type: text/plain\r\n\r\n");       
                                sw.Flush();
                                fs.WriteLine("HTTP/1.0 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                                fs.Flush();
                            }
                        }
                    }
                    else if (response.Contains("HTTP/1.1"))
                    {
                        if (response.StartsWith("GET"))
                        {
                            string name = response.Substring(response.IndexOf('=') + 1, response.IndexOf("HTTP") - response.IndexOf('=') - 2);
                            if (details.ContainsKey(name))
                            {
                                Console.Write("HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\n" + details[name] + "\r\n");
                                sw.Write("HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\n" + details[name] + "\r\n");      
                                sw.Flush();
                                fs.WriteLine("HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\n" + details[name] + "\r\n");
                                fs.Flush();
                            }
                            else
                            {
                                Console.Write("HTTP/1.1 404 Not Found\r\nContent-Type: text/plain\r\n\r\n");
                                sw.Write("HTTP/1.1 404 Not Found\r\nContent-Type: text/plain\r\n\r\n");    
                                sw.Flush();
                                fs.WriteLine("HTTP/1.1 404 Not Found\r\nContent-Type: text/plain\r\n\r\n");
                                fs.Flush();
                            }
                        }
                        else                                                                                                            //POST
                        {
                            string newResponse = response.Substring(response.IndexOf("name="));
                            string[] split = newResponse.Split('&');
                            string name = split[0].Substring(split[0].IndexOf('=') + 1);                                     //gets name and location
                            string location = split[1].Substring(split[1].IndexOf('=') + 1);
                            if (!details.ContainsKey(name))
                            {
                                details.TryAdd(name, location);
                                Console.Write("HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                                sw.Write("HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\n");       
                                sw.Flush();
                                fs.WriteLine("HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                                fs.Flush();
                            }
                            else                                                                                        //update 
                            {
                                details[name] = location;
                                Console.Write("HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                                sw.Write("HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\n");        //write to console, client, ui
                                sw.Flush();
                                fs.WriteLine("HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                                fs.Flush();
                            }
                        }
                    }
                    else                                                                                                                 //http0.9
                    {
                        if (response.StartsWith("GET"))
                        {
                            string name = response.Substring(response.IndexOf('/') + 1);                                            //gets name 
                            if (details.ContainsKey(name.Trim()))
                            {
                                Console.Write("HTTP/0.9 200 OK\r\nContent-Type: text/plain\r\n\r\n" + details[name.Trim()] + "\r\n");
                                sw.Write("HTTP/0.9 200 OK\r\nContent-Type: text/plain\r\n\r\n" + details[name.Trim()] + "\r\n");       //write to client, form
                                sw.Flush();
                                fs.WriteLine("HTTP/0.9 200 OK\r\nContent-Type: text/plain\r\n\r\n" + details[name.Trim()] + "\r\n");
                                fs.Flush();
                            }
                            else 
                            {
                                Console.Write("HTTP/0.9 404 Not Found\r\nContent-Type: text/plain\r\n\r\n");
                                sw.Write("HTTP/0.9 404 Not Found\r\nContent-Type: text/plain\r\n\r\n");                         //write to client, console, form
                                sw.Flush();
                                fs.WriteLine("HTTP/0.9 404 Not Found\r\nContent-Type: text/plain\r\n\r\n");
                                fs.Flush();
                            }
                        }
                        else                                                                                                            //PUT
                        {
                            string name = response.Substring(response.IndexOf('/') + 1, response.IndexOf("\r\n") - response.IndexOf('/') - 1); 
                            string location = response.Substring(response.IndexOf("\r\n")).Trim();
                            if (!details.ContainsKey(name))                                                                         //if name not there new one added
                            {
                                details.TryAdd(name, location);
                                Console.Write("HTTP/0.9 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                                sw.Write("HTTP/0.9 200 OK\r\nContent-Type: text/plain\r\n\r\n");                              //response from client, console, form
                                sw.Flush();
                                fs.WriteLine("HTTP/0.9 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                                fs.Flush();
                            }
                            else    
                            {
                                details[name] = location;
                                Console.Write("HTTP/0.9 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                                sw.Write("HTTP/0.9 200 OK\r\nContent-Type: text/plain\r\n\r\n");                     
                                sw.Flush();
                                fs.WriteLine("HTTP/0.9 200 OK\r\nContent-Type: text/plain\r\n\r\n");                    //outputs
                                fs.Flush();
                            }

                        }
                    }
                }
                else
                {
                    string[] words = response.Split(' ');
                    string name = words[0].Trim();                                                              //WHOIS handler
                    string multiWords = "";
                    for (int i = 1; i < words.Length; i++)
                    {
                        multiWords += words[i] + " ";
                    }
                    string location = multiWords.Trim();
                    if (words.Length == 1)
                    {
                     
                        if (details.ContainsKey(name))
                        {
                            Console.WriteLine(details[name] + "\r\n");
                            sw.WriteLine(details[name] + "\r\n");                                                      // write info
                            sw.Flush();
                            fs.WriteLine(details[name] + "\r\n");
                            fs.Flush();
                        }
                        else
                        { 
                            Console.WriteLine("ERROR: no entries found\r\n");
                            sw.WriteLine("ERROR: no entries found\r\n");
                            sw.Flush();
                            fs.WriteLine("ERROR: no entries found\r\n");
                            fs.Flush();
                        }
                    }
                    else if (words.Length > 1)
                    {
                                      
                        if (details.ContainsKey(name))
                        {
                           
                            details[name] = location;                                                                      //update to
                            Console.WriteLine("OK\r\n");
                            sw.WriteLine("OK\r\n");
                            sw.Flush();
                            fs.WriteLine("OK\r\n");
                            fs.Flush();
                        }
                        else
                        {
                            details.TryAdd(name, location);                                                                //add to
                            Console.WriteLine("OK\r\n");
                            sw.WriteLine("OK\r\n");
                            sw.Flush();
                            fs.WriteLine("OK\r\n");
                            fs.Flush();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Server error: " + e);
                fs.WriteLine("Server error: " + e);
                fs.Flush();
            }
            finally
            {
                socketStream.Close();
                connection.Close();
            }
        }
    }
}

