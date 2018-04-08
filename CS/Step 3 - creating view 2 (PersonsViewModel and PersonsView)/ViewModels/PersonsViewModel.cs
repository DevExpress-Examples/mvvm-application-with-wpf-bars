using System;
using System.Collections.Specialized;
using System.ComponentModel;
using DevExpress.Xpf.Core;
using DX_MVVM.Commands;
using DX_MVVM.Models;

namespace DX_MVVM.ViewModels {
    public class PersonsViewModel : LockableCollection<PersonViewModel> {
        public CommandBase NewPersonCommand { get; private set; }
        public CommandBase DeletePersonCommand { get; private set; }
        public Persons Persons { get; private set; }
        public PersonViewModel Selected {
            get { return this[SelectedIndex]; }
            protected set { SelectedIndex = IndexOf(value); }
        }
        public int SelectedIndex {
            get { return selectedIndex; }
            set {
                value = IsValidSelectedIndex(value) ? value : -1;
                if(selectedIndex == value) return;
                selectedIndex = value;
                OnSelectedIndexChanged();
            }
        }
        public PersonViewModel this[Person person] {
            get {
                if(person == null) return null;
                foreach(PersonViewModel viewModel in this)
                    if(viewModel.Person == person)
                        return viewModel;
                return null;
            }
        }

        public PersonsViewModel(Persons persons) :
            this(persons, null) {
        }
        internal PersonsViewModel(Persons persons, Func<Person, PersonViewModel> creatingMethod) {
            NewPersonCommand = new CommandBase(OnNewPersonCommadExecute);
            DeletePersonCommand = new CommandBase(OnDeletePersonCommadExecute, OnDeletePersonCommadCanExecute);
            CreatingMethod = creatingMethod;
            Persons = persons;
            Persons.CollectionChanged += new NotifyCollectionChangedEventHandler(OnPersonsCollectionChanged);
            SyncCollection();
        }
        protected virtual void OnNewPersonCommadExecute(object paremeter) {
            Person person = new Person() {
                FirstName = "First Name",
                LastName = "Last Name",
            };
            Persons.Add(person);
            Selected = this[person];
        }
        protected virtual void OnDeletePersonCommadExecute(object paremeter) {
            int selectedIndex = SelectedIndex;
            Persons.Remove(Selected.Person);
            if(IsValidSelectedIndex(selectedIndex))
                SelectedIndex = selectedIndex;
            else if(selectedIndex >= Count)
                SelectedIndex = Count - 1;
            else if(selectedIndex < 0)
                selectedIndex = 0;
        }
        protected virtual bool OnDeletePersonCommadCanExecute(object paremeter) {
            return IsValidSelectedIndex(SelectedIndex);
        }
        protected virtual void OnSelectedIndexChanged() {
            OnPropertyChanged(new PropertyChangedEventArgs("Selected"));
            OnPropertyChanged(new PropertyChangedEventArgs("SelectedIndex"));
            DeletePersonCommand.RaiseCanExecuteChanged();
        }
        protected virtual void OnPersonsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            SyncCollection();
        }
        protected void SyncCollection() {
            BeginUpdate();
            Clear();
            foreach(Person person in Persons) {
                PersonViewModel personViewModel;
                if(CreatingMethod != null) personViewModel = CreatingMethod(person);
                else personViewModel = new PersonViewModel(person);
                Add(personViewModel);
            }
            EndUpdate();
            if(SelectedIndex == -1 && Count > 0) SelectedIndex = 0;
        }
        
        bool IsValidSelectedIndex(int selectedIndex) {
            return selectedIndex >= 0 && selectedIndex < Count;
        }

        int selectedIndex = -1;
        Func<Person, PersonViewModel> CreatingMethod = null;
    }
}
