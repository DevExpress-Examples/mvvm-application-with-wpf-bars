using System;
using DX_MVVM.Models;

namespace DX_MVVM.ViewModels {
    public class PersonViewModel : ViewModelBase {
        public String FirstName {
            get { return Person.FirstName; }
            set {
                if(Person.FirstName == value) return;
                Person.FirstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public String LastName {
            get { return Person.LastName; }
            set {
                if(Person.LastName == value) return;
                Person.LastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public Uri Photo {
            get { return Person.Photo; }
            set {
                if(Person.Photo == value) return;
                Person.Photo = value;
                OnPropertyChanged("Photo");
            }
        }
        public String Email {
            get { return Person.Email; }
            set {
                if(Person.Email == value) return;
                Person.Email = value;
                OnPropertyChanged("Email");
            }
        }
        public bool IsReadOnly {
            get { return isReadOnly; }
            set {
                if(isReadOnly == value) return;
                isReadOnly = value;
                OnPropertyChanged("IsReadOnly");
            }
        }
        public Person Person { get; private set; }

        public PersonViewModel(Person person) {
            Person = person;
        }

        bool isReadOnly = false;
    }
}
