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

        private Dictionary<string, Page> pages = new Dictionary<string, Page>();

        public void Navigate(Page page)
        {
            string name = page.GetType().Name;
            pages[name] = pages.ContainsKey(name) ? pages[name] : page;
            CurrentPage = pages[name];
        }

    }
}
