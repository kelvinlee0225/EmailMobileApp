using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EmailMobileApp.Models
{
    public class Mail : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Mail(string name, string email, string title, string content)
        {
            Name = name;
            Email = email;
            Title = title;
            Content = content;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

    }
}
