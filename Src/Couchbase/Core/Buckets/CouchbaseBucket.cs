﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Couchbase.Configuration;
using Couchbase.Configuration.Server.Providers;
using Couchbase.IO;
using Couchbase.IO.Operations;

namespace Couchbase.Core.Buckets
{
    public class CouchbaseBucket : IBucket, IConfigListener
    {
        private readonly ILog Log = LogManager.GetCurrentClassLogger();
        private readonly IClusterManager _clusterManager;
        private IConfigInfo _configInfo;
        private volatile bool _disposed;

        internal CouchbaseBucket(IClusterManager clusterManager)
        {
            _clusterManager = clusterManager;
        }

        internal CouchbaseBucket(IClusterManager clusterManager, string bucketName)
        {
            _clusterManager = clusterManager;
            Name = bucketName;
        }

        public string Name { get; set; }

        void IConfigListener.NotifyConfigChanged(IConfigInfo configInfo)
        {
            Interlocked.Exchange(ref _configInfo, configInfo);
        }

        public IOperationResult<T> Insert<T>(string key, T value)
        {
            var keyMapper = _configInfo.GetKeyMapper(Name);
            var vBucket = (IVBucket)keyMapper.MapKey(key);
            var server = vBucket.LocatePrimary();

            var operation = new SetOperation<T>(key, value, vBucket);
            var operationResult = server.Send(operation);

            if (CheckForConfigUpdates(operationResult))
            {
                Log.Debug(m => m("Requires retry {0}", key));
            }

            return operationResult;
        }

        public IOperationResult<T> Get<T>(string key)
        {
            var keyMapper = _configInfo.GetKeyMapper(Name);
            var vBucket = (IVBucket)keyMapper.MapKey(key);
            var server = vBucket.LocatePrimary();

            var operation = new GetOperation<T>(key, vBucket);
            var operationResult = server.Send(operation);

            if (CheckForConfigUpdates(operationResult))
            {
                Log.Debug(m => m("Requires retry {0}", key));
            }

            return operationResult;
        }

        bool CheckForConfigUpdates<T>(IOperationResult<T> operationResult)
        {
            var requiresRetry = false;
            if (operationResult.Status == ResponseStatus.VBucketBelongsToAnotherServer)
            {
                var bucketConfig = ((OperationResult<T>)operationResult).GetConfig();
                if (bucketConfig != null)
                {
                    _clusterManager.NotifyConfigPublished(bucketConfig);
                    requiresRetry = true;
                }
            }
            return requiresRetry;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _clusterManager.DestroyBucket(this);
                if (disposing)
                {
                    GC.SuppressFinalize(this);
                }
                _disposed = true;
            }
        }

        ~CouchbaseBucket()
        {
            Dispose(false);
        }
    }
}