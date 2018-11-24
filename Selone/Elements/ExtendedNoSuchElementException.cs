using System;
using OpenQA.Selenium;

namespace Kontur.Selone.Elements
{
    public class ExtendedNoSuchElementException : NoSuchElementException
    {
        public ExtendedNoSuchElementException()
        {
        }

        public ExtendedNoSuchElementException(string elementDescription, string failedDescription, NoSuchElementException innerException)
            : base($"\nElement location:\n{elementDescription}\n\nSearch log:\n{failedDescription}", innerException)
        {
        }

        public ExtendedNoSuchElementException(string elementDescription, NoSuchElementException innerException)
            : base($"\nElement location:\n{elementDescription}\n", innerException)
        {
        }

        public ExtendedNoSuchElementException(string message)
            : base(message)
        {
        }

        public ExtendedNoSuchElementException(string message, Exception innerException)
            : base(GetMessage(message, innerException), GetGetException(innerException))
        {
            Reason = innerException is ExtendedNoSuchElementException e ? e.Reason : message;
        }

        public ExtendedNoSuchElementException(string me, string reason, Exception innerException)
            : base($"\nElement location:\n{me}\n\nSearch log:\n{reason}", innerException)
        {
            Reason = reason;
            Me = me;
        }

        public string Reason { get; }
        public string Me { get; }

        private static string GetMessage(string message, Exception exception)
        {
            return exception is ExtendedNoSuchElementException e
                ? $"\nElement location:\n{message}\n\nSearch log:\n{e.Reason}"
                : $"\nElement location:\n{message}";
        }

        private static Exception GetGetException(Exception exception)
        {
            return exception is ExtendedNoSuchElementException e ? e.InnerException : exception;
        }
    }
}