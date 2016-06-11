using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMenuApplication1
{
    public class MainViewModel
    {
        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
        public MainViewModel()
        {
            MenuItems = new ObservableCollection<MenuItemViewModel>() {
                new MenuItemViewModel {
                    Header = "Menu A",
                    ChildMenuItems = new ObservableCollection<MenuItemViewModel>
                    {
                        new MenuItemViewModel { Header = "Sub Menu A-1", Icon="icons/aquarius.png" }
                    }
                },
                new MenuItemViewModel {
                    Header = "Menu B",
                    ChildMenuItems = new ObservableCollection<MenuItemViewModel>
                    {
                        new MenuItemViewModel { Header = "Sub Menu B-1", Icon="icons/aries.png" }
                    }
                },
            };

        }
    }
}
