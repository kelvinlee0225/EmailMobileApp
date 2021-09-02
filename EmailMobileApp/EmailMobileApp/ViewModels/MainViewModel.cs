using EmailMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EmailMobileApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Mail _selectedMail;
        public Mail SelectMail 
        {
            get
            {
                return _selectedMail;
            }
            set
            {
                _selectedMail = value;

                if(_selectedMail != null)
                {
                    SelectedMailCommand.Execute(_selectedMail);
                }
            }
        }
        public ICommand SelectedMailCommand { get;  }
        public ICommand AddMailCommand { get; }
        public ICommand DeleteMailCommand { get; }
        public ICommand AttachPhoto { get; }

        public ObservableCollection<Mail> Mails { get; set; } = new ObservableCollection<Mail>()
        {
            new Mail("from@example1","example@example.com", "Try1", "Content1"),
            new Mail("from@example2","example@example.com", "Try2", "Content2")
        };
        public MainViewModel()
        {
            SelectedMailCommand = new Command<Mail>(OnMailSelected);
            AddMailCommand = new Command<Mail>(AddMail);
            DeleteMailCommand = new Command<Mail>(DeleteMail);
            //AttachPhoto = new Command<Mail>(PhotoAttachment);
        }

        private void DeleteMail(Mail mail)
        {
            Mails.Remove(mail);
        }

        private async void AddMail(Mail mail)
        {
            var from = await App.Current.MainPage.DisplayPromptAsync("Especifique", "Su nombre");
            var to = await App.Current.MainPage.DisplayPromptAsync("Especifique", "Correo de destino");
            var subject = await App.Current.MainPage.DisplayPromptAsync("Especifique", "Titulo del correo");
            var content = await App.Current.MainPage.DisplayPromptAsync("Especifique", "Contenido del correo");

            if(from != null && to != null && subject != null && content != null)
            {
                Mails.Add(new Mail(from, to, subject, content));
            }
        }

        private async void OnMailSelected(Mail mail)
        {
            await App.Current.MainPage.DisplayAlert(mail.From ,mail.Subject, "OK");
        }
    }
}
