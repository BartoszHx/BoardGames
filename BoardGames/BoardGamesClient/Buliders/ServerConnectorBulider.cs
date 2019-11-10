using System;
using System.Collections.Generic;
using System.Text;
using BoardGamesClient.Servers;

namespace BoardGamesClient.Buliders
{
    internal class ServerConnectorBulider
    {
        private string host;
        private int portGameOnline;
        private int portUser;

        public ServerConnectorBulider Host(string host)
        {
            this.host = host;
            return this;
        }

        public ServerConnectorBulider PortGameOnline(int port)
        {
            this.portGameOnline = port;
            return this;
        }

        public ServerConnectorBulider PortUser(int port)
        {
            this.portUser = port;
            return this;
        }

        public ServerConnector Build()
        {
            this.validBuild();
            return new ServerConnector(this.host, this.portGameOnline, this.portUser);
        }

        private void validBuild()
        {
            if (this.host == null)
            {
                throw new Exception("Host not set");
            }

            if (this.portGameOnline <= 0)
            {
                throw new Exception("PortGameOnline not set");
            }

            if (this.portUser <= 0)
            {
                throw new Exception("PortUser not set");
            }
        }
    }
}
