using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DaysGoneModManager.Helpers;
using MaterialDesignThemes.Wpf;
using PropertyChanged;

namespace DaysGoneModManager.Views
{
    public interface IBaseView
    {
        void CloseView();
        ViewMenuData ViewMenuData { get; set; }
    }

    [AddINotifyPropertyChangedInterface]
    public abstract class BaseView : UserControl, IBaseView
    {
        public virtual void CloseView()
        {

        }

        public abstract ViewMenuData ViewMenuData { get; set; }
    }

    public record ViewMenuData
    {
        public int ViewIndex { get; set; }
        public string ViewLabel { get; set; }
        public PackIconKind ViewIcon { get; set; }
        public AppConstants.ViewTypes ViewType { get; set; }
    }

}
