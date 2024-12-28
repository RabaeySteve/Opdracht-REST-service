using FitnessClientWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static FitnessClientWPF.Model.Reservation;

namespace FitnessClientWPF.Services {
    public class MemberService {
        private HttpClient client;
        public MemberService() {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5054/");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }
        public async Task<List<Member>> GetAllMembersAsync() {
            try {
                HttpResponseMessage response = await client.GetAsync("/api/Member/members");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<List<Member>>();
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString()); return null;
            }
        }
      
    }
}
