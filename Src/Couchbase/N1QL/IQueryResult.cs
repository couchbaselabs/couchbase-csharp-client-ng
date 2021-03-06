﻿using System.Collections.Generic;

namespace Couchbase.N1QL
{
    /// <summary>
    /// Interface for the results of a N1QL query.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQueryResult<T>
    {
        /// <summary>
        /// The resultset of the N1QL query.
        /// </summary>
        List<T> Rows { get; set; }

        Error Error { get; set; }

        bool Success { get; }
    }
}

#region [ License information          ]

/* ************************************************************
 *
 *    @author Couchbase <info@couchbase.com>
 *    @copyright 2014 Couchbase, Inc.
 *
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 *
 *        http://www.apache.org/licenses/LICENSE-2.0
 *
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 *
 * ************************************************************/

#endregion