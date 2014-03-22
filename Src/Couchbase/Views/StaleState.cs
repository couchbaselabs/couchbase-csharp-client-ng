﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couchbase.Views
{
    /// <summary>
    /// Allow the results from a stale view to be used
    /// </summary>
    public enum StaleState
    {
        None,

        //Force a view update before returning data
        False, 
        //Allow stale views
        Ok,
        //Allow stale view, update view after it has been accessed
        UpdateAfter
    }

    internal static class StaleStateExtensions
    {
        public static string ToLowerString(this StaleState value)
        {
            var parsed = "false";
            switch (value)
            {
                case StaleState.False:
                    break;
                case StaleState.Ok:
                    parsed = "ok";
                    break;
                case StaleState.UpdateAfter:
                    parsed = "update_after";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("value");
            }
            return parsed;
        }
    }
}