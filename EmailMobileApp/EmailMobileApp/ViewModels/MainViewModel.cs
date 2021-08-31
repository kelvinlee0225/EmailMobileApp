using EmailMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

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

        public ObservableCollection<Mail> Mails { get; } = new ObservableCollection<Mail>()
        {
            new Mail("Kelvin", "example@example.com", "Try1", "Content1"),
            new Mail("Kelvin", "example@example.com", "Try2", "Content2")
        };
        public MainViewModel()
        {
            SelectedMailCommand = new Command<Mail>(OnMailSelected);
            AddMailCommand = new Command<Mail>(AddMail);
            DeleteMailCommand = new Command<Mail>(DeleteMail);
        }

        private void DeleteMail(Mail mail)
        {
            Mails.Remove(mail);
        }

        private async void AddMail(Mail mail)
        {
            var nombre = await App.Current.MainPage.DisplayPromptAsync("Especifique", "Su nombre");
            var correo = await App.Current.MainPage.DisplayPromptAsync("Especifique", "Correo de destino");
            var titulo = await App.Current.MainPage.DisplayPromptAsync("Especifique", "Titulo del correo");
            var contenido = await App.Current.MainPage.DisplayPromptAsync("Especifique", "Contenido del correo");

            if(nombre != null && correo != null && titulo != null && contenido != null)
            {
                Mails.Add(new Mail(nombre, correo, titulo, contenido));
            }
        }

        private async void OnMailSelected(Mail mail)
        {
            await App.Current.MainPage.DisplayAlert(mail.Name,mail.Title, "OK");
        }
    }
}
