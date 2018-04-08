using DX_MVVM.Models;

namespace DX_MVVM.Helpers {
    public static class PersonsCreator {
        public static readonly Persons Persons;

        static PersonsCreator() {
            Persons = new Persons();
            foreach(Person person in PersonCreator.Person)
                Persons.Add(person);
        }
    }
}
