using System;
using DX_MVVM.Models;

namespace DX_MVVM.Helpers {
    public static class PersonCreator {
        public static readonly Person[] Person;

        static PersonCreator() {
            Person = new Person[4];
            Person[0] = new Person() {
                FirstName = "Anne",
                LastName = "Dodsworth",
                Photo = new Uri("/DX_MVVM;component/Images/AnneDodsworth.jpg", UriKind.Relative),
                Email = "Anne.Dodsworth@persons.com",
            };
            Person[1] = new Person() {
                FirstName = "Nancy",
                LastName = "Davolio",
                Photo = new Uri("/DX_MVVM;component/Images/NancyDavolio.jpg", UriKind.Relative),
                Email = "Nancy.Davolio@persons.com",
            };
            Person[2] = new Person() {
                FirstName = "Robert",
                LastName = "King",
                Photo = new Uri("/DX_MVVM;component/Images/RobertKing.jpg", UriKind.Relative),
                Email = "Robert.King@persons.com",
            };
            Person[3] = new Person() {
                FirstName = "Steven",
                LastName = "Buchanan",
                Photo = new Uri("/DX_MVVM;component/Images/StevenBuchanan.jpg", UriKind.Relative),
                Email = "Steven.Buchanan@persons.com",
            };
        }
    }
}
