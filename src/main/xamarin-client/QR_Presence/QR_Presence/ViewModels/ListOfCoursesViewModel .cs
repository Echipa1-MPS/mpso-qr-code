using QR_Presence.Models;
using QR_Presence.Views;
using QR_Presence.Views.AdminPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                }
            }
        }


        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    OnPropertyChanged(nameof(IsRefreshing));
                }
            }
        }

        public Command GoToNextPage { get; set; }
        public Command GoNewElementPage { get; set; }
        public Command RefreshCommand { get; set; }


        public ListOfCoursesViewModel()
        {

            Task.Run(async () =>
            {
                GetCoursesModel cour = await Services.APICalls.GetAllCourses();
                ListOf = new ObservableCollection<CourseInfoModel>(cour.Courses);
            }).Wait();

            Delete = new Command<CourseInfoModel>(async model =>
           {
               if (await Services.APICalls.DeleteCourseAdminAsync(model.Id_Course))
               {
                   ListOf.Remove(model);

                   PageTitle = $"Count {ListOf.Count}";
                   OnPropertyChanged(nameof(PageTitle));
               }
           });

            GoToNextPage = new Command(async () =>
            {
                if (SelectedElement != null)
                    await Application.Current.MainPage.Navigation.PushAsync(new EditCoursePage(SelectedElement));
            });

            GoNewElementPage = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new EditCoursePage());
            });

            RefreshCommand = new Command(async () =>
            {
                IsRefreshing = true;
                GetCoursesModel cour = await Services.APICalls.GetAllCourses();
                ListOf = new ObservableCollection<CourseInfoModel>(cour.Courses);
                OnPropertyChanged(nameof(ListOf));
                IsRefreshing = false;
                PageTitle = $"Count {ListOf.Count}";


            });

            PageTitle = $"Count {ListOf.Count}";
        }
    }
}
