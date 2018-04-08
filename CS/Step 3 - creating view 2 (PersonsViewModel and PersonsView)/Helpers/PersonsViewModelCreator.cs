using DX_MVVM.Models;
using DX_MVVM.ViewModels;

namespace DX_MVVM.Helpers {
    public static class PersonsViewModelCreator {
        public static readonly PersonsViewModel PersonsViewModel;

        static PersonsViewModelCreator() {
            PersonsViewModel = new PersonsViewModel(PersonsCreator.Persons, CreatingMethod);
        }
        static PersonViewModel CreatingMethod(Person person) {
            foreach(PersonViewModel personViewModel in PersonViewModelCreator.PersonViewModel)
                if(personViewModel.Person == person)
                    return personViewModel;
            return new PersonViewModel(person);
        }
    }
}
