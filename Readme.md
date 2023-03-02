<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128641144/22.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E3341)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# MVVM Application with WPF Bars

This example demonstrates how to use [WPF Bars](https://docs.devexpress.com/WPF/6194/controls-and-libraries/ribbon-bars-and-menu/bars) in an MVVM application. Bars are bound to commands that allow you to add and remove items from the data source and edit item properties:

![image](https://user-images.githubusercontent.com/65009440/222427680-3b751290-7d8d-437a-8edd-0c4fa9f79831.png)

## Implementation Details

### Add Button

The **Add** button is the [BarButtonItem](https://docs.devexpress.com/WPF/DevExpress.Xpf.Bars.BarButtonItem) object bound to the `AddIssue` View Model command:

![image](https://user-images.githubusercontent.com/65009440/222439722-624fd482-4ae7-4ed9-ac8f-0df9c732ccee.png)

```xaml
<dxb:BarButtonItem x:Name="addItem" 
                   Content="Add" 
                   Command="{Binding AddIssueCommand}" 
                   .../>
```

```cs
[Command]
public void AddIssue() {
    int newId = Issues.Count == 0 ? 0 : Issues.Max(p => p.Id) + 1;
    Issue issue = new Issue() { Id = newId, Subject = "New Issue " + newId, 
                                Completed = false, Priority = Priority.Low };
    Issues.Add(issue);
}
```

The `Issues` property is the content control's `ItemsSource`.

> **Note**
>
> You can also use the [DialogService](https://docs.devexpress.com/WPF/17467/mvvm-framework/services/predefined-set/dialog-services/dialogservice) to invoke a dialog when a user clicks the **Add** button. For example, to allow users to specify the created item properties.

### Remove Button

The **Remove** button is the [BarButtonItem](https://docs.devexpress.com/WPF/DevExpress.Xpf.Bars.BarButtonItem) object bound to the `RemoveIssue` View Model command:

![image](https://user-images.githubusercontent.com/65009440/222439820-4292e9b5-7dab-4694-9087-93f461e4b9ed.png)

```xaml
<dxb:BarButtonItem x:Name="removeItem" 
                   Content="Remove" 
                   Command="{Binding RemoveIssueCommand}" 
                   .../>
```

```cs
[Command]
public void RemoveIssue() {
    Issues.Remove(SelectedIssue);
}
public bool CanRemoveIssue() {
    return SelectedIssue != null;
}
```

The `SelectedIssue` is a View Model property bound to the content control's `SelectedItem` property ([ListBoxEdit.SelectedItem](https://docs.devexpress.com/WPF/DevExpress.Xpf.Editors.ListBoxEdit.SelectedItem) in this example).

### Completed Button

The **Completed** button is the [BarCheckItem](https://docs.devexpress.com/WPF/DevExpress.Xpf.Bars.BarCheckItem) object that toggles the selected item's `Completed` property:

![image](https://user-images.githubusercontent.com/65009440/222439913-af88d620-5ccb-489b-bd77-721feed5f266.png)

```xaml
<dxb:BarCheckItem x:Name="completed" Content="Completed" 
                  IsChecked="{Binding SelectedIssue.Completed}"
                  IsEnabled="{DXBinding 'SelectedIssue != null'}"
                  .../>
```

### Priority Button

The **Priority** button is the [BarSubItem](https://docs.devexpress.com/WPF/DevExpress.Xpf.Bars.BarSubItem) object that contains [BarCheckItems](https://docs.devexpress.com/WPF/DevExpress.Xpf.Bars.BarCheckItem). This button allows you to specify the selected item's `Priority` property value:

![image](https://user-images.githubusercontent.com/65009440/222431938-411e4b7d-a611-46e8-a4a0-af0aed612696.png)

```xaml
<dxb:BarSubItem x:Name="priority" Content="Priority" 
                IsEnabled="{DXBinding 'SelectedIssue != null'}">
    <dxb:BarCheckItem Content="Low" GroupIndex="0" Background="Green" Foreground="White"
                      Command="{DXCommand Execute='SelectedIssue.Priority = $local:Priority.Low'}"
                      IsChecked="{DXBinding 'SelectedIssue.Priority == $local:Priority.Low', Mode=OneWay}"/>
    <dxb:BarCheckItem Content="Normal" GroupIndex="0" Background="Orange" Foreground="White"
                      Command="{DXCommand Execute='SelectedIssue.Priority = $local:Priority.Normal'}"
                      IsChecked="{DXBinding 'SelectedIssue.Priority == $local:Priority.Normal', Mode=OneWay}"/>
    <dxb:BarCheckItem Content="High" GroupIndex="0"  Background="Red" Foreground="White"
                      Command="{DXCommand Execute='SelectedIssue.Priority = $local:Priority.High'}"
                      IsChecked="{DXBinding 'SelectedIssue.Priority == $local:Priority.High', Mode=OneWay}"/>
</dxb:BarSubItem>
```

Refer to the following help topics for more information: [DXBinding](https://docs.devexpress.com/WPF/115771/mvvm-framework/dxbinding/dxbinding), [DXCommand](https://docs.devexpress.com/WPF/115776/mvvm-framework/dxbinding/dxcommand), and [Language Specification](https://docs.devexpress.com/WPF/115777/mvvm-framework/dxbinding/language-specification).

### Tags Selector

The **Tags** selector is the [BarEditItem](https://docs.devexpress.com/WPF/DevExpress.Xpf.Bars.BarEditItem) object with the embedded [Checked Token Combo Box](https://docs.devexpress.com/WPF/DevExpress.Xpf.Editors.CheckedTokenComboBoxStyleSettings). This element allows you to assign multiple tags to the selected item:

![image](https://user-images.githubusercontent.com/65009440/222433054-d4d4e32b-7165-4bb8-944d-2d80a80a24bd.png)

```xaml
<dxb:BarEditItem x:Name="tags" Content="Tags: " 
                 EditValue="{Binding SelectedIssue.Tags}" 
                 IsVisible="{DXBinding 'SelectedIssue != null'}">
    <dxb:BarEditItem.EditSettings>
        <dxe:ComboBoxEditSettings ItemsSource="{dxe:EnumItemsSource EnumType={x:Type local:Tag}}">
            <dxe:ComboBoxEditSettings.StyleSettings>
                <dxe:CheckedTokenComboBoxStyleSettings 
                    AllowEditTokens="False" 
                    FilterOutSelectedTokens="False" 
                    NewTokenPosition="None"/>
            </dxe:ComboBoxEditSettings.StyleSettings>
        </dxe:ComboBoxEditSettings>
    </dxb:BarEditItem.EditSettings>
</dxb:BarEditItem>
```

The [BarEditItem.EditValue](https://docs.devexpress.com/WPF/DevExpress.Xpf.Bars.BarEditItem.EditValue) property is bound to the `Tags` property of the `List<object>` type. In this case, the Combo Box with multiple item selection can post selected values to the data source. Refer to the following help topic for more information: [Implement multi-select in DevExpress WPF Data Editors](https://supportcenter.devexpress.com/ticket/details/t889444/how-to-implement-multi-select-when-using-devexpress-wpf-data-editors-comboboxedit).

### Item Context Menu

Each item contains the context menu that duplicates the listed actions. To create this menu, add bar item links to the [PopupMenu](https://docs.devexpress.com/WPF/DevExpress.Xpf.Bars.PopupMenu)'s `Items` collection and assign this menu to the [BarManager.DXContextMenu](https://docs.devexpress.com/WPF/DevExpress.Xpf.Bars.BarManager.DXContextMenu) attached property:

![image](https://user-images.githubusercontent.com/65009440/222435918-14b45416-6af1-42aa-9329-c27884450b78.png)

```xaml
<dxe:ListBoxEdit.ItemTemplate>
    <DataTemplate>
        <StackPanel Orientation="Horizontal">
            <dxe:ComboBoxEdit>
                <!-- ... -->
            </dxe:ComboBoxEdit>
            <TextBlock ... />

            <dxb:BarManager.DXContextMenu>
                <dxb:PopupMenu>
                    <dxb:BarButtonItemLink BarItemName="addItem"/>
                    <dxb:BarButtonItemLink BarItemName="removeItem"/>
                    <dxb:BarItemLinkSeparator/>
                    <dxb:BarCheckItemLink BarItemName="completed"/>
                    <dxb:BarItemLinkSeparator/>
                    <dxb:BarSubItemLink BarItemName="priority"/>
                    <dxb:BarItemLinkSeparator/>
                    <dxb:BarEditItemLink BarItemName="tags"/>
                </dxb:PopupMenu>
            </dxb:BarManager.DXContextMenu>

        </StackPanel>
    </DataTemplate>
</dxe:ListBoxEdit.ItemTemplate>
```

## Files to Review

* [Data.cs](./CS/Bars_in_MVVM_Application/Data.cs) (VB: [Data.vb](./VB/Bars_in_MVVM_Application/Data.vb))
* [ViewModel.cs](./CS/Bars_in_MVVM_Application/ViewModel.cs) (VB: [ViewModel.vb](./VB/Bars_in_MVVM_Application/ViewModel.vb))
* [MainWindow.xaml](./CS/Bars_in_MVVM_Application/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/Bars_in_MVVM_Application/MainWindow.xaml))

## Documentation

* [DevExpress MVVM Framework](https://docs.devexpress.com/WPF/15112/mvvm-framework)
* [List of Bar Items and Links](https://docs.devexpress.com/WPF/6646/controls-and-libraries/ribbon-bars-and-menu/common-concepts/the-list-of-bar-items-and-links)
* [Menus](https://docs.devexpress.com/WPF/115388/controls-and-libraries/ribbon-bars-and-menu/menus)
