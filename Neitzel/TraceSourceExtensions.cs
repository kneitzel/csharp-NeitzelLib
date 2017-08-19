using System;
using System.Diagnostics;
using System.Globalization;

namespace Neitzel
{
    /// <summary>
    /// Extensions to TraceSource
    /// </summary>
    public static class TraceSourceExtensions
    {
        #region Critical 

        /// <summary>
        /// Log a critical event
        /// </summary>
        /// <param name="source">TraceSource to log the event to.</param>
        /// <param name="id">Id to use.</param>
        /// <param name="exception">Exception, can be null.</param>
        /// <param name="message">Message</param>
        /// <param name="args">Arguments</param>
        public static void Critical(this TraceSource source, int id, Exception exception, string message, params object[] args)
        {
            // Validate parameter
            if (source == null) throw new ArgumentNullException(nameof(source));

            //  create message and trace it.
            message = CreateMessage(exception, message, args);
            source.TraceEvent(TraceEventType.Critical, id, message);
        }

        /// <summary>
        /// Log a critical event
        /// </summary>
        /// <param name="source">TraceSource to log the event to.</param>
        /// <param name="id">Id to use.</param>
        /// <param name="message">Message</param>
        /// <param name="args">Arguments</param>
        public static void Critical(this TraceSource source, int id, string message, params object[] args)
        {
            source.Critical(id, null, message, args);
        }

        #endregion

        #region Error 

        /// <summary>
        /// Log an error event
        /// </summary>
        /// <param name="source">TraceSource to log the event to.</param>
        /// <param name="id">Id to use.</param>
        /// <param name="exception">Exception, can be null.</param>
        /// <param name="message">Message</param>
        /// <param name="args">Arguments</param>
        public static void Error(this TraceSource source, int id, Exception exception, string message, params object[] args)
        {
            // Validate parameter
            if (source == null) throw new ArgumentNullException(nameof(source));

            //  create message and trace it.
            message = CreateMessage(exception, message, args);
            source.TraceEvent(TraceEventType.Error, id, message);
        }

        /// <summary>
        /// Log an error event
        /// </summary>
        /// <param name="source">TraceSource to log the event to.</param>
        /// <param name="id">Id to use.</param>
        /// <param name="message">Message</param>
        /// <param name="args">Arguments</param>
        public static void Error(this TraceSource source, int id, string message, params object[] args)
        {
            source.Error(id, null, message, args);
        }

        #endregion

        #region Warning 

        /// <summary>
        /// Log a warning event
        /// </summary>
        /// <param name="source">TraceSource to log the event to.</param>
        /// <param name="id">Id to use.</param>
        /// <param name="exception">Exception, can be null.</param>
        /// <param name="message">Message</param>
        /// <param name="args">Arguments</param>
        public static void Warning(this TraceSource source, int id, Exception exception, string message, params object[] args)
        {
            // Validate parameter
            if (source == null) throw new ArgumentNullException(nameof(source));

            //  create message and trace it.
            message = CreateMessage(exception, message, args);
            source.TraceEvent(TraceEventType.Warning, id, message);
        }

        /// <summary>
        /// Log a warning event
        /// </summary>
        /// <param name="source">TraceSource to log the event to.</param>
        /// <param name="id">Id to use.</param>
        /// <param name="message">Message</param>
        /// <param name="args">Arguments</param>
        public static void Warning(this TraceSource source, int id, string message, params object[] args)
        {
            source.Warning(id, null, message, args);
        }

        #endregion

        #region Info 

        /// <summary>
        /// Log a informational event
        /// </summary>
        /// <param name="source">TraceSource to log the event to.</param>
        /// <param name="id">Id to use.</param>
        /// <param name="exception">Exception, can be null.</param>
        /// <param name="message">Message</param>
        /// <param name="args">Arguments</param>
        public static void Info(this TraceSource source, int id, Exception exception, string message, params object[] args)
        {
            // Validate parameter
            if (source == null) throw new ArgumentNullException(nameof(source));

            //  create message and trace it.
            message = CreateMessage(exception, message, args);
            source.TraceEvent(TraceEventType.Information, id, message);
        }

        /// <summary>
        /// Log a informational event
        /// </summary>
        /// <param name="source">TraceSource to log the event to.</param>
        /// <param name="id">Id to use.</param>
        /// <param name="message">Message</param>
        /// <param name="args">Arguments</param>
        public static void Info(this TraceSource source, int id, string message, params object[] args)
        {
            source.Info(id, null, message, args);
        }

        #endregion

        #region Verbose 

        /// <summary>
        /// Log a verbose event
        /// </summary>
        /// <param name="source">TraceSource to log the event to.</param>
        /// <param name="id">Id to use.</param>
        /// <param name="exception">Exception, can be null.</param>
        /// <param name="message">Message</param>
        /// <param name="args">Arguments</param>
        public static void Verbose(this TraceSource source, int id, Exception exception, string message, params object[] args)
        {
            // Validate parameter
            if (source == null) throw new ArgumentNullException(nameof(source));

            //  create message and trace it.
            message = CreateMessage(exception, message, args);
            source.TraceEvent(TraceEventType.Verbose, id, message);
        }

        /// <summary>
        /// Log a verbose event
        /// </summary>
        /// <param name="source">TraceSource to log the event to.</param>
        /// <param name="id">Id to use.</param>
        /// <param name="message">Message</param>
        /// <param name="args">Arguments</param>
        public static void Verbose(this TraceSource source, int id, string message, params object[] args)
        {
            source.Verbose(id, null, message, args);
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Create the message from exception and original message.
        /// </summary>
        /// <param name="exception">Exception of this message (can be null)</param>
        /// <param name="message">Original message.</param>
        /// <param name="args">Arguments.</param>
        /// <returns></returns>
        private static string CreateMessage(Exception exception, string message, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, message, args) + (exception == null ? "" : " : " + exception.GetFullMessage());
        }

        #endregion
    }
}
