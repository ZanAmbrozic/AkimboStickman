using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomFunctions
{
    public static class DateTimeExtensions
    {
        // Convert datetime to UNIX time
        public static string ToUnixTime(this DateTime dateTime)
        {
            DateTimeOffset dto = new DateTimeOffset(dateTime.ToUniversalTime());
            return dto.ToUnixTimeSeconds().ToString();
        }

        /// <summary>
        /// Converts datetime to UNIX time including miliseconds
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>UNIX time including miliseconds</returns>
        public static string ToUnixTimeMilliSeconds(this DateTime dateTime)
        {
            DateTimeOffset dto = new DateTimeOffset(dateTime.ToUniversalTime());
            return dto.ToUnixTimeMilliseconds().ToString();
        }
    }
}
