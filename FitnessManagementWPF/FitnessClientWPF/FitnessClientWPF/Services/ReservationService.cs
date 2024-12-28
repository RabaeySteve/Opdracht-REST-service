using FitnessClientWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static FitnessClientWPF.Model.Reservation;

namespace FitnessClientWPF.Services {
    public class ReservationService {

        private HttpClient client;
        public ReservationService() {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5054/");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async Task<ReservationPost> PostReservationAsync(ReservationPost reservation) {
            try {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/Reservation", reservation);

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<ReservationPost>();
            } catch (Exception ex) {
                Console.WriteLine($"Error posting reservation: {ex.Message}");
                MessageBox.Show(ex.ToString()); return null;
            }
        }
        public async Task<Dictionary<int, List<Equipment>>> GetAvailableTimeSlotsAsync(string date) {
            try {
                HttpResponseMessage response = await client.GetAsync($"api/Reservation/{date}/AvailableTimeSlots");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Dictionary<int, List<Equipment>>>();
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.ToString()); return null;
            }
        }

        public async Task<List<ReservationGetDTO>> GetMemberDateAsync(int id, string date) {
            try {
                HttpResponseMessage response = await client.GetAsync($"/api/Reservation/{id}/{date}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<List<ReservationGetDTO>>();
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString()); return null;
            }
        }

    }
}

