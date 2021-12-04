using Microcharts;
using QR_Presence.Models;
using QR_Presence.Models.APIModels;
using QR_Presence.Services;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Entry = Microcharts.ChartEntry;


namespace QR_Presence.ViewModels
{
    class CoursePageViewModel : BaseViewModel
    {
        public CoursesEnrolled Course { get; set; }
        public string EnrolledStudentsCount { get; set; }

        public List<ListOfDate> Dates_to_Show { get; set; }

        public ListOfDate _selectedDate { get; set; }
        public ListOfDate SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    if (_selectedDate != null)
                        GenerateStat.Execute(SelectedDate.Date);
                }
            }
        }


        private Interval _selectedInterval;
        public Interval SelectedInterval
        {
            get
            {
                return _selectedInterval;
            }
            set
            {
                if (_selectedInterval != value)
                {
                    _selectedInterval = value;
                    Dates_to_Show = Dates.Find(x => x.Id_interval == _selectedInterval.id_interval).List_of_dates;
                    OnPropertyChanged(nameof(Dates_to_Show));
                }
            }
        }

        #region Mock

        public Chart Graf { get; set; }

        public List<User> PersonsPresents { get; set; } = new List<User>();
        public List<User> PersonsAttentive { get; set; } = new List<User>();
        public List<User> PersonsActives { get; set; } = new List<User>();

        #endregion Mock


        private ExcelService ExcelService;
        public List<DatesModel> Dates { get; set; } = new List<DatesModel>();

        List<int> Intervals_id = new List<int>();
        public Command ExportExcel { get; set; }
        public Command GenerateStat { get; set; }

        public CoursePageViewModel(CoursesEnrolled course)
        {
            Course = course;
            ExcelService = new ExcelService();
            Course.Intervals.ForEach(i => Intervals_id.Add(i.id_interval));
            EnrolledStudentsCount = $"Number of students enrolled:{Course.Students_Enrolled.Count}";

            Task.Run(async () =>
            {
                Dates = await Services.APICalls.GetDatesForIntervalsAsync(Intervals_id);
            }).Wait();

            ExportExcel = new Command(async () =>
            {
                await ExportToExcel();
            });
            GenerateStat = new Command(async date =>
            {
                Graf = await Services.StatsService.GenerateStatsForDate(date.ToString(), SelectedInterval.id_interval);

                try
                {
                    PersonsPresents = new List<User>();
                    PersonsAttentive = new List<User>();
                    PersonsActives = new List<User>();

                    PersonsPresents = Services.StatsService.Stats.List_qr_attendance[0];
                    PersonsAttentive = Services.StatsService.Stats.List_qr_attendance[1];
                    PersonsActives = Services.StatsService.Stats.List_qr_attendance[2];
                }
                catch (ArgumentOutOfRangeException ex)
                {
                }

                OnPropertyChanged(nameof(Graf));
                OnPropertyChanged(nameof(PersonsPresents));
                OnPropertyChanged(nameof(PersonsAttentive));
                OnPropertyChanged(nameof(PersonsActives));

            });
        }

        #region ExportEcel
        async Task ExportToExcel()
        {
            var fileName = $"Presence{Course.name_course}-{SelectedInterval.day}{SelectedInterval.start_h}00{SelectedDate}.xlsx";
            string filepath = ExcelService.GenerateExcel(fileName);


            PutDataInList(PersonsPresents, "QR_1", filepath);
            PutDataInList(PersonsAttentive, "QR_2", filepath);
            PutDataInList(PersonsActives, "QR_3", filepath);

            ExcelStructure data = new ExcelStructure
            {
                Headers = new List<string>() { $"FullStrike", "Absence" }
            };

            data.Values.Add(new List<string>() { Services.StatsService.Stats.FullStrike.ToString(), Services.StatsService.Stats.Absent.ToString() });
            ExcelService.InsertDataIntoSheet(filepath, "QR_1", data);


            await Share.RequestAsync(new ShareFileRequest()
            {
                Title = $"{Course.name_course}-{DateTime.Now.ToString("dd-MMMM-yyyy")}",
                File = new ShareFile(filepath)
            });
        }
        public void PutDataInList(List<User> users, string qr, string filepath)
        {
            ExcelStructure data = new ExcelStructure
            {
                Headers = new List<string>() { $"{qr} - Name", "SecondName", "Group", "LDAP" }
            };

            foreach (var item in users)
            {
                data.Values.Add(new List<string>() { item.Name, item.SecondName, item.Group, item.Ldap });
            }

            ExcelService.InsertDataIntoSheet(filepath, "QR_1", data);
        }
        #endregion ExportEcel
    }
}
