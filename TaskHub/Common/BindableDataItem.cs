using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TaskHub.Common
{
    public class BindableDataItem<T>:INotifyPropertyChanged
    {
        public T Value { get; set; }
        private string _Text;

        public string Text
        {
            get => _Text;
            set
            {
                _Text = value;
                OnPropertyChanged(nameof(_Text));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
