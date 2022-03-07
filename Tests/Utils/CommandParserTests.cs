using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace MyTelegramBot.Utils.Tests
{
    [TestClass()]
    public class CommandParserTests
    {
        [TestMethod()]
        public void Parse_CommandNoArguments_ThrowArgumentException()
        {
            // Arrange
            CommandParser parser = new CommandParser();

            // Act
            Action actual = new Action(() =>
            {
                parser.Parse("/help");
            });

            // Assert
            Assert.ThrowsException<ArgumentException>(actual);
        }
        [TestMethod()]
        public void Parse_CommandOneArgument_ReturnArrayOneArgument()
        {
            // Arrange
            CommandParser parser = new CommandParser();

            // Act
            ParsedCommand actual = parser.Parse("/echo 123");

            // Assert
            Assert.AreEqual("/echo", actual.CommandText);
            Assert.AreEqual("123", actual.ArgumentsText);
            Assert.AreEqual("123", actual.Arguments.First());
        }
        [TestMethod()]
        public void Parse_CommandTwoArguments_ReturnArrayTwoArguments()
        {
            // Arrange
            CommandParser parser = new CommandParser();

            // Act
            ParsedCommand actual = parser.Parse("/pay @Durov 10$");

            // Assert
            Assert.AreEqual("/pay", actual.CommandText);
            Assert.AreEqual("@Durov 10$", actual.ArgumentsText);
            Assert.AreEqual(2, actual.Arguments.Length);
            Assert.AreEqual("@Durov", actual.Arguments.First());
            Assert.AreEqual("10$", actual.Arguments.Last());
        }
        [TestMethod()]
        public void Parse_CommandTenArguments_ReturnArrayTenArguments()
        {
            // Arrange
            CommandParser parser = new CommandParser();

            // Act
            ParsedCommand actual = parser.Parse("/kick 1 2 3 4 5 6 7 8 9 10");

            // Assert
            Assert.AreEqual("/kick", actual.CommandText);
            Assert.AreEqual("1 2 3 4 5 6 7 8 9 10", actual.ArgumentsText);
            Assert.AreEqual(10, actual.Arguments.Length);
            Assert.AreEqual(
                "1 2 3 4 5 6 7 8 9 10",
                string.Join(" ", actual.Arguments)
            );
        }
        [TestMethod()]
        public void Parse_CommandTwoArgumentsSemiliconSplitKey_ReturnArrayArguments()
        {
            // Arrange
            CommandParser parser = new CommandParser();
            parser.SplitKey = ";";

            // Act
            ParsedCommand actual = parser.Parse("/get John Doe;FirstName Surname");

            // Assert
            Assert.AreEqual("/get", actual.CommandText);
            Assert.AreEqual("John Doe;FirstName Surname", actual.ArgumentsText);
            Assert.AreEqual(2, actual.Arguments.Length);
            Assert.AreEqual("John Doe", actual.Arguments.First());
            Assert.AreEqual("FirstName Surname", actual.Arguments.Last());
        }
        [TestMethod()]
        public void Parse_CommandTwoArgumentsEnterDivider_ReturnArrayArguments()
        {
            // Arrange
            CommandParser parser = new CommandParser();
            parser.Divider = "\n";

            // Act
            ParsedCommand actual = parser.Parse(
                "Say Me\n" +
                "hello world"
            );

            // Assert
            Assert.AreEqual("Say Me", actual.CommandText);
            Assert.AreEqual("hello world", actual.ArgumentsText);
            Assert.AreEqual(2, actual.Arguments.Length);
            Assert.AreEqual("hello", actual.Arguments.First());
            Assert.AreEqual("world", actual.Arguments.Last());
        }
        [TestMethod()]
        public void Validate_CommandOneArgumentHasOneArgument_True()
        {
            // Arrange
            CommandParser parser = new CommandParser();

            // Act
            bool actual = parser.Validate("/pay 1");

            // Assert
            Assert.IsTrue(actual);
        }
        [TestMethod()]
        public void Validate_CommandNoArgumentsHasOneArgument_False()
        {
            // Arrange
            CommandParser parser = new CommandParser();

            // Act
            bool actual = parser.Validate("/help");

            // Assert
            Assert.IsFalse(actual);
        }
        [TestMethod()]
        public void Validate_CommandTwoArgumentsHasTwoArguments_True()
        {
            // Arrange
            CommandParser parser = new CommandParser();

            // Act
            bool actual = parser.Validate("/pay @Durov 1", 2);

            // Assert
            Assert.IsTrue(actual);
        }
        [TestMethod()]
        public void Validate_CommandTwoArgumentsHasTwoArguments_False()
        {
            // Arrange
            CommandParser parser = new CommandParser();

            // Act
            bool actual = parser.Validate("/pay 1", 2);

            // Assert
            Assert.IsFalse(actual);
        }
        [TestMethod()]
        public void Validate_CommandTwoArgumentsHasOnePlusArguments_True()
        {
            // Arrange
            CommandParser parser = new CommandParser();
            parser.ValidateOptions = CommandParserValidateOptions.EqualsOrGreater;

            // Act
            bool actual = parser.Validate("/pay 1 2", 1);

            // Assert
            Assert.IsTrue(actual);
        }
        [TestMethod()]
        public void Validate_CommandTwoArgumentsHasOneMinusArguments_False()
        {
            // Arrange
            CommandParser parser = new CommandParser();
            parser.ValidateOptions = CommandParserValidateOptions.EqualsOrLess;

            // Act
            bool actual = parser.Validate("/pay 1 2", 1);

            // Assert
            Assert.IsFalse(actual);
        }
        [TestMethod()]
        public void Validate_CommandNoArgumentsHasOneMinusArgument_True()
        {
            // Arrange
            CommandParser parser = new CommandParser();
            parser.ValidateOptions = CommandParserValidateOptions.EqualsOrLess;

            // Act
            bool actual = parser.Validate("/pay", 1);

            // Assert
            Assert.IsFalse(actual);
        }
        [TestMethod()]
        public void Validate_CommandThreeArgumentsInOneFourArgumentsRange_True()
        {
            // Arrange
            CommandParser parser = new CommandParser();

            // Act
            bool actual = parser.Validate("/buy @Durov 100 Bread", 1, 4);

            // Assert
            Assert.IsTrue(actual);
        }
        [TestMethod()]
        public void Validate_CommandOneArgumentsInTwoFourArgumentsRange_False()
        {
            // Arrange
            CommandParser parser = new CommandParser();

            // Act
            bool actual = parser.Validate("/pay 100", 2, 4);

            // Assert
            Assert.IsFalse(actual);
        }

    }
}