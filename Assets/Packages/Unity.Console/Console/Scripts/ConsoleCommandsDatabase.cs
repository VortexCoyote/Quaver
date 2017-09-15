﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Wenzil.Console
{
    /// <summary>
    /// Use RegisterCommand() to register your own commands. Registered commands persist between scenes but don't persist between multiple application executions.
    /// </summary>
    public static class ConsoleCommandsDatabase
    {
        private static Dictionary<string, ConsoleCommand> s_database = new Dictionary<string, ConsoleCommand>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Return all the commands in alphabetical order.
        /// </summary>
        public static IEnumerable<ConsoleCommand> commands { get { return s_database.OrderBy(kv => kv.Key).Select(kv => kv.Value); } }

        public static void RegisterCommand(string command, ConsoleCommandCallback callback, string description = "", string usage = "")
        {
            RegisterCommand(command, description, usage, callback);
        }

        public static void RegisterCommand(string command, string description, string usage, ConsoleCommandCallback callback)
        {
            s_database[command] = new ConsoleCommand(command, description, usage, callback);
        }

        public static string ExecuteCommand(string command, params string[] args)
        {
            try
            {
                ConsoleCommand retrievedCommand = GetCommand(command);
                return retrievedCommand.callback(args);
            }
            catch (NoSuchCommandException e)
            {
                return e.Message;
            }
        }

        public static bool TryGetCommand(string command, out ConsoleCommand result)
        {
            try
            {
                result = GetCommand(command);
                return true;
            }
            catch (NoSuchCommandException)
            {
                result = default(ConsoleCommand);
                return false;
            }
        }

        public static ConsoleCommand GetCommand(string command)
        {
            if (HasCommand(command))
            {
                return s_database[command];
            }
            else
            {
                command = command.ToUpper();
                throw new NoSuchCommandException("Command " + command + " not found.", command);
            }
        }

        public static bool HasCommand(string command)
        {
            return s_database.ContainsKey(command);
        }
    }
}