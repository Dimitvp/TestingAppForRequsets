using ConsoleAppTestingRequests;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Http;

using WebApplicationForTestingRequests.Services;

namespace WebApplicationForTestingRequests.Controllers
{
    public class TestRequestsController : ApiController
    {
        // Set up the timer for 5 seconds.
        private Timer timer = new Timer(5000);
        private readonly string filePath = Path.Combine("C:\\01. MyWork\\TestingRequestsToApp\\TestingAppForRequsets\\Output", "Output" + ".yaml");
       
        [HttpPost]
        public async Task<IHttpActionResult> TestingPostRequests(PostModel postModel)
        {
            var file = CreateOrTakeFile();
            StreamWriter writer = file.AppendText();
            try
            {
                var testingService = new TestRequestsService();

                testingService.StartTimer(timer);

                while (timer.Enabled)
                {
                    // Print results.
                    timer.Enabled = await testingService.PrintTimes(postModel, writer, timer);
                    // Wait 2 seconds.
                    await writer.WriteLineAsync("WAITING");
                    System.Threading.Thread.Sleep(2000);
                }
                await writer.WriteLineAsync("****************STOP with this shit******************");
                return Ok(postModel.Post);

            }
            catch (Exception ex)
            {
                var messageError = ex.Message;
                if (ex.InnerException != null)
                {
                    messageError += "; Inner=" + ex.InnerException.Message;
                }

                return Json(messageError);
            }
            finally
            {
                timer.Enabled = false;
                writer.Flush();
                writer.Close();
            }
           

        }

        [HttpGet]
        public IHttpActionResult TestingGetRequests()
        {
            try
            {
                string reader = File.ReadAllText(filePath);

                return Json(reader);
            }
            catch (Exception ex)
            {
                var messageError = ex.Message;
                if (ex.InnerException != null)
                {
                    messageError += "; Inner=" + ex.InnerException.Message;
                }

                return Json(messageError);
            }
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
    }
}