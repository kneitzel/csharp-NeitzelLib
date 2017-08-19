using System;
using System.Data;

namespace Neitzel.Data
{
    /// <summary>
    /// Extensions for <see cref="IDataReader"/> based data readers.
    /// </summary>
    public static class DataReaderExtensions
    {
        /// <summary>
        /// Gets a boolean from a column of a reader.
        /// </summary>
        /// <param name="reader">Reader to read the data from.</param>
        /// <param name="columnName">Name of column to read the data from.</param>
        /// <returns></returns>
        public static bool GetBoolean(this IDataReader reader, string columnName)
        {
            return reader.GetBoolean(reader.GetOrdinal(columnName));
        }

        /// <summary>
        /// Gets a nullable Int32 from a column of a reader.
        /// </summary>
        /// <param name="reader">Reader to read the data from.</param>
        /// <param name="columnName">Name of column to read the data from.</param>
        /// <returns></returns>
        public static bool? GetNullableBoolean(this IDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? (bool?)null : reader.GetBoolean(ordinal);
        }

        /// <summary>
        /// Gets a byte from a column of a reader.
        /// </summary>
        /// <param name="reader">Reader to read the data from.</param>
        /// <param name="columnName">Name of column to read the data from.</param>
        /// <returns></returns>
        public static byte GetByte(this IDataReader reader, string columnName)
        {
            return reader.GetByte(reader.GetOrdinal(columnName));
        }

        /// <summary>
        /// Gets a nullable Int32 from a column of a reader.
        /// </summary>
        /// <param name="reader">Reader to read the data from.</param>
        /// <param name="columnName">Name of column to read the data from.</param>
        /// <returns></returns>
        public static byte? GetNullableByte(this IDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? (byte?)null : reader.GetByte(ordinal);
        }

        /// <summary>
        /// Gets a Int32 from a column of a reader.
        /// </summary>
        /// <param name="reader">Reader to read the data from.</param>
        /// <param name="columnName">Name of column to read the data from.</param>
        /// <returns></returns>
        public static Int32 GetInt(this IDataReader reader, string columnName)
        {
            return reader.GetInt32(reader.GetOrdinal(columnName));
        }

        /// <summary>
        /// Gets a nullable Int32 from a column of a reader.
        /// </summary>
        /// <param name="reader">Reader to read the data from.</param>
        /// <param name="columnName">Name of column to read the data from.</param>
        /// <returns></returns>
        public static Int32? GetNullableInt(this IDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? (Int32?)null : reader.GetInt32(ordinal);
        }

        /// <summary>
        /// Gets the string of a reader from the given column name.
        /// </summary>
        /// <param name="reader">Reader to read the column from.</param>
        /// <param name="columName">Name of column to read.</param>
        /// <returns></returns>
        public static string GetString(this IDataReader reader, string columName)
        {
            return reader.GetString(reader.GetString(columName));
        }

    }
}
