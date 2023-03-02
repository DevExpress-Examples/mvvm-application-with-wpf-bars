using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bars_in_MVVM_Application {
    public enum Priority {
        Low,
        Normal,
        High
    }
    public enum Tag {
        Urgent,
        NeedResearch,
        Complex,
        Easy,
        Postponed
    }
    public class Issue : BindableBase {
        public int Id { get { return GetValue<int>(); } set { SetValue(value); } }
        public string Subject { get { return GetValue<string>(); } set { SetValue(value); } }
        public Priority Priority { get { return GetValue<Priority>(); } set { SetValue(value); } }
        public bool Completed { get { return GetValue<bool>(); } set { SetValue(value); } }
        public List<object> Tags { get { return GetValue<List<object>>(); } set { SetValue(value); } }
    }
    public class IssueDataModel {
        public static ObservableCollection<Issue> GetIssues() {
            var issies = new ObservableCollection<Issue>() {
                new Issue() { Id = 0, Subject = "Will we track sales history in our system?", 
                              Priority = Priority.Normal, Completed = true, Tags = new List<object>() { Tag.Urgent, Tag.NeedResearch } },
                new Issue() { Id = 1, Subject = "What database types will we support?",
                              Priority = Priority.Low, Completed = true, Tags = new List<object>() { Tag.Complex, Tag.Postponed } },
                new Issue() { Id = 2, Subject = "We are using different paths for different modules.",
                              Priority = Priority.High, Completed = false, Tags = new List<object>() { Tag.Complex, Tag.NeedResearch } },
                new Issue() { Id = 3, Subject = "Inconsistency. Please fix it.",
                              Priority = Priority.High, Completed = false, Tags = new List<object>() { Tag.Urgent, Tag.Easy } },
                new Issue() { Id = 4, Subject = "Somebody has to look at it.",
                              Priority = Priority.Normal, Completed = false, Tags = new List<object>() { Tag.Postponed, Tag.Easy } },
            };
            return issies;
        }
    }
}
