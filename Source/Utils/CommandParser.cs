
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTelegramBot.Utils
{
    /// <summary>
    /// Enum <c>CommandParserValidateOptions</c> describes the options to validate 
    /// arguments count.
    /// </summary>
    public enum CommandParserValidateOptions
    {
        /// <summary>
        /// The number of arguments must be equal to the specified one.
        /// </summary>
        None = 0,
        /// <summary>
        /// The number of arguments must be greater than or equal to the specified one.
        /// </summary>
        EqualsOrGreater = 1,
        /// <summary>
        /// The number of arguments must be less than or equal to the specified one.
        /// </summary>
        EqualsOrLess = 2,
    }
    /// <summary>
    /// Struct <c>ParsedCommand</c> describes command parse result.
    /// </summary>
    public struct ParsedCommand
    {
        /// <property>
        /// Property <c>Command</c> represents the command part of the text.
        /// </property>
        public string CommandText { get; set; }
        /// <property>
        /// Property <c>ArgumentsText</c> represents the arguments part of the text.
        /// </property>
        public string ArgumentsText { get; set; }
        /// <property>
        /// Property <c>Arguments</c> represents the collection of command arguments.
        /// </property>
        public string[] Arguments { get; set; }
    }
    /// <summary>
    /// Class <c>CommandParser</c> describes a string command parser with
    /// a bunch of configs.
    /// </summary>
    public class CommandParser
    {
        /// <value>
        /// Property <c>SplitKey</c> represents a separator between arguments.
        /// </value>
        public string SplitKey { get; set; }
        /// <value>
        /// Property <c>Divider</c> repressents a divider between command and 
        /// arguments in the text.
        /// </value>
        public string Divider { get; set; }
        /// <value>
        /// Property <c>ValidateOptions</c> represents a default validate options 
        /// preset.
        /// </value>
        public CommandParserValidateOptions ValidateOptions { get; set; }
        /// <summary>
        /// Creates a <c>CommandParser</c> with default <c>SplitKey</c> and <c>Divider</c>.
        /// </summary>
        public CommandParser(): this(" ") {}
        /// <summary>
        /// Creates a <c>CommandParser</c> with certain <c>SplitKey</c>, default <c>Divider</c>
        /// and default <c>ValidateOptions</c>.
        /// </summary>
        /// <param name="splitKey">separator between arguments</param>
        public CommandParser(string splitKey): this(splitKey, " ") {}
        /// <summary>
        /// Creates a <c>CommandParser</c> with default <c>SplitKey</c>, default <c>Divider</c>
        /// and certain <c>ValidateOptions</c>.
        /// </summary>
        /// <param name="validateOptions">default validate options preset</param>
        public CommandParser(CommandParserValidateOptions validateOptions): this(" ", " ", validateOptions) {}
        /// <summary>
        /// Creates a <c>CommandParser</c> with certain <c>SplitKey</c>, default 
        /// <c>Divider</c> and certain <c>ValidateOptions</c>
        /// </summary>
        /// <param name="splitKey">separator between arguments</param>
        /// <param name="validateOptions">default validate options preset</param>
        public CommandParser(string splitKey, CommandParserValidateOptions validateOptions): this(splitKey, " ", validateOptions) {}
        /// <summary>
        /// Creates a <c>CommandParser</c> with certain <c>SplitKey</c>, certain <c>Divider</c>
        /// and default <c>ValidateOptions</c>.
        /// </summary>
        /// <param name="splitKey">separator between arguments</param>
        /// <param name="divider">divider between command and argument in the text</param>
        public CommandParser(string splitKey, string divider): this(splitKey, divider, CommandParserValidateOptions.None) {}
        /// <summary>
        /// Creates a <c>CommandParser</c> with certain <c>SplitKey</c>, certain <c>Divider</c>
        /// and certain <c>ValidateOptions</c>.
        /// </summary>
        /// <param name="splitKey">separator between arguments</param>
        /// <param name="divider">divider between command and argument in the text</param>
        /// <param name="validateOptions">default validate options preset</param>
        public CommandParser(string splitKey, string divider, CommandParserValidateOptions validateOptions)
        {
            SplitKey = splitKey;
            Divider = divider;
            ValidateOptions = validateOptions;
        }
        /// <summary>
        /// Parses the message text and splits to command parts.
        /// </summary>
        /// <param name="text">command message text</param>
        /// <returns>Composed <c>ParsedCommand</c> with command parts.</returns>
        public ParsedCommand Parse(string text)
        {
            ParsedCommand parsedCommand = new ParsedCommand();
            string[] divided = text.Split(Divider, 2);
            if (divided.Length != 2)
            {
                throw new ArgumentException("There are no arguments in the text.", "text");
            }
            parsedCommand.CommandText = divided.First();
            parsedCommand.ArgumentsText = divided.Last();
            parsedCommand.Arguments = parsedCommand.ArgumentsText.Split(SplitKey);
            return parsedCommand;
        }
        /// <summary>
        /// Validates the message text as a command with arguments.
        /// </summary>
        /// <param name="text">command message text</param>
        /// <returns>Returns <c>true</c> if the message can be parsed, and <c>false</c> if not.</returns>
        public bool Validate(string text)
        {
            string[] divided = text.Split(Divider, 2);
            if (divided.Length != 2)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Validates the message text as a command with a certain number of arguments.
        /// </summary>
        /// <param name="text">command message text</param>
        /// <param name="argumentsCount">number of required command arguments</param>
        /// <returns>Returns <c>true</c> if the message can be parsed, and <c>false</c> if not.</returns>
        public bool Validate(string text, int argumentsCount)
        {
            return Validate(text, argumentsCount, ValidateOptions);
        }
        /// <summary>
        /// Validates the message text as a command with a certain number of arguments.
        /// </summary>
        /// <param name="text">command message text</param>
        /// <param name="argumentsCountMin">number of minimum required command arguments</param>
        /// <param name="argumentsCountMax">number of maximum required command arguments</param>
        /// <returns>Returns <c>true</c> if the message can be parsed, and <c>false</c> if not.</returns>
        public bool Validate(string text, int argumentsCountMin, int argumentsCountMax)
        {
            string[] divided = text.Split(Divider, 2);
            if (divided.Length != 2)
            {
                return false;
            }
            int argsLength = divided.Last().Split(SplitKey).Length;
            return argsLength >= argumentsCountMin && argsLength <= argumentsCountMax;
        }
        /// <summary>
        /// Validates the message text as a command with a certain number of arguments.
        /// </summary>
        /// <param name="text">command message text</param>
        /// <param name="argumentsCount">number of required command arguments</param>
        /// <param name="options">argument count limit type</param>
        /// <returns>Returns <c>true</c> if the message can be parsed, and <c>false</c> if not.</returns>
        public bool Validate(string text, int argumentsCount, CommandParserValidateOptions options)
        {
            string[] divided = text.Split(Divider, 2);
            if (divided.Length != 2)
            {
                return false;
            }
            string[] commandText = divided.Last().Split(SplitKey);
            return new Dictionary<CommandParserValidateOptions, bool>() 
            {
                {CommandParserValidateOptions.None, commandText.Length == argumentsCount },
                {CommandParserValidateOptions.EqualsOrGreater, commandText.Length >= argumentsCount },
                {CommandParserValidateOptions.EqualsOrLess, commandText.Length <= argumentsCount },
            }[options];
        }
    }
}
