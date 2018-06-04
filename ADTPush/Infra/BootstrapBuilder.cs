using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTPush.Infra
{
    public class BootstrapBuilder
    {
        private Action<ServerConfig> _configAction;

        public void ServerConfig(Action<ServerConfig> configAction)
        {
            _configAction = configAction;
        }

        public PushServer Build(int Port, int MaxConnect)
        {
            var config = DefaultConfig(Port, MaxConnect);

            _configAction?.Invoke(config);


            var server = new PushServer();
            server.Setup(config);

            return server;
        }

        protected virtual ServerConfig DefaultConfig(int port, int MaxConnect)
        {
            return new ServerConfig
            {
                Name = "ADT PUSH SERVER",
                Ip = "192.168.0.5",
                Port = port,
                Mode = SocketMode.Tcp,
                MaxConnectionNumber = MaxConnect
            };
        }

    }
}
