using Microcharts;
using QR_Presence.Models;
using QR_Presence.Models.APIModels;
using QR_Presence.Services;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.ChartEntry;


namespace QR_Presence.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage : ContentPage
    {
        public CoursesEnrolled Course { get; set; } 

        public List<UserModel> PersonsPresents { get; set; } = new List<Models.UserModel>
        {
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
             new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
             new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
             new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
             new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            }
        };
        public List<UserModel> PersonsAttentive { get; set; } = new List<Models.UserModel>
        {
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            }
        };
        public List<UserModel> PersonsActives { get; set; } = new List<Models.UserModel>
        {
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                name = "Maria",
                surname = "Lipan",
                group="344C4",
                username = "maria_lipan2902",
                Privilege = 1
            }
        };

        public List<string> Dates { get; set; } = new List<string>
        {
            new DateTime(2021,12,23).ToString("MMMM dd, yyyy"),
            new DateTime(2021,12,23).ToString("MMMM dd, yyyy"),
            new DateTime(2021,12,23).ToString("MMMM dd, yyyy"),
            new DateTime(2021,12,23).ToString("MMMM dd, yyyy"),
            new DateTime(2021,12,23).ToString("MMMM dd, yyyy"),
            new DateTime(2021,12,23).ToString("MMMM dd, yyyy"),
        };

        public Button CurrentListSelected { get; set; } = new Button { Text = "-" };

        public static List<Entry> Entrys { get; set; } = new List<Entry>
        {
            new Entry(50)
            {
                Color = SKColor.Parse(Color.Red.ToHex()),
                ValueLabelColor = SKColor.Parse(Color.Red.ToHex()),
                Label = "Presence",
                TextColor = SKColor.Parse(Color.Red.ToHex()),
                ValueLabel = $"50"
            },
            new Entry(30)
            {
                Color = SKColor.Parse(Color.Green.ToHex()),
                ValueLabelColor = SKColor.Parse(Color.Green.ToHex()),
                Label = "Attentive",
                TextColor = SKColor.Parse(Color.Green.ToHex()),

                ValueLabel = $"30"
            },
            new Entry(25)
            {
                Color = SKColor.Parse(Color.Blue.ToHex()),
                ValueLabelColor = SKColor.Parse(Color.Blue.ToHex()),
                Label = "Actives",
                TextColor = SKColor.Parse(Color.Blue.ToHex()),

                ValueLabel = $"25"
            },
        };
        public Chart Graf { get; set; } = new PointChart
        {
            Entries = Entrys,
            LabelTextSize = 40f,
            Margin = 50,

            LabelOrientation = Orientation.Horizontal,
            ValueLabelOrientation = Orientation.Horizontal,
            BackgroundColor = SKColor.Parse(Color.Transparent.ToHex())
        };

        private ExcelService excelService;

        public IntervalModel SelectedInterval { get; set; }
        public string SelectedDate { get; set; }

        public string EnrolledStudentsCount { get; set; }

        public CoursePage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public CoursePage(CoursesEnrolled course)
        {
            InitializeComponent();
            Course = course;
            EnrolledStudentsCount = $"Number of students enrolled:{Course.Students_Enrolled.Count}";
            BindingContext = this;
            excelService = new ExcelService();

        }

        async Task ExportToExcel()
        {
            var fileName = $"Presence{Course.name_course}-{SelectedInterval + SelectedDate }.xlsx";
            string filepath = excelService.GenerateExcel(fileName);


            PutDataInList(PersonsPresents, "QR_1", filepath);
            PutDataInList(PersonsAttentive, "QR_2", filepath);
            PutDataInList(PersonsActives, "QR_3", filepath);

            await Share.RequestAsync(new ShareFileRequest()
            {
                Title = $"{Course.name_course}-{DateTime.Now.ToString("dd-MMMM-yyyy")}",
                File = new ShareFile(filepath)
            });
        }

        public void PutDataInList(List<UserModel> users, string qr, string filepath)
        {
            ExcelStructure data = new ExcelStructure
            {
                Headers = new List<string>() { $"{qr} - Name", "SecondName", "Group", "LDAP" }
            };

            foreach (var item in users)
            {
                data.Values.Add(new List<string>() { item.name, item.surname, item.group, item.username });
            }

            excelService.InsertDataIntoSheet(filepath, qr, data);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == CurrentListSelected)
            {
                if (btn == presenceButton)
                {
                    personsPresents.IsVisible = !personsPresents.IsVisible;
                }
                else if (btn == attentiveButton)
                {
                    personsAttentive.IsVisible = !personsAttentive.IsVisible;
                }
                else if (btn == activesButton)
                {
                    personsActives.IsVisible = !personsActives.IsVisible;
                }
                return;
            }

            if (btn == presenceButton)
            {
                personsPresents.IsVisible = true;
            }
            else if (btn == attentiveButton)
            {
                personsAttentive.IsVisible = true;
            }
            else if (btn == activesButton)
            {
                personsActives.IsVisible = true;
            }

            if (CurrentListSelected.Text != "-")
            {
                if (CurrentListSelected == presenceButton)
                {
                    personsPresents.IsVisible = false;
                }
                else if (CurrentListSelected == attentiveButton)
                {
                    personsAttentive.IsVisible = false;
                }
                else if (CurrentListSelected == activesButton)
                {
                    personsActives.IsVisible = false;
                }

            }
            CurrentListSelected = btn;

            return;
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
        //    await Navigation.PushAsync(new Views.AdminPages.EditCoursePage(Course));
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            await ExportToExcel();
        }
    }
}