using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neitzel
{
    /// <summary>
    /// Extensions to the Exception class.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Get the full message of an exception as string.
        /// </summary>
        /// <param name="exception">Exception that should be shown.</param>
        /// <returns>String that includes the whole exception.</returns>
        public static string GetFullMessage(this Exception exception)
        {
            // Validate parameter
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            // Message with inner exception
            if (exception.InnerException == null)
            {
                return string.Format(CultureInfo.InvariantCulture, "{0} {1}", exception.Message, exception.StackTrace);
            }

            // Message without inner exception
            return string.Format(CultureInfo.InvariantCulture, "{0} {1} ({2})", exception.Message, exception.StackTrace, exception.InnerException.GetFullMessage());
        }
    }
}
