using FitnessClientWPF.Model;
using FitnessClientWPF.Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using static FitnessClientWPF.Model.Reservation;

namespace FitnessClientWPF {
    public partial class MainWindow : Window {

        private readonly ReservationWindow _reservationWindow;
        private MemberService memberService;
        private ReservationService reservationService;
        private Member SelectedMember;
        private string formattedDate;
        private int EquipmentTimeSlots;
        private List<Member> _members;
        private ComboBox _memberComboBox;
        private const int TimeSlotHourDif = 7;
        private DispatcherTimer _searchTimer;

        public MainWindow(ReservationWindow reservationWindow) {
            InitializeComponent();
            memberService = new MemberService();
            reservationService = new ReservationService();
            _reservationWindow = reservationWindow;

            _searchTimer = new DispatcherTimer();
            _searchTimer.Interval = TimeSpan.FromMilliseconds(300); // 300 ms vertraging
            _searchTimer.Tick += SearchTimer_Tick;

            // Vul de Hours ComboBox
            Hours.Items.Add(1);
            Hours.Items.Add(2);

            InitializeData();
        }
        private async void InitializeData() {
            try {
                // Haal alle leden op
                 _members = await memberService.GetAllMembersAsync();
               
            } catch (Exception ex) {
                MessageBox.Show($"Error while loading {ex.Message}", "Error");
                Application.Current.Shutdown(); // Sluit de applicatie als cruciale gegevens niet kunnen worden geladen
            }
        }

        private void cbMember_KeyUp(object sender, KeyEventArgs e) {
            _searchTimer.Stop(); // Stop de timer als deze al actief is
            _searchTimer.Start(); // Start de timer opnieuw
        }

        private void SearchTimer_Tick(object? sender, EventArgs e) {
            _searchTimer.Stop(); // Stop de timer om te voorkomen dat deze meerdere keren wordt uitgevoerd

            string searchText = cbMember.Text?.ToLower() ?? string.Empty;

            if (_members == null || !_members.Any())
                return;

            string[] searchTerms = searchText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var filteredMembers = _members
                .Where(member =>
                    searchTerms.All(term =>
                        (!string.IsNullOrEmpty(member.FirstName) && member.FirstName.ToLower().Contains(term)) ||
                        (!string.IsNullOrEmpty(member.LastName) && member.LastName.ToLower().Contains(term))))
                .ToList();

            if (string.IsNullOrWhiteSpace(searchText)) {
                filteredMembers = _members.ToList();
            }

            cbMember.ItemsSource = filteredMembers;
            cbMember.IsDropDownOpen = filteredMembers.Any();
           
        }
        private void cbMember_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (cbMember.SelectedItem is Member selectedMember) {
                SelectedMember = selectedMember; // Stel SelectedMember in
                Console.WriteLine($"Selected Member: {SelectedMember.FirstName} {SelectedMember.LastName}");
            } else {
                SelectedMember = null; // Reset SelectedMember als er niets geselecteerd is
            }
        }


        private void ExitClick(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private async void SearchBtn_Click(object sender, RoutedEventArgs e) {
            if (SelectedMember == null) {
                MessageBox.Show("Select a Member.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (dpDate.SelectedDate == null) {
                MessageBox.Show("Select a Date.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime dpDatum = dpDate.SelectedDate.Value;
            DateOnly date = DateOnly.FromDateTime(dpDatum);
            formattedDate = date.ToString("yyyy-MM-dd");

            List<ReservationGetDTO> reservationsMemberDate = await reservationService.GetMemberDateAsync(SelectedMember.MemberId, formattedDate);
            List<ReservationSetDTO> reservationDisplay = new List<ReservationSetDTO>();

            foreach (ReservationGetDTO reservation in reservationsMemberDate) {
                if (reservation.Reservations.Count > 0) {
                    for (int i = 0; i < reservation.Reservations.Count; i++) {
                        ReservationSetDTO reservationSet = new ReservationSetDTO(
                            reservation.ReservationId + i,
                            reservation.GroupsId,
                            reservation.MemberId,
                            reservation.FirstName,
                            reservation.LastName,
                            reservation.Email,
                            reservation.Date,
                            reservation.Reservations[i].TimeSlot.TimeSlotId,
                            reservation.Reservations[i].TimeSlot.StartTime,
                            reservation.Reservations[i].TimeSlot.EndTime,
                            reservation.Reservations[i].TimeSlot.PartOfDay,
                            reservation.Reservations[i].Equipment.EquipmentId,
                            reservation.Reservations[i].Equipment.EquipmentType);

                        reservationDisplay.Add(reservationSet);
                    }
                }
            }
            EquipmentTimeSlots = reservationDisplay.Count();
            ReservationsList.ItemsSource = reservationDisplay;
            ResetUILowerComponents();
        }

        private async void Find_Click(object sender, RoutedEventArgs e) {
            if (SelectedMember == null) {
                MessageBox.Show("Select a Member.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(formattedDate)) {
                MessageBox.Show("Select a Date.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int selectedTimeSlot = 0;

            if (TimeSlots.SelectedItem != null && int.TryParse(TimeSlots.SelectedItem.ToString(), out int parsedTimeSlot)) {
                selectedTimeSlot = parsedTimeSlot - TimeSlotHourDif;
            } else {
                MessageBox.Show("Select a valid time slot.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (selectedTimeSlot == null) {
                MessageBox.Show("Select a Timeslot.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Dictionary<int, List<Equipment>> availableTimeSlots = await reservationService.GetAvailableTimeSlotsAsync(formattedDate);

            List<Equipment> equipmentTimeSlot = availableTimeSlots
             .Where(x => x.Key == selectedTimeSlot)
             .SelectMany(x => x.Value)
             .ToList();

            List<Equipment> equipmentTimeSlot2 = availableTimeSlots
                .Where(x => x.Key == selectedTimeSlot + 1)
                 .SelectMany(x => x.Value)
                 .ToList();

            int hour = (int)Hours.SelectedItem;
            if (hour == 1 ) {
                if (EquipmentTimeSlots < 4) {
                    EquipmentGrid.ItemsSource = equipmentTimeSlot;
                    SecondEquipment.ItemsSource = null;
                } else {
                    MessageBox.Show("Can' make more then 4 reservations on a day");
                }

            } 
            if (hour == 2 ) {
                if (EquipmentTimeSlots < 3) {
                    EquipmentGrid.ItemsSource = equipmentTimeSlot;
                    SecondEquipment.ItemsSource = equipmentTimeSlot2;
                } else {
                    MessageBox.Show("Can't make more then 4 reservations on a day");
                }

            } 

        }
        private async void Hours_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (string.IsNullOrEmpty(formattedDate)) {
                MessageBox.Show("Select a Date.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (SelectedMember == null) {
                MessageBox.Show("Select a member.", "error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var selectedHour = Hours.SelectedItem;
            try {
                Dictionary<int, List<Equipment>> availableTimeSlots = await reservationService.GetAvailableTimeSlotsAsync(formattedDate);
                List<ReservationGetDTO> personReservations = await reservationService.GetMemberDateAsync(SelectedMember.MemberId, formattedDate);

                List<int> personTimeSlots = personReservations
                   .SelectMany(reservation => reservation.Reservations)
                   .Select(r => r.TimeSlot.TimeSlotId)
                   .Distinct()
                   .ToList();

                personTimeSlots.Sort();
                Dictionary<int, List<Equipment>> personAvailableTimeSlots = new Dictionary<int, List<Equipment>>();


                if (selectedHour != null && int.TryParse(selectedHour.ToString(), out int hour) && hour == 1 && EquipmentTimeSlots < 4) {
                    foreach (int slot in availableTimeSlots.Keys) {

                        bool contains = personTimeSlots.Contains(slot);
                       


                        if (!contains ) {
                            personAvailableTimeSlots[slot] = availableTimeSlots[slot];
                        }
                    }

                    TimeSlots.Items.Clear();
                    if (EquipmentTimeSlots < 4) {
                        foreach (int key in personAvailableTimeSlots.Keys) {
                            TimeSlots.Items.Add(key + TimeSlotHourDif);
                        }
                    }
                   


                } else {
                    TimeSlots.Items.Clear();
                }
                if (selectedHour != null && int.TryParse(selectedHour.ToString(), out int hour2) && hour2 == 2 && EquipmentTimeSlots < 3) {
                    foreach (int slot in availableTimeSlots.Keys) {

                        bool contains = personTimeSlots.Contains(slot);
                        bool twoAfter = personTimeSlots.Contains(slot + 1);
                        if (!contains && !twoAfter) {
                            personAvailableTimeSlots[slot] = availableTimeSlots[slot];
                        }
                    }


                    List<int> keysToRemove = new List<int>();
                    foreach (int slot in personAvailableTimeSlots.Keys) {
                        if (personAvailableTimeSlots.ContainsKey(slot + 1) && !personAvailableTimeSlots.ContainsKey(slot + 2)) {
                            keysToRemove.Add(slot + 1);
                        }
                    }


                    foreach (int key in keysToRemove) {
                        personAvailableTimeSlots.Remove(key);
                    }


                    TimeSlots.Items.Clear();
                    if (EquipmentTimeSlots < 3) {
                        foreach (int key in personAvailableTimeSlots.Keys) {
                            TimeSlots.Items.Add(key + TimeSlotHourDif);
                        }
                    }
                   
                }


            } catch (Exception ex) {

                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ResetUIComponents();
            }

        }
        private void ShowSelected_Click(object sender, RoutedEventArgs e) {

            Equipment? selectedEquipment1 = EquipmentGrid.SelectedItem as Equipment;
            Equipment? selectedEquipment2 = SecondEquipment.SelectedItem as Equipment;


            StringBuilder displayText = new StringBuilder();

            if (selectedEquipment1 != null)
                displayText.Append($"Slot1: {selectedEquipment1.EquipmentType} (ID: {selectedEquipment1.EquipmentId})");

            if (selectedEquipment2 != null)
                displayText.Append($", Slot2: {selectedEquipment2.EquipmentType} (ID: {selectedEquipment2.EquipmentId})");

            if (selectedEquipment1 == null && selectedEquipment2 == null)
                displayText.AppendLine("No items selected.");


            SelectedItemsDisplay.Text = displayText.ToString();
        }

        private async void ReservationWindow_click(object sender, RoutedEventArgs e) {
            // Valideer verplichte velden
            if (SelectedMember == null) {
                MessageBox.Show("Select a Member.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!DateOnly.TryParseExact(formattedDate, "yyyy-MM-dd", out DateOnly selectedDate)) {
                MessageBox.Show("Invalid date format.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

           
            if (selectedDate < today) {
                MessageBox.Show("The selected date cannot be in the past.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                ResetUIComponents();
                return;
            }

           
            if (selectedDate > today.AddDays(7)) {
                MessageBox.Show("The selected date cannot be more than 7 days in the future.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                ResetUIComponents();
                return;
            }
            if (string.IsNullOrEmpty(formattedDate)) {
                MessageBox.Show("Select a Date.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                ResetUIComponents();
                return;
            }

            if (TimeSlots.SelectedItem == null) {
                MessageBox.Show("Select a TimeSlot.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                ResetUIComponents();
                return;
            }

            if (EquipmentGrid.SelectedItem == null) {
                MessageBox.Show("Select at least 1 equipment", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if ((int)Hours.SelectedItem == 2) {
                if (SecondEquipment.SelectedItem == null) {
                    MessageBox.Show("Selects second Equipment", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            ReservationWindow reservationWindow = new ReservationWindow();
            bool? result = reservationWindow.ShowDialog();


            Equipment equipment = (Equipment)EquipmentGrid.SelectedItem;
            Equipment? equipment2 = (Equipment)EquipmentGrid.SelectedItem;

            List<TimeSlotEquipment> timeSlotEquipments = new List<TimeSlotEquipment>
            {
                new TimeSlotEquipment
                {
                    TimeSlotId = (int)TimeSlots.SelectedItem - TimeSlotHourDif,
                    EquipmentId = equipment.EquipmentId
                }
            };

            if (int.TryParse(Hours.SelectedItem?.ToString(), out int selectedValue)) {
                if (selectedValue == 2 && equipment2 != null) {
                    TimeSlotEquipment timeSlotEquipment2 = new TimeSlotEquipment {
                        TimeSlotId = (int)TimeSlots.SelectedItem + 1 - TimeSlotHourDif,
                        EquipmentId = equipment2.EquipmentId
                    };
                    timeSlotEquipments.Add(timeSlotEquipment2);
                }
            }

            ReservationPost reservation = new ReservationPost {
                MemberId = SelectedMember.MemberId,
                Date = DateOnly.ParseExact(formattedDate, "yyyy-MM-dd"),
                Reservations = timeSlotEquipments
            };

            try {
                if (result == true) {
                    await reservationService.PostReservationAsync(reservation);

                    // Succesbericht weergeven
                    MessageBox.Show("Reservation addes succesfully.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

                    ResetUIComponents();
                }
            } catch (Exception ex) {
                // Toon foutbericht als er een fout optreedt
                MessageBox.Show($"Reservatie failed: {ex.Message}", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
        private void ResetUIComponents() {
          
           
            Hours.ItemsSource = null;
            TimeSlots.ItemsSource = null;  // Verwijder items
            TimeSlots.SelectedIndex = -1;  // Zorg ervoor dat er niets geselecteerd is
            TimeSlots.IsEnabled = false;   // Schakel de ComboBox uit

            EquipmentGrid.ItemsSource = null;
            SecondEquipment.ItemsSource = null;
            SelectedItemsDisplay.Text = string.Empty;
        }
        private void ResetUILowerComponents() {


            Hours.ItemsSource = null;
            TimeSlots.Items.Clear();
            TimeSlots.ItemsSource = null;  
            TimeSlots.SelectedIndex = -1;  
            TimeSlots.IsEnabled = true;   

            EquipmentGrid.ItemsSource = null;
            SecondEquipment.ItemsSource = null;
            SelectedItemsDisplay.Text = string.Empty;
        }

    }
}
