using POLYCLINIC.Data.Entities;
using System.ComponentModel;
using System.Windows.Controls;

namespace POLYCLINIC.Client.Interfaces
{
    public interface INavigation
    {
        event PropertyChangedEventHandler CurrentPageChanged;

        Page CurrentPage { get; set; }

        void Navigate(Page page);
    }
}
