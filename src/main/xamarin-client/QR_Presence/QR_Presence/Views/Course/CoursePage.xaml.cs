using Microcharts;
using QR_Presence.Models;
using QR_Presence.Models.APIModels;
using QR_Presence.Services;
using QR_Presence.ViewModels;
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
        public Button CurrentListSelected { get; set; } = new Button { Text = "-" };

        public CoursePage()
        {
            InitializeComponent();
            BindingContext = this;
        }
        public CoursePage(CoursesEnrolled course)
        {
            InitializeComponent();
            BindingContext = new CoursePageViewModel(course);
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
            // await Navigation.PushAsync(new Views.AdminPages.EditCoursePage(Course));
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Button_Clicked_3(object sender, EventArgs e)
        {
        }
    }
}