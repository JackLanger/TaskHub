using System.Collections.ObjectModel;

namespace TaskHub.ViewModels
{
    public class TaskCardListViewModel:ModelBase
    {
        public ObservableCollection<TaskCardViewModel> Cards { get; set; }
    }
}
