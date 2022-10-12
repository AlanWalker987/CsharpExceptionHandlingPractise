using System;
using System.IO;
using System.Runtime.Serialization;

namespace CsharpExceptionHandling
{
    public class ExceptionBasics
    {
        public void ExceptionDemo()
        {
                StreamReader streamReader = null;
                try
                {
                    streamReader = new StreamReader(@"D:\ExceptionHandlingDemoFolder\readtext.txt");
                    Console.WriteLine(streamReader.ReadToEnd());
                    Console.ReadLine();
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine("Please check if the file {0} exists", ex.FileName);
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    if (streamReader != null)
                    {
                        streamReader.Close();
                    }
                    Console.WriteLine("Finally Block");
                }
        }
    }

    public class InnerExceptionBasics
    {
        public void InnerExceptionDemo()
        {
            try
            {
                try
                {
                    Console.WriteLine("Enter the first number");
                    int firtNumber = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter the second number");
                    int secondNumber = int.Parse(Console.ReadLine());

                    int result = firtNumber / secondNumber;

                    Console.WriteLine("Result is {0}", result);
                }
                catch (Exception ex)
                {
                    string filepath = @"D:\ExceptionHandlingDemoFolder\log1.txt";
                    if (File.Exists(filepath))
                    {
                        StreamWriter streamWriter = new StreamWriter(filepath);
                        streamWriter.Write(ex.GetType().Name);
                        streamWriter.WriteLine();
                        streamWriter.Write(ex.Message);
                        streamWriter.Close();
                        Console.WriteLine("There is a problem please try later or else check the log here {0}", filepath);
                    }
                    else
                    {
                        throw new FileNotFoundException(filepath + "is not present", ex);
                    }
                }
            }catch(Exception exception)
            {
                Console.WriteLine("Current Exception - {0}", exception.GetType().Name);
                Console.WriteLine();
                Console.WriteLine("Inner Exception - {0}", exception.InnerException.GetType().Name);
            }
            
           // InnerException property returns the exception instance that caused the current exception
           // To retain the original exception pass it as a parameter to the constructor of the current exception
           // always check if inner exception is null or not otherwise it will get nullreference exception
           // to get the type of innerexception use getType method and use Name property to get the name of the exception
        }
    }

    [Serializable]
    public class CustomException : Exception
    {
        public CustomException() : base() { }

        public CustomException(string message) : base(message) { }

        public CustomException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    public class Program
    {
       
        static void Main(string[] args) {

            /* Exception Basics 
            ExceptionBasics exceptionBasics = new ExceptionBasics();
            exceptionBasics.ExceptionDemo();
            */

            /* Inner Exception Basics
              InnerExceptionBasics innerExceptionBasics = new InnerExceptionBasics();
               innerExceptionBasics.InnerExceptionDemo();
              Console.ReadLine();
            */

            // Custom Exceptions
            try
            {
                throw new CustomException("Unable to fetch the data from the json");
            }catch(CustomException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}

// Notes on Exception Handling

// An exception is an unforeseen error that occurs when a program is running
// Examples
// trying to read a file that does not exist, throws FileNotFoundException
// trying to read from a database table that does not exist, throws SqlException

// More specific exception on the top and general exception on the bottom - otherwise will get a compiler error

// we use try catch and finally blocks for exception handling
// try - code that possibly causes an exception will be in try block
// catch - handles the exception
// finally - cleans and frees the resources that the class was holding on to during the program execution
// finally block is optional

// Finally block will execute irrespective of exception or not so it is always good to release resources in the finally block

