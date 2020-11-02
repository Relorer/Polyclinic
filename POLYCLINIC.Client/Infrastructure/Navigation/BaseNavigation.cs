using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace POLYCLINIC.Client.Infrastructure
{
    public abstract class BaseNavigation
    {
        public event PropertyChangedEventHandler CurrentPageChanged;

        private Page currentPage;
        public Page CurrentPage
        {
            get
            {
                return currentPage;
            }
            set
            {
                currentPage = value;
                CurrentPageChanged?.Invoke(null, new PropertyChangedEventArgs("CurrentPage"));
            }
        }

        public void Navigate(Page page)
        {
            CurrentPage = page;
        }

    }
}
