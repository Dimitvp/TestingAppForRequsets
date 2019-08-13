using ConsoleAppTestingRequests;
using System.IO;
using System.Web.Http;
using System.Timers;
using System;
using System.Collections.Generic;

namespace WebApplicationForTestingRequests.Controllers
{
    public class TestRequestsController : ApiController
    {
        private readonly string filePath = Path.Combine("C:\\01. MyWork\\TestingRequestsToApp\\Output", "Output" + ".yaml");
        private static Timer _timer;
        private static List<DateTime> _results = new List<DateTime>();

        [HttpPost]
        public IHttpActionResult TestingPostRequests(PostModel postModel)
        {
            var file = CreateOrTakeFile();

            StreamWriter writer = file.AppendText();

            writer.WriteLine(postModel.NumberOfRequest + ". " + postModel.Post);
            writer.Flush();
            writer.Close();

            //Start();
            //while (true)
            //{
            //    // Print results.
            //    PrintTimes();
            //    // Wait 2 seconds.
            //    Console.WriteLine("WAITING");
            //    System.Threading.Thread.Sleep(2000);
            //}

            return Ok(postModel.Post);
        }

        [HttpGet]
        public IHttpActionResult TestingGetRequests()
        {
            var file = CreateOrTakeFile();

            var getModel = new GetModel()
            {
                Get = "I am getting."
            };

            StreamWriter writer = file.AppendText();

            writer.WriteLine(getModel.Get);
            writer.Flush();
            writer.Close();


            return Ok();
        }

        private FileInfo CreateOrTakeFile()
        {
            FileInfo fi = new FileInfo(filePath);

            // This text is added only once to the file.
            if (!fi.Exists)
            {
                //Create a file to write to.
                using (StreamWriter sw = fi.CreateText())
                {
                    sw.WriteLine("Hello");
                    sw.WriteLine("And");
                    sw.WriteLine("Welcome");
                }
            }

            return fi;
        }

        public static void Start()
        {
            // Set up the timer for 3 seconds.
            var timer = new Timer(3000);
            // To add the elapsed event handler:
            // ... Type "_timer.Elapsed += " and press tab twice.
            timer.Elapsed += new ElapsedEventHandler(_timer_Elpased);
            timer.Enabled = true;
            _timer = timer;
        }

        public static void _timer_Elpased(object source, ElapsedEventArgs e)
        {
            // Add DateTime for each timer event.
            _results.Add(DateTime.Now);

        }

        public void PrintTimes()
        {
            // Print all the recorded times from the timer.
            if (_results.Count > 0)
            {
                Console.WriteLine("TIMES:");
                foreach (var time in _results)
                {
                    Console.Write(time.ToShortTimeString() + " ");
                }
                Console.WriteLine();
            }
        }
    }
}