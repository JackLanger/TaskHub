using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHub.ViewModels
{
    public class TaskCardListViewModel:ModelBase
    {
        public ObservableCollection<TaskCardViewModel> Cards { get; set; }
    }
}
