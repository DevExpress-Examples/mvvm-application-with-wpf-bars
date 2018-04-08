using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXBarsAndMVVM.Models;

namespace DXBarsAndMVVM.Commands {
    //public static class PersonsCommands {
    //    public static readonly MenuCommandBase NewPersonCommand;
    //    public static readonly MenuCommandBase DeletePersonCommand;

    //    static PersonsCommands() {
    //        NewPersonCommand = new MenuCommandBase(OnNewPersonExecute, OnNewPersonCanExecute) {
    //            Caption = "New",
    //            Glyph = new Uri("/DXBarsAndMVVM;component/Images/Icons/new-16x16.png", UriKind.Relative),
    //        };
    //        DeletePersonCommand = new MenuCommandBase(OnDeletePersonExecute, OnDeletePersonCanExecute) {
    //            Caption = "Delete",
    //            Glyph = new Uri("/DXBarsAndMVVM;component/Images/Icons/close-16x16.png", UriKind.Relative),
    //        };
    //    }
    //    static void OnNewPersonExecute(object parameter) {
    //        Persons persons = (Persons)parameter;
    //        persons.Add(new Person());
    //    }
    //    static bool OnNewPersonCanExecute(object parameter) {
    //        return true;
    //    }
    //    static void OnDeletePersonExecute(object parameter) {
    //        Persons persons = (Persons)parameter;
    //        persons.Remove(persons.Selected);
    //    }
    //    static bool OnDeletePersonCanExecute(object parameter) {
    //        Persons persons = (Persons)parameter;
    //        return persons.Count > 0;
    //    }
    //}
}
