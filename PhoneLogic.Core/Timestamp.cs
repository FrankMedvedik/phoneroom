using System;
// The namespace can be overidden by the /N option:
// GenerateTimeStampFile file.cs /N:MyNameSpace
// Such settings will override your value here.
namespace PhoneLogic.Core
    {
    /// <summary>
    /// Static Timestamp related data.
    /// </summary>
    /// <remarks>
    /// THIS FILE IS CHANGED BY EXTERNAL PROGRAMS.
    /// Do not modify the namespace, as it may be overwritten. You can
    ///    set the namespace with the /N option.
    /// Do not modify the definition of BuildAt as your changes will be discarded.
    /// Do not modify the definition of TimeStampedBy as your changes will be discarded.
    /// </remarks>
    public static class Timestamp
        {
        /// <summary>
        /// The time stamp
        /// </summary>
        /// <remarks>
        /// Do not modify the definition of BuildAt as your changes will be discarded.
        /// </remarks>
        public static DateTime BuildAt { get { return new DateTime(635725840566606320); } } //--**
        /// <summary>
        /// The program that time stamped it.
        /// </summary>
        /// <remarks>
        /// Do not modify the definition of TimeStampedBy as your changes will be discarded.
        /// </remarks>
        public static string TimeStampedBy { get { return @"GenerateTimeStampFile V1.0"; } } //--**
        }
    }
