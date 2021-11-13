using QR_Presence.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace QR_Presence.ViewModels
{
    public class ListOfCoursesViewModel : BaseViewModel
    {
        public ObservableCollection<CourseInfoModel> ListOf { get; set; }
        public string PageTitle { get; set; }
        public Command<CourseInfoModel> Delete { get; private set; }

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

            Delete = new Command<CourseInfoModel>(async model =>
            {
                ListOf.Remove(model);
                PageTitle = $"Count {ListOf.Count}";
                OnPropertyChanged(nameof(PageTitle));
            });
            PageTitle = $"Count {ListOf.Count}";
            
        }
    }
}
