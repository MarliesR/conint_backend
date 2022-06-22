using System;
using Xunit;
using conint.server;
using System.IO;
using System.Runtime.InteropServices;
using System.Net;
using System.Threading.Tasks;

namespace conint.tests
{
    public class UnitTest1
    {
        [Fact]
        public void FilePath()
        {
            string filename = "BTC-USD.csv";
            StockFile file = new StockFile(filename);
            file.createFilePath();
            Assert.Equal(Directory.GetCurrentDirectory() + "\\" + filename, file.path);
        }

        [Fact]
        public void CheckFilePath_valid()
        {
            StockFile file = new StockFile("BTC-USD.csv");
            file.createFilePath();
            var filename = "./BTC-USD.csv";
            var test = $"filepath test check valid: {file.path}";
            bool filepathValid = file.CheckIfFileExists(filename);
            Assert.True(filepathValid, System.Reflection.Assembly.GetEntryAssembly().Location);
        }

        [Fact]
        public void CheckFilePath_invalid_fileDoesntExist()
        {
            StockFile file = new StockFile("BTC.csv");
            file.createFilePath();
            bool filepathValid = file.CheckIfFileExists(file.path);
            Assert.False(filepathValid);
            //Assert.True(filepathValid); //for failing test
        }

        [Fact]
        public void CheckJson_notEmpty()
        {
            StockFile file = new StockFile("BTC-USD.csv");
            file.path = "./BTC-USD.csv";
            string json = file.ConvertToJson();
            Assert.NotNull(json);
        }

        //SERVER tests
        [Fact]
        public void ServerPrefix_linux()
        {
            Server testserver = new Server();
            testserver.isLinux = true;
            testserver.AddPrefix();
            string testString = "";
            HttpListenerPrefixCollection prefixes = Server.server.Prefixes;
            foreach (string prefix in prefixes)
            {
                testString = prefix;
            }
            Assert.Equal("http://*:1300/server/", testString);
        }

        [Fact]
        public void ServerPrefix_windows()
        {
            Server testserver = new Server();
            testserver.isLinux = false;
            testserver.AddPrefix();
            string testString = "";
            HttpListenerPrefixCollection prefixes = Server.server.Prefixes;
            foreach (string prefix in prefixes)
            {
                testString = prefix;
            }
            Assert.Equal("http://localhost:1300/server/", testString);
        }
        [Fact]
        public void StopServer()
        {
            Server server = new Server();
            server.CheckOSforPath();
            server.Start();
            server.Close();
            Assert.NotNull(server);
        }

    }
}
