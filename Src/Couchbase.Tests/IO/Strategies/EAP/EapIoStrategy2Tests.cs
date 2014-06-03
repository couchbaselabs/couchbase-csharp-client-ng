﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase.Configuration.Client;
using Couchbase.IO;
using Couchbase.IO.Operations;
using Couchbase.IO.Strategies;
using NUnit.Framework;

namespace Couchbase.Tests.IO.Strategies.EAP
{
    [TestFixture]
    public class EapIoStrategy2Tests
    {
        private DefaultIOStrategy _ioStrategy;
        private IConnectionPool<EapConnection> _connectionPool;
        //private IConnectionPool<SslConnection> _connectionPool;
        //private const string Address = "192.168.56.104:11207";
        private const string Address = "127.0.0.1:11210";

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var ipEndpoint = Couchbase.Core.Server.GetEndPoint(Address);
            var connectionPoolConfig = new PoolConfiguration {UseSsl = false};
            //_connectionPool = new ConnectionPool<SslConnection>(connectionPoolConfig, ipEndpoint);      
            _connectionPool = new ConnectionPool<EapConnection>(connectionPoolConfig, ipEndpoint); 
            _ioStrategy = new DefaultIOStrategy(_connectionPool);
        }

        [Test]
        public void TestGet()
        {
            for (int i = 0; i < 10; i++)
            {
                var operation = new ConfigOperation();
                var result = _ioStrategy.Execute(operation);
                Assert.IsTrue(result.Success);
                Assert.IsNotNull(result.Value);
                Console.WriteLine(result.Value.ToString());
            }
        }


        [TestFixtureTearDown]
        public void TearDown()
        {
            _connectionPool.Dispose();
        }
    }
}