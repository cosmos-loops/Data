﻿// Description: C# Extension Methods | Enhance the .NET Framework and .NET Core with over 1000 extension methods.
// Website & Documentation: https://csharp-extension.com/
// Issues: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License (MIT): https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: https://zzzprojects.com/
// Copyright © ZZZ Projects Inc. All rights reserved.

using System;
using System.Data;
using Cosmos;

namespace Npgsql
{
    public static partial class NpgsqlClientExtensions
    {
        public static T ExecuteScalarTo<T>(this NpgsqlConnection @this, string cmdText, NpgsqlParameter[] parameters, CommandType commandType, NpgsqlTransaction transaction)
        {
            using (var command = @this.CreateCommand())
            {
                command.CommandText = cmdText;
                command.CommandType = commandType;
                command.Transaction = transaction;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                return command.ExecuteScalar().CastTo<T>();
            }
        }

        public static T ExecuteScalarTo<T>(this NpgsqlConnection @this, Action<NpgsqlCommand> commandFactory)
        {
            using (NpgsqlCommand command = @this.CreateCommand())
            {
                commandFactory(command);

                return command.ExecuteScalar().CastTo<T>();
            }
        }

        public static T ExecuteScalarTo<T>(this NpgsqlConnection @this, string cmdText)
        {
            return @this.ExecuteScalarTo<T>(cmdText, null, CommandType.Text, null);
        }

        public static T ExecuteScalarTo<T>(this NpgsqlConnection @this, string cmdText, NpgsqlTransaction transaction)
        {
            return @this.ExecuteScalarTo<T>(cmdText, null, CommandType.Text, transaction);
        }

        public static T ExecuteScalarTo<T>(this NpgsqlConnection @this, string cmdText, CommandType commandType)
        {
            return @this.ExecuteScalarTo<T>(cmdText, null, commandType, null);
        }

        public static T ExecuteScalarTo<T>(this NpgsqlConnection @this, string cmdText, CommandType commandType, NpgsqlTransaction transaction)
        {
            return @this.ExecuteScalarTo<T>(cmdText, null, commandType, transaction);
        }

        public static T ExecuteScalarTo<T>(this NpgsqlConnection @this, string cmdText, NpgsqlParameter[] parameters)
        {
            return @this.ExecuteScalarTo<T>(cmdText, parameters, CommandType.Text, null);
        }

        public static T ExecuteScalarTo<T>(this NpgsqlConnection @this, string cmdText, NpgsqlParameter[] parameters, NpgsqlTransaction transaction)
        {
            return @this.ExecuteScalarTo<T>(cmdText, parameters, CommandType.Text, transaction);
        }

        public static T ExecuteScalarTo<T>(this NpgsqlConnection @this, string cmdText, NpgsqlParameter[] parameters, CommandType commandType)
        {
            return @this.ExecuteScalarTo<T>(cmdText, parameters, commandType, null);
        }
    }
}