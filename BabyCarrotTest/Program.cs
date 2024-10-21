using BabyCarrot.Tools;
using BabyCarrot.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyCarrotTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //LogManager log = new LogManager(null, "_test");

            //log.WriteLine("[Begin Processng]...");
            //for(int index = 0; index < 10;index++)
            //{
            //    log.WriteLine("Processing:" + index);

            //    System.Threading.Thread.Sleep(500);

            //    log.WriteLine("Done:" + index);
            //}
            //log.WriteLine("[End Processng]...");

            //log.WriteConsole("Extension Test");

            string temp = "1";
            Console.WriteLine("IsNumeric? : {0}",temp.IsNumeric());
            Console.WriteLine("IsDateTime? : {0}", temp.IsDateTime());
            DateTime d = DateTime.Now;
            Console.WriteLine(d.LastDateOfMonth());

            string content = "Hello, world <br/> this is alex";
            EmailManager.Send("receiver@gmail.com", "test", content);

            var emailMgr = new EmailManager("", 25, "id", "pwd");

        }

    }

    public static class ExtensionLog
    {
        public static void WriteConsole(this LogManager log, string data)
        {
            log.Write(data);
            Console.Write(data);
        }
    }
}
