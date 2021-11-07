using Microcharts;
using QR_Presence.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.ChartEntry;


namespace QR_Presence.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage : ContentPage
    {
        public CourseInfoModel Course { get; set; } = new CourseInfoModel
        {
            Id_Cours = 1,
            Name_C = "IOCLA",
            Professor = "Prof. Razvan Deaconescu",
            Desc = "Programare in limbaj de asamblare este un curs de din Anul 2 in care se invata notiuni de hardware",
            Grading = "30% 3 Teme \n20% Teste de curs \n50% examenul \nCerinte minime min 50% parcurs si min 50% examen \n",
            Intervals = new List<IntervalModel>
            {
                new IntervalModel
                {
                    Name="MPS",
                    Day="Marti",
                    StartHour=18,
                    Step=2
                },
                new IntervalModel
                {
                    Name="EP",
                    Day="Miercuri",
                    StartHour=12,
                    Step=2
                }
            }
        };

        public List<UserModel> PersonsPresents { get; set; } = new List<Models.UserModel>
        {
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
             new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
             new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
             new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
             new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            }
        };
        public List<UserModel> PersonsAttentive { get; set; } = new List<Models.UserModel>
        {
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            }
        };
        public List<UserModel> PersonsActives { get; set; } = new List<Models.UserModel>
        {
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
                Privilege = 1
            },
            new Models.UserModel
            {
                Name = "Maria",
                SecondName = "Lipan",
                Group="344C4",
                LDAP = "maria_lipan2902",
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

        public CoursePage()
        {
            InitializeComponent();
            BindingContext = this;
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
    }
}