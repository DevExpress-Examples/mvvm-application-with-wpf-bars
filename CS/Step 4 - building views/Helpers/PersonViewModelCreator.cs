using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DX_MVVM.ViewModels;

namespace DX_MVVM.Helpers {
    public static class PersonViewModelCreator {
        public static readonly PersonViewModel[] PersonViewModel;
        public static PersonViewModel DesignPersonViewModel { get { return PersonViewModel[0]; } }

        static PersonViewModelCreator() {
            int length = PersonCreator.Person.Length;
            PersonViewModel = new PersonViewModel[length];
            for(int i = 0; i < length; i++)
                PersonViewModel[i] = new PersonViewModel(PersonCreator.Person[i]) { IsReadOnly = true };
        }
    }
}
