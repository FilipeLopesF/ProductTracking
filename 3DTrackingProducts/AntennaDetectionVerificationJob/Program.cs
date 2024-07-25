using _3DTrackingProducts.Api.Dtos;
using _3DTrackingProducts.Api.Migrations;
using _3DTrackingProducts.Api.Models;
using _3DTrackingProducts.Api.Resources;
using _3DTrackingProducts.Api.Utils;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace _3DTrackingProducts.ConsoleApp
{
    class Program
    {
        static HttpClient client = new HttpClient();
        private static int miliseconds = 3 * 100000;
        private const int diff = 2;

        static async Task<List<RoomDto>> GetAllRooms()
        {
            List<RoomDto> rooms = new List<RoomDto>();
            HttpResponseMessage responseMessage = await client.GetAsync($"api/rooms/all");
            if (responseMessage.IsSuccessStatusCode)
            {
                rooms = await responseMessage.Content.ReadAsAsync<List<RoomDto>>();
            }
            else
            {
                Console.WriteLine($"ERROR GET ALL ROOMS - {responseMessage.Content.ReadAsStringAsync().Result}");
            }
            return rooms;
        }

        static async Task<List<PairAntennaDto>> GetPairAntennasByRoomId(Guid id)
        {
            List<PairAntennaDto> pairAntennas = new List<PairAntennaDto>();
            HttpResponseMessage responseMessage = await client.GetAsync($"api/rooms/{id}/antennas");
            if (responseMessage.IsSuccessStatusCode)
            {
                pairAntennas = await responseMessage.Content.ReadAsAsync<List<PairAntennaDto>>();
            }
            else
            {
                Console.WriteLine($"ERROR GET PAIR ANTENNAS BY ROOM ID - {responseMessage.Content.ReadAsStringAsync().Result}");
            }
            return pairAntennas;
        }

        static async Task<List<ControlTagDto>> GetControllTagsByRoomId(string roomId)
        {
            List<ControlTagDto> pairAntennas = new List<ControlTagDto>();
            HttpResponseMessage responseMessage = await client.GetAsync($"api/controlTag/{roomId}");
            if (responseMessage.IsSuccessStatusCode)
            {
                pairAntennas = await responseMessage.Content.ReadAsAsync<List<ControlTagDto>>();
            }
            else
            {
                Console.WriteLine($"ERROR GET CONTROLL TAGS BY ROOM ID - {responseMessage.Content.ReadAsStringAsync().Result}");
            }
            return pairAntennas;
        }

        static async Task<CalculatePositionDto?> CalculatePosition(string epc, Guid pairAntennaId)
        {
            HttpResponseMessage responseMessage = await client.GetAsync($"api/controlTag/{epc}/antennas/{pairAntennaId}/position");
            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadAsAsync<CalculatePositionDto>();
            }
            else
            {

                Console.WriteLine($"ERROR GET CONTROLL TAG POSITION - {responseMessage.Content.ReadAsStringAsync().Result}");

            }

            return null;
        }

        static async void UpdateAntenna(Guid id, PairAntennaDetectionResource pairAntennaDetectionResource)
        {
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync($"api/antennas/{id}/detection", pairAntennaDetectionResource);
            if (responseMessage.IsSuccessStatusCode)
            {
                Console.WriteLine($"UPDATING DECTECTION INFO PAIR ANTENNA");
            }
            else
            {

                Console.WriteLine($"ERROR UPDATING DETECTION INFO PAIR ANTENNA - {responseMessage.Content.ReadAsStringAsync().Result}");

            }
        }

        static bool isWithinRange(CalculatePositionDto calculatePositionDto, ControlTagDto controlTagDto)
        {
            return (calculatePositionDto.locX >= controlTagDto.PositionX - diff && calculatePositionDto.locX <= controlTagDto.PositionX + diff)
                    && (calculatePositionDto.locY >= controlTagDto.PositionY - diff && calculatePositionDto.locY <= controlTagDto.PositionY + diff); 
        }

        static void Main()
        {
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

            Console.WriteLine("Beginning TASK ANTENNA DETECTION VERIFICATION");
            try
            {
                List<RoomDto> rooms = await GetAllRooms();
                if (rooms.IsNullOrEmpty())
                {
                    Console.WriteLine("No rooms registered");
                }
                else
                {
                    foreach (RoomDto room in rooms)
                    {
                        List<PairAntennaDto> pairAntennas = await GetPairAntennasByRoomId(room.Id);
                        if (pairAntennas.IsNullOrEmpty())
                        {
                            Console.WriteLine($"No pairAntenna registered for room id: {room.Id}");
                        }
                        else
                        {
                            foreach (PairAntennaDto pairAntenna in pairAntennas)
                            {

                                List<ControlTagDto> controlTags = await GetControllTagsByRoomId(room.Id.ToString());

                                bool detectedControlTag = false;
                                bool detectedSuccessfull = false;

                                foreach (ControlTagDto controlTag in controlTags)
                                {
                                    Console.WriteLine($"PairAntenna {pairAntenna.Id} ipAddress01: {pairAntenna.antenna01IP}; ipAddress02: {pairAntenna.antenna02IP}");

                                    CalculatePositionDto? calculatePosition = await CalculatePosition(controlTag.Epc, pairAntenna.Id);

                                    if (calculatePosition != null)
                                    {
                                        detectedControlTag = true;
                                        detectedSuccessfull = isWithinRange(calculatePosition, controlTag); 
                                    }
                                }

                                PairAntennaDetectionResource pairAntennaDetectionResource = new PairAntennaDetectionResource
                                {
                                    antenna01IP = pairAntenna.antenna01IP,
                                    antenna01X = pairAntenna.antenna01X,
                                    antenna01Y = pairAntenna.antenna01Y,
                                    antenna02IP = pairAntenna.antenna02IP,
                                    antenna02X = pairAntenna.antenna02X,
                                    antenna02Y = pairAntenna.antenna02Y,
                                    idRoom = room.Id,
                                    DetectingState = (detectedControlTag) ? (detectedSuccessfull ? 2 : 1) : 0
                                };

                                UpdateAntenna(pairAntenna.Id, pairAntennaDetectionResource);
                            }
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
    }
}