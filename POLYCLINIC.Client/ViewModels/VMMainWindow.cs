using POLYCLINIC.Client.Utils;
using System;
using System.Windows.Controls;

namespace POLYCLINIC.Client.ViewModels
{
    class VMMainWindow : VMBase
    {
        public Page CurrentPage
        {
            get
            {
                return MainNavigation.Instance.CurrentPage;
            }
        }
        
        public VMMainWindow()
        {
            MainNavigation.Instance.CurrentPageChanged += (sender, e) => OnPropertyChanged(e.PropertyName);
        }
        
    }
}
