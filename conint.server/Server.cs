using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace conint.server
{
    public class Server
    {
        public static HttpListener server;
        public bool isLinux = false;
        public Server()
        {
            server = new HttpListener();
            CheckOSforPath();
        }
        public void AddPrefix()
        {
            if (isLinux)
            {
                server.Prefixes.Add(@"http://*:1300/server/");
            }
            else
            {
                server.Prefixes.Add(@"http://localhost:1300/server/");
            }
        }
        public void Start()
        {
            AddPrefix();
            server.Start();
        }
        public void Close()
        {
            server.Close();
        }
        public void CheckOSforPath()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                isLinux = true;
            }
        }
        public static async Task HandleConnections()
        {
            bool runserver = true;
            byte[] answerData;
            while (runserver)
            {
                //server wartet auf request
                HttpListenerContext context = await server.GetContextAsync();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                if ((request.HttpMethod == "GET") && (request.Url.AbsolutePath == "/server/"))
                {
                    //file machen und zu json convertieren
                    StockFile file = new StockFile("BTC-USD.csv");
                    file.createFilePath();
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        file.path = "./BTC-USD.csv";
                    }
                    string json = "not found";
                    if (file.CheckIfFileExists(file.path))
                    {
                        json = file.ConvertToJson();
                    }
                    answerData = Encoding.UTF8.GetBytes(json);
                    response.ContentType = "application/json";
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Headers.Add("Access-Control-Allow-Origin", "*");
                    response.ContentEncoding = Encoding.UTF8;
                    response.ContentLength64 = answerData.LongLength;
                }
                else
                {
                    answerData = Encoding.UTF8.GetBytes("");
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                }
                await response.OutputStream.WriteAsync(answerData, 0, answerData.Length);
                response.Close();
            }
        }
    }
}
