using ModManagerLib;
using System.Net;
using System.Net.WebSockets;
using System.Text.Json;

namespace ModManagerServer
{
    internal class Program 
    {
        static int ListenPort = 400;
        static string ServerPath = string.Empty;

        static void ParseArgs(string[] args)
        {
            for (int x = 0; x < args.Length; x++)
            {
                string arg = args[x];
                switch (arg.ToLower())
                {
                    case "--port":
                    case "-p":
                        int.TryParse(args[x + 1], out ListenPort);
                        break;

                    case "--dir":
                    case "-d":
                        ServerPath = args[x + 1];
                        break;

                }
            }
        }

        static void Main(string[] args)
        {
            ParseArgs(args);

            if (string.IsNullOrWhiteSpace(ServerPath))
            {
                ServerPath = Directory.GetCurrentDirectory();
            }
            if (ListenPort == 0)
            {
                ListenPort = 400;
            }

            using var server = new Server(ListenPort, ServerPath);
            server.Start();
            server.Refresh();

            while (true)
            {
                string cmd = Console.ReadLine();
                switch (cmd.ToLower())
                {
                    case "r":
                    case "refresh":
                        server.Refresh();
                        break;

                    case "exit":
                        server.Stop();
                        return;

                    case "cls":
                        Console.Clear();
                        break;

                }
            }
        }
    }
}
