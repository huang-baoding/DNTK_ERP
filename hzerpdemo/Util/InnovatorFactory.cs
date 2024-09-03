using Aras.IOM;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace hzerpdemo.Util
{
    public class InnovatorFactory
    {
        private static IConfiguration config;
        public InnovatorFactory(IConfiguration configuration)
        {
            config = configuration;
        }
        private static HttpServerConnection _connection;
        public static Innovator GetInnovator()
        {
            if (_connection == null)
            {
                _connection = IomFactory.CreateHttpServerConnection(config["InnovatorUrl"],
                  config["InnovatorDatabase"],
                   config["InnovatorUserName"],
                   config["InnovatorPassword"]);
            }
            //var connection =
            //    IomFactory.CreateHttpServerConnection(config["InnovatorUrl"],
            //       config["InnovatorDatabase"],
            //        config["InnovatorUserName"],
            //        config["InnovatorPassword"]);
            //connection.Login();
            //return IomFactory.CreateInnovator(connection);

            _connection.Login();
            return IomFactory.CreateInnovator(_connection);
        }

        public static void Logout()
        {
            if (_connection != null)
            {
                _connection.Logout();
            }
        }
    }
}
