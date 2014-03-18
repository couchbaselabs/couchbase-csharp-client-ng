﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase.Configuration.Client;
using Couchbase.Configuration.Server;
using Couchbase.Configuration.Server.Serialization;
using Couchbase.Core;

namespace Couchbase.Configuration
{
    internal class DefaultConfig : IConfigInfo
    {
        private readonly DateTime _creationTime = DateTime.Now;
        private readonly IServerConfig _serverConfig;
        private readonly ClientConfiguration _clientConfig;

        public DefaultConfig(ClientConfiguration clientConfig, IServerConfig serverConfig)
        {
            _clientConfig = clientConfig;
            _serverConfig = serverConfig;
        }

        public DefaultConfig(ClientConfiguration clientConfig)
        {
            _clientConfig = clientConfig;
        }

        public IBucketConfig BucketConfig { get; set; }

        public IServerConfig ServerConfig { get { return _serverConfig; } }

        public ClientConfiguration ClientConfig { get { return _clientConfig; } }
 
        public DateTime CreationTime { get { return _creationTime; } }

        public void Dispose()
        {
            throw new NotImplementedException();
        }


        public string BucketName
        {
            get { throw new NotImplementedException(); }
        }


        public Core.Buckets.BucketTypeEnum BucketType
        {
            get { throw new NotImplementedException(); }
        }


        public Core.Buckets.NodeLocatorEnum NodeLocator
        {
            get { throw new NotImplementedException(); }
        }


        public IKeyMapper GetKeyMapper(string bucketName)
        {
            throw new NotImplementedException();
        }


        IKeyMapper IConfigInfo.GetKeyMapper(string bucketName)
        {
            throw new NotImplementedException();
        }
    }
}