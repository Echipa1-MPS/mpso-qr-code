using QR_Presence.Models;
using QR_Presence.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace QR_Presence.ViewModels
{
    public class ListOfCoursesViewModel : BaseViewModel
    {
        public ObservableCollection<CourseInfoModel> ListOf { get; set; }
        public string PageTitle { get; set; }
        public Command<CourseInfoModel> Delete { get; private set; }


        CourseInfoModel selectedElement;
        public CourseInfoModel SelectedElement
        {
            get
            {
                return selectedElement;
            }
            set
            {
                if (selectedElement != value)
                {
                    selectedElement = value;
                    GoToNextPage.Execute(selectedElement);
                    selectedElement = null;
                }
            }
        }

        public Command GoToNextPage { get; set; }
        public ListOfCoursesViewModel()
        {
            ListOf = new ObservableCollection<CourseInfoModel>
            {
                new CourseInfoModel
                {
                    Name_C = "MPS",
                    Professor = "Vasile",
                    Desc= "mihai_vasile1@upb.ro",
                    Grading = "344CC",
                },
                new CourseInfoModel
                {
                    Name_C = "MPS",
                    Professor = "Vasile",
                    Desc= "mihai_vasile1@upb.ro",
                    Grading = "344CC",
                },
                new CourseInfoModel
                {
                    Name_C = "MPS",
                    Professor = "Vasile",
                    Desc= "mihai_vasile1@upb.ro",
                    Grading = "344CC",
                },
                new CourseInfoModel
                {
                    Name_C = "MPS",
                    Professor = "Vasile",
                    Desc= "mihai_vasile1@upb.ro",
                    Grading = "344CC",
                }
            };

            Delete = new Command<CourseInfoModel>(model =>
            {
                ListOf.Remove(model);
                PageTitle = $"Count {ListOf.Count}";
                OnPropertyChanged(nameof(PageTitle));
            });

            GoToNextPage = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CoursePage(SelectedElement));
            });

            PageTitle = $"Count {ListOf.Count}";
        }
    }
}
