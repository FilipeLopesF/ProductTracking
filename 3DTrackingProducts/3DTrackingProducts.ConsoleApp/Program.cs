using _3DTrackingProducts.Api.Dtos;
using _3DTrackingProducts.Api.Models;
using _3DTrackingProducts.Api.Resources;
using _3DTrackingProducts.Api.Utils;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;

namespace _3DTrackingProducts.ConsoleApp
{
    class Program
    {
        static HttpClient client = new HttpClient();
        private static int miliseconds = 60000;
        private const uint ENABLE_EXTENDED_FLAGS = 0x0080;

        static async Task<List<TagDto>> GetAllTags()
        {
            List<TagDto> tags = new List<TagDto>();
            HttpResponseMessage responseMessage = await client.GetAsync("api/tags/all");
            if (responseMessage.IsSuccessStatusCode)
            {
                tags = await responseMessage.Content.ReadAsAsync<List<TagDto>>();
            }
            else
            {
                Console.WriteLine($"ERROR GET ALL TAGS - {responseMessage.Content.ReadAsStringAsync().Result}");
            }
            return tags;
        }

        static async void AddTagPosition(TagPositionResource tagPosition)
        {
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync($"api/tags/positions", tagPosition);
            if (responseMessage.IsSuccessStatusCode)
            {
                Console.WriteLine(responseMessage.ToString());
            }
            else
            {
                Console.WriteLine($"ERROR ADD TAG POSITION- {responseMessage.ToString()}");
            }
        }

        static async Task<CalculatePositionDto?> CalculatePosition(string epc)
        {
            HttpResponseMessage responseMessage = await client.GetAsync($"api/tags/{epc}/position");
            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadAsAsync<CalculatePositionDto>();
            }
            else
            {

                Console.WriteLine($"ERROR GET TAG POSITION - {responseMessage.Content.ReadAsStringAsync().Result}");

            }

            return null;  
        }


        static void Main()
        {
            DisableQuickEditMode();
            client.BaseAddress = new Uri("https://localhost:7168/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            while (true)
            {
                RunAsync().GetAwaiter().GetResult();
                Thread.Sleep(miliseconds);
            }
        }

        static async Task RunAsync()
        {
            
            Console.WriteLine("Begining Task TAG POSITION CALCULATION");
            try
            {

                List<TagDto> tags = await GetAllTags();
                if (tags.IsNullOrEmpty())
                {
                    Console.WriteLine("No tags registered");
                }
                else
                {
                    foreach (TagDto tag in tags)
                    {
                        Console.WriteLine($"Tag with epc: {tag.Epc}");

                        CalculatePositionDto? calculatePosition = await CalculatePosition(tag.Epc);

                        if (calculatePosition != null)
                        {
                            TagPositionResource tagPosition = new TagPositionResource
                            {
                                TagEPC = tag.Epc,
                                x = calculatePosition.locX,
                                y = calculatePosition.locY,
                                angleAntenna01 = calculatePosition.angleAntenna01,
                                angleAntenna02 = calculatePosition.angleAntenna02,
                                ParAntennaId = calculatePosition.pairAntenna.Id
                            };

                            AddTagPosition(tagPosition);
                        }
                    }
                }

                Console.WriteLine("End Task");
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        [DllImport("kernel32.dll")]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);


        private static void DisableQuickEditMode()
        {

            IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
            SetConsoleMode(handle, ENABLE_EXTENDED_FLAGS);
        }
    }
}