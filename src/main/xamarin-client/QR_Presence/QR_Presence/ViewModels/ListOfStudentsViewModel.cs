﻿using QR_Presence.Models;
using QR_Presence.Models.APIModels;
using QR_Presence.Views;
using QR_Presence.Views.EditPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QR_Presence.ViewModels
{
    public class ListOfStudentsViewModel : BaseViewModel
    {
        public ObservableCollection<User> ListOf { get; set; }
        public string PageTitle { get; set; }
        public Command<User> Delete { get; private set; }

        User selectedElement;
        public User SelectedElement
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



        public ListOfStudentsViewModel()
        {
            Task.Run(async () =>
            {
                StudentsAdmin stud = await Services.APICalls.GetStudentsAdminAsync();
                ListOf = new ObservableCollection<User>(stud.students);
            }).Wait();

            Delete = new Command<User>(async model =>
            {
                if (await Services.APICalls.DeleteUserAdminAsync(model.Email))
                {
                    ListOf.Remove(model);
                    PageTitle = $"Count {ListOf.Count}";
                    OnPropertyChanged(nameof(PageTitle));
                }
            });

             GoToNextPage = new Command(async () =>
            {
                if (SelectedElement != null)
                    await Application.Current.MainPage.Navigation.PushAsync(new EditProfile { BindingContext = SelectedElement });
            });

            GoNewElementPage = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage(false));
            });

            RefreshCommand = new Command(async () =>
            {
                IsRefreshing = true;
                StudentsAdmin stud = await Services.APICalls.GetStudentsAdminAsync();
                ListOf = new ObservableCollection<User>(stud.students);
                OnPropertyChanged(nameof(ListOf));
                PageTitle = $"Count {ListOf.Count}";
                OnPropertyChanged(nameof(PageTitle));

                IsRefreshing = false;

            });


            PageTitle = $"Count {ListOf.Count}";
        }
    }
}
