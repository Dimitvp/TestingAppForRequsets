using ConsoleAppTestingRequests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Timers;

namespace WebApplicationForTestingRequests.Services
{
    public class TestRequestsService
    {
        private Timer _timer;
        private List<DateTime> _results = new List<DateTime>();
        private int counter = 1;

        public void StartTimer(Timer timer)
        {
            
            // To add the elapsed event handler:
            // ... Type "_timer.Elapsed += " and press tab twice.
            timer.Elapsed += new ElapsedEventHandler(_timer_Elpased);
            timer.Enabled = true;
            _timer = timer;
        }

        public async Task<bool> PrintTimes(PostModel postModel, StreamWriter writer, Timer timer)
        {
            postModel.NumberOfRequest++;
            await writer.WriteLineAsync(postModel.NumberOfRequest + ". " + postModel.Post);
            // Print all the recorded times from the timer.
            if (_results.Count > 0)
            {
                if (counter > 5)
                {
                    return timer.Enabled = false;
                }

                await writer.WriteLineAsync("TIMES: " + counter);

                foreach (var time in _results)
                {
                    await writer.WriteLineAsync(time.ToShortTimeString() + " ");
                }
                counter++;
            }
            return timer.Enabled;
        }

        private void _timer_Elpased(object source, ElapsedEventArgs e)
        {
            // Add DateTime for each timer event.
            _results.Add(DateTime.Now);

        }
    }
}