<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128641144/16.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E3341)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# Simple MVVM application with DXBars - Tutorial


<p>This tutorial demonstrates how to implement a simple and flexible data management system by using the MVVM pattern for WPF platform.</p>
<p><strong>Task<br /> </strong>Implement a flexible data management system by using the MVVM pattern for WPF platform.</p>
<p><strong>Input Data<br /> </strong>A list of Person objects providing custom information (first name, last name, photo and email).</p>
<p><strong>Additional Requirements</strong><br /> The management system should provide an ability to modify information on persons and add/delete persons. The system should prevent accidental modification of persons' data via visual controls.</p>
<p><strong>Step 1. Creating Data Model.<br /> </strong>First, we create a simple data model:</p>
<p>1. The Person class with a set of relevant fields:</p>


```cs
public class Person { 
 public string FirstName = string.Empty; 
 public string LastName = string.Empty; 
 public Uri Photo = null; 
 public string Email = string.Empty; 
}

```

<p>2. The Persons entity that is a collection of Person objects:</p>


```cs
public class Persons : ObservableCollection<Person> {  }
```


<p>3. Utility classes that help creating Person objects:</p>


```cs
public static class PersonCreator { 
 public static readonly Person[] Person; 
 static PersonCreator() { 
   Person = new Person[4]; 
   Person[0] = new Person() { 
     //... 
   }; 
   //... 
 } 
} 

public static class PersonsCreator { 
 public static readonly Persons Persons; 
 static PersonsCreator() { 
   Persons = new Persons(); 
   foreach(Person person in PersonCreator.Person) 
     Persons.Add(person); 
 } 
}
```


<p><strong>Step 2. Creating Form for Person Entity.</strong><br /> In this step we create a view model for the Person entity by using the MVVM pattern - the PersonViewModel class. This class implements the interaction between the data model and UI.</p>
<p>1. Create a base view-model class implementing the INotifyPropertyChanged interface</p>


```cs
public class ViewModelBase : INotifyPropertyChanged { 
 public event PropertyChangedEventHandler PropertyChanged; 
 protected virtual void OnPropertyChanged(string propertyName) { 
   if(PropertyChanged != null) 
     PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); 
 } 
}
```
<p>2. Create the PersonViewModel class by inheriting from ViewModelBase. The class declares properties which are in-sync with Person object's properties.</p>

```cs
public class PersonViewModel : ViewModelBase { 
 public String FirstName { 
   get { return Person.FirstName; } 
   set { 
     if(Person.FirstName == value) return; 
     Person.FirstName = value; 
     OnPropertyChanged("FirstName"); 
   } 
 } 

 public Person Person { get; private set; } 

 public PersonViewModel(Person person) { 
   Person = person; 
 } 
 //... 
}
```

<p>The PersonViewModel class also contains a non-model related property (IsReadOnly). This property prevents objects from being modified by an end-user.</p>


```cs
public class PersonViewModel : ViewModelBase {
 //...
 public bool IsReadOnly {
   get { return isReadOnly; }
   set {
     if(isReadOnly == value) return;
     isReadOnly = value;
     OnPropertyChanged("IsReadOnly");
   }
 }
 bool isReadOnly = false;
 //...
}
```

<p>3. Create a view form containing controls used to display and edit data.</p>

```xml
<UserControl ...>
 <!--...-->
 <Grid>
   <Grid.RowDefinitions>
     <RowDefinition Height="*"/>
     <RowDefinition Height="Auto"/>
   </Grid.RowDefinitions>
   <dxe:ImageEdit Source="{Binding Photo}" IsReadOnly="True"/>
   <StackPanel Grid.Row="1" Orientation="Vertical" HorizontalAlignment="Center">
     <WrapPanel HorizontalAlignment="Center" Margin="3">
       <TextBlock Text="Fisrt Name:" VerticalAlignment="Center"/>
       <dxe:TextEdit Text="{Binding FirstName, UpdateSourceTrigger=PropertyChanged}" IsReadOnly="{Binding IsReadOnly}" Margin="5,0,0,0"/>
     </WrapPanel>
     <WrapPanel HorizontalAlignment="Center" Margin="3">
       <TextBlock Text="Last Name:" VerticalAlignment="Center"/>
       <dxe:TextEdit Text="{Binding LastName, UpdateSourceTrigger=PropertyChanged}" IsReadOnly="{Binding IsReadOnly}" Margin="5,0,0,0"/>
     </WrapPanel>
     <WrapPanel HorizontalAlignment="Center" Margin="3">
       <TextBlock Text="Email:" VerticalAlignment="Center"/>
       <dxe:TextEdit Text="{Binding Email, UpdateSourceTrigger=LostFocus}" IsReadOnly="{Binding IsReadOnly}" Margin="5,0,0,0"/>
     </WrapPanel>
   </StackPanel>
 </Grid>
 <!--...-->
</UserControl>
```

<p>4. A helper PersonViewModelCreator class creates a PersonViewModel and populates it with data:</p>


```cs
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
```

<p>Initialize the UserControl.DataContext property with the view-model provided by PersonViewModelCreator :</p>

```xml
<UserControl x:Class="DXBarsAndMVVM.Views.PersonView"
            xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
            xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
            xmlns:dx="http://schemas.devexpress.com/winfx/2008/xaml/core"
            xmlns:dDx="http://schemas.devexpress.com/winfx/2008/xaml/core"
            xmlns:dxe="http://schemas.devexpress.com/winfx/2008/xaml/editors"
            xmlns:dxb="http://schemas.devexpress.com/winfx/2008/xaml/bars"
            xmlns:hlp="clr-namespace:DXBarsAndMVVM.Helpers"
            xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
            xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
            mc:Ignorable="d dDx"
            dDx:ThemeManager.ThemeName="Office2007Silver" d:DesignHeight="300" d:DesignWidth="300" 
            d:DataContext="{x:Static hlp:PersonViewModelCreator.DesignPersonViewModel}">
 <!--...-->
</UserControl>
```

<p>5. Add an additional visual control element (BarCheckItem) to the view form that will be bound to the PersonViewModel.IsReadOnly property</p>

```xml
<UserControl ...>
 <dxb:BarManager>
   <dxb:BarManager.Items>
     <dxb:BarCheckItem Name="cbIsReadOnly" Content="Locked" IsChecked="{Binding IsReadOnly}"/>
   </dxb:BarManager.Items>
   <dxb:BarManager.Bars>
     <dxb:Bar>
       <dxb:Bar.ItemLinks>
         <dxb:BarCheckItemLink BarItemName="cbIsReadOnly"/>
       </dxb:Bar.ItemLinks>
     </dxb:Bar>
   </dxb:BarManager.Bars>
   <Grid>
     <!--editor controls-->
   </Grid>
 </dxb:BarManager>
</UserControl>
```

<p>6. To test the project, add the following code to the main window</p>

```xml
<Window x:Class="DXBarsAndMVVM.MainWindow"
       xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
       xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
       xmlns:vw="clr-namespace:DXBarsAndMVVM.Views"
       xmlns:hlp="clr-namespace:DXBarsAndMVVM.Helpers"
       Title="MainWindow" Height="350" Width="525" DataContext="{x:Static hlp:PersonViewModelCreator.DesignPersonViewModel}">
 <Grid>
   <vw:PersonView/>
 </Grid>
</Window>
```

<p><strong>Step 3. Creating Form for Persons Entity.</strong></p>
<p>1. Create a PersonsViewModel object that is a collection of PersonViewModel objects. The PersonsViewModel class is inherited from the LockableCollection<PersonViewMode> class defined in the DevExpress.Xpf.Core library. This class allows you to temporarily lock change notifications by using the BeginUpdate and EndUpdate methods. The BeginUpdate method suppresses the CollectionChanged event until the EndUpdate method is called. Once the EndUpdate method is called, the CollectionChanged event is raised.</p>


```cs
public class PersonsViewModel : LockableCollection<PersonViewModel> {
 public Persons Persons { get; private set; }
 public PersonsViewModel(Persons persons) {
   Persons = persons;
 }
}
```

<p>2. When the Persons model collection is changed, it should be synchronized with the PersonsViewModel.</p>

```cs
public class PersonsViewModel : LockableCollection<PersonViewModel> {
 public Persons Persons { get; private set; }
 public PersonsViewModel(Persons persons) {
   Persons = persons;
   Persons.CollectionChanged += new NotifyCollectionChangedEventHandler(OnPersonsCollectionChanged);
   SyncCollection();
 }

 protected virtual void OnPersonsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
   SyncCollection();
 }

 protected void SyncCollection() {
   BeginUpdate();
   Clear();
   foreach(Person person in Persons) 
     Add(new PersonViewModel(person));
   EndUpdate();
 }
}

```

<p>3. To allow a user to select a model, new Selected and SelectedIndex properties are introduced. In addition, an indexer property is added that allows you to get PersonViewModel objects by Person objects.</p>

```cs
public class PersonsViewModel : LockableCollection<PersonViewModel> {
 //...
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
     if(viewModel.Person == person) return viewModel;
     return null;
   }
 }

 protected virtual void OnSelectedIndexChanged() {
   OnPropertyChanged(new PropertyChangedEventArgs("Selected"));
   OnPropertyChanged(new PropertyChangedEventArgs("SelectedIndex"));
 }

 bool IsValidSelectedIndex(int selectedIndex) {
   return selectedIndex >= 0 && selectedIndex < Count;
 }

 int selectedIndex = -1;
 //...
}
```

<p>4. Now we create a view containing a ListBox control.</p>

```xml
<ListBox x:Name="list" ItemsSource="{Binding}" SelectedIndex="{Binding SelectedIndex}">
 <ListBox.ItemTemplate>
   <DataTemplate>
     <StackPanel Orientation="Horizontal">
       <TextBlock Text="{Binding FirstName}" />
       <TextBlock Text="{Binding LastName}" Margin="15,0,0,0" />
     </StackPanel>
   </DataTemplate>
 </ListBox.ItemTemplate>
</ListBox>
```

<p>5. A helper PersonsViewModelCreator class creates a PersonsViewModel and populates it with data:</p>


```cs
public class PersonsViewModel : LockableCollection<PersonViewModel> {
 public PersonsViewModel(Person persons) :
   this(persons, null) {
 }

 internal PersonsViewModel(Persons persons, Func<Person, PersonViewModel> creatingMethod) {
   NewPersonCommand = new PersonsCommand(OnNewPersonCommadExecute);
   DeletePersonCommand = new PersonsCommand(OnDeletePersonCommadExecute, OnDeletePersonCommadCanExecute);
   CreatingMethod = creatingMethod;
   Persons = persons;
   Persons.CollectionChanged += new NotifyCollectionChangedEventHandler(OnPersonsCollectionChanged);
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
 Func<Person, PersonViewModel> CreatingMethod = null;
}

public static class PersonsViewModelCreator {
 public static readonly PersonsViewModel PersonsViewModel;
 static PersonsViewModelCreator() {
   PersonsViewModel = new PersonsViewModel(PersonsCreator.Persons, CreatingMethod);
 }

 static PersonViewModel CreatingMethod(Person person) {
   foreach(PersonViewModel personViewModel in PersonViewModelCreator.PersonViewModel)
   if(personViewModel.Person == person) return personViewModel;
   return new PersonViewModel(person);
 }

}

```

<p>The following code sets a DataContext for a view.</p>

```xml
<UserControl x:Class="DXBarsAndMVVM.Views.PersonsView"
            xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
            xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
            xmlns:dx="http://schemas.devexpress.com/winfx/2008/xaml/core"
            xmlns:dDx="http://schemas.devexpress.com/winfx/2008/xaml/core"
            xmlns:dxb="http://schemas.devexpress.com/winfx/2008/xaml/bars"
            xmlns:hlp="clr-namespace:DXBarsAndMVVM.Helpers"
            xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
            xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
            mc:Ignorable="d dDx"
            dDx:ThemeManager.ThemeName="Office2007Silver"
            d:DesignHeight="200" d:DesignWidth="200" d:DataContext="{x:Static hlp:PersonsViewModelCreator.PersonsViewModel}"
            >
 <!--...-->
</UserControl>
```

<p>6. Create a command class that implements the ICommand interface</p>

```cs
public class PersonsCommand : ICommand {
 public event EventHandler CanExecuteChanged {
   add { if(canExecute != null) canExecuteChanged += value; }
   remove { if(canExecute != null) canExecuteChanged -= value; }
 }

 public PersonsCommand(Action<object> execute) : 
   this(execute, null) {
 }

 public PersonsCommand(Action<object> execute, Func<object, bool> canExecute) {
   if(execute == null)
     throw new ArgumentNullException("execute");
   this.execute = execute;
   this.canExecute = canExecute;
 }

 public bool CanExecute(object parameter) {
   if(canExecute == null) return true;
   return canExecute(parameter);
 }

 public void Execute(object parameter) {
   execute(parameter);
 }

 public void RaiseCanExecuteChanged() {
   if(canExecuteChanged != null) canExecuteChanged(this, EventArgs.Empty);
 }

 Action<object> execute;

 Func<object, bool> canExecute;

 event EventHandler canExecuteChanged;

}
```

<p>Create commands that are used to work with the Person collection:</p>

```cs
public class PersonsViewModel : LockableCollection<PersonViewModel> {
 public PersonsCommand NewPersonCommand { get; private set; }
 public PersonsCommand DeletePersonCommand { get; private set; }
 //...
 public PersonsViewModel(Persons persons) : this(persons, null) { }
 internal PersonsViewModel(Persons persons, Func<Person, PersonViewModel> creatingMethod) {
   NewPersonCommand = new PersonsCommand(OnNewPersonCommadExecute);
   DeletePersonCommand = new PersonsCommand(OnDeletePersonCommadExecute, OnDeletePersonCommadCanExecute);
   //...
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
 //...
}
```

<p>7. Add visual elements (BarButtonItems) to the form and bind them to corresponding commands.</p>

```xml
<dxb:BarManager>
 <dxb:BarManager.Items>
   <dxb:BarButtonItem Name="btNew" Content="New" Glyph="/DXBarsAndMVVM;component/Images/Icons/new-16x16.png" Command="{Binding NewPersonCommand}"/>
   <dxb:BarButtonItem Name="btDelete" Content="Delete" Glyph="/DXBarsAndMVVM;component/Images/Icons/close-16x16.png" Command="{Binding DeletePersonCommand}"/>
 </dxb:BarManager.Items>
 <dxb:BarManager.Bars>
   <dxb:Bar>
     <dxb:Bar.ItemLinks>
       <dxb:BarButtonItemLink BarItemName="btNew"/>
       <dxb:BarButtonItemLink BarItemName="btDelete"/>
     </dxb:Bar.ItemLinks>
   </dxb:Bar>
 </dxb:BarManager.Bars>
 <!--...-->
</dxb:BarManager>
```

<p><strong>Step 4. Building GUI</strong></p>
<p>1. Add DXTabControl with two tabs to the main window. The first tab will allow a end-user to select a record (a Person object) while the second tab will be used to edit selected record's properties:</p>

```xml
<Window x:Class="DXBarsAndMVVM.MainWindow"
       xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
       xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
       xmlns:dx="http://schemas.devexpress.com/winfx/2008/xaml/core"
       xmlns:dDx="http://schemas.devexpress.com/winfx/2008/xaml/core"
       xmlns:dxb="http://schemas.devexpress.com/winfx/2008/xaml/bars"
       xmlns:vw="clr-namespace:DXBarsAndMVVM.Views"
       xmlns:hlp="clr-namespace:DXBarsAndMVVM.Helpers"
       xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
       mc:Ignorable="dDx" dDx:ThemeManager.ThemeName="Office2007Silver" 
       Title="MainWindow" Height="350" Width="525" DataContext="{x:Static hlp:PersonsViewModelCreator.PersonsViewModel}">
 <dx:DXTabControl SelectedIndex="0">
   <dx:DXTabControl.View>
     <dx:TabControlMultiLineView HeaderLocation="Bottom"/>
   </dx:DXTabControl.View>
   <dx:DXTabItem Content="{Binding}" Header="Persons">
     <dx:DXTabItem.ContentTemplate>
       <DataTemplate>
         <vw:PersonsView/>
       </DataTemplate>
     </dx:DXTabItem.ContentTemplate>
   </dx:DXTabItem>
   <dx:DXTabItem Content="{Binding Selected}" Header="{Binding Selected}">
      <dx:DXTabItem.HeaderTemplate>
        <DataTemplate>
         <StackPanel Orientation="Horizontal">
            <TextBlock Text="Details:"/>
            <TextBlock Text="{Binding FirstName}" Margin="5,0,0,0"/>
            <TextBlock Text="{Binding LastName}" Margin="5,0,0,0"/>
          </StackPanel>
        </DataTemplate>
      </dx:DXTabItem.HeaderTemplate>
     <dx:DXTabItem.ContentTemplate>
        <DataTemplate>
          <vw:PersonView/>
        </DataTemplate>
      </dx:DXTabItem.ContentTemplate>
    </dx:DXTabItem>
  </dx:DXTabControl>
</Window>
```

<p>2. Tabs contain PersonsView and PersonView objects. Each view defines its own BarManager with a Bar. So, when switching between tabs, you will see a bar within a tab page. It is handy to have a single bar at the top of the main form instead. This can be implemented by using DXBars merging features.<br /> To accomplish this, add BarManager with a bar at the top of the main window.</p>

```xml
<dxb:BarManager>
 <dxb:BarManager.Bars>
   <dxb:Bar x:Name="MainBar" Caption="Bar"/>
 </dxb:BarManager.Bars>
 <!--...-->
</dxb:BarManager>
```

<p>Here is the complete code of the main window:</p>

```xml
<Window x:Class="DXBarsAndMVVM.MainWindow"
       xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
       xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
       xmlns:dx="http://schemas.devexpress.com/winfx/2008/xaml/core"
       xmlns:dDx="http://schemas.devexpress.com/winfx/2008/xaml/core"
       xmlns:dxb="http://schemas.devexpress.com/winfx/2008/xaml/bars"
       xmlns:vw="clr-namespace:DXBarsAndMVVM.Views"
       xmlns:hlp="clr-namespace:DXBarsAndMVVM.Helpers"
       xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
       mc:Ignorable="dDx" dDx:ThemeManager.ThemeName="Office2007Silver" 
       Title="MainWindow" Height="350" Width="525" DataContext="{x:Static hlp:PersonsViewModelCreator.PersonsViewModel}">
 <dxb:BarManager>
   <dxb:BarManager.Bars>
     <dxb:Bar x:Name="MainBar" Caption="Bar"/>
   </dxb:BarManager.Bars>
   <dx:DXTabControl SelectedIndex="0">
     <dx:DXTabControl.View>
       <dx:TabControlMultiLineView HeaderLocation="Bottom"/>
     </dx:DXTabControl.View>
     <dx:DXTabItem Content="{Binding}" Header="Persons">
       <dx:DXTabItem.ContentTemplate>
         <DataTemplate>
           <vw:PersonsView Loaded="PersonsView_Loaded"/>
         </DataTemplate>
       </dx:DXTabItem.ContentTemplate>
     </dx:DXTabItem>
     <dx:DXTabItem Content="{Binding Selected}" Header="{Binding Selected}">
       <dx:DXTabItem.HeaderTemplate>
         <DataTemplate>
           <StackPanel Orientation="Horizontal">
             <TextBlock Text="Details:"/>
             <TextBlock Text="{Binding FirstName}" Margin="5,0,0,0"/>
             <TextBlock Text="{Binding LastName}" Margin="5,0,0,0"/>
           </StackPanel>
        </DataTemplate>
      </dx:DXTabItem.HeaderTemplate>
      <dx:DXTabItem.ContentTemplate>
         <DataTemplate>
          <vw:PersonView Loaded="PersonView_Loaded"/>
         </DataTemplate>
       </dx:DXTabItem.ContentTemplate>
     </dx:DXTabItem>
   </dx:DXTabControl>
 </dxb:BarManager>
</Window>
```

<p>The code-behind class:</p>

```cs
public partial class MainWindow : Window {
 public MainWindow() {
   InitializeComponent();
 }

 void PersonsView_Loaded(object sender, RoutedEventArgs e) {
   PersonsView personsView = (PersonsView)sender;
   MainBar.UnMerge();
   MainBar.Merge(personsView.ChildBar);
 }

 void PersonView_Loaded(object sender, RoutedEventArgs e) {
   PersonView personView = (PersonView)sender;
   MainBar.UnMerge();
   MainBar.Merge(personView.ChildBar);
 }

}
```

<p><strong>Conclusion<br /> </strong>MVVM provides a flexible way to write complex GUI systems. This tutorial helps you understand the basic principles of writing applications by using the MVVM pattern. The use of the DXBars component will help you add an efficient navigation UI to your applications.</p>
