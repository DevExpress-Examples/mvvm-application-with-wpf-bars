using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System.Collections.ObjectModel;
using System.Linq;

namespace Bars_in_MVVM_Application {
    public class ViewModel : ViewModelBase {
        public ViewModel() {
            Issues = IssueDataModel.GetIssues();
            SelectedIssue = Issues.FirstOrDefault();
        }
        public ObservableCollection<Issue> Issues { get { return GetValue<ObservableCollection<Issue>>(); } set { SetValue(value); } }
        public Issue SelectedIssue { get { return GetValue<Issue>(); } set { SetValue(value); } }
        [Command]
        public void AddIssue() {
            int newId = Issues.Count == 0 ? 0 : Issues.Max(p => p.Id) + 1;
            Issue issue = new Issue() { Id = newId, Subject = "New Issue " + newId, Completed = false, Priority = Priority.Low };
            Issues.Add(issue);
        }
        [Command]
        public void RemoveIssue() {
            Issues.Remove(SelectedIssue);
        }
        public bool CanRemoveIssue() {
            return SelectedIssue != null;
        }
    }
}
