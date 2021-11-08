using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaysGoneModManager.Helpers;
using DaysGoneModManager.Views;
using PropertyChanged;

namespace DaysGoneModManager.Services
{
    public interface IControllerService
    {
        IBaseView CurrentView { get; }
        void InitializeService(IEnumerable<IBaseView> views);
        void SetView(IBaseView view);
        void CloseViews();
        IEnumerable<IBaseView> GetViews();
        void StartAtHome();
        void LastView();
        object GetLastView();
        event EventHandler ViewChanged;
    }

    [AddINotifyPropertyChangedInterface]
    public class ControllerService : IControllerService
    {
        public event EventHandler ViewChanged;

        public IBaseView CurrentView { get; private set; }
        ObservableCollection<IBaseView> ViewCollection { get; set; }

        public void InitializeService(IEnumerable<IBaseView> views)
        {
            ViewCollection = new ObservableCollection<IBaseView>();
            ViewCollection.AddRange(views.OrderBy(v => v.ViewMenuData.ViewIndex).ToArray());
        }

        public void SetView(IBaseView view)
        {
            if (CurrentView != view)
            {
                CurrentView = view;
                ViewChanged(this, EventArgs.Empty);
            }
        }

        public void CloseViews() { foreach (var view in ViewCollection) view.CloseView(); }

        public IEnumerable<IBaseView> GetViews() => ViewCollection;
        public void StartAtHome() => CurrentView = ViewCollection.FirstOrDefault();
        public void LastView()
        {
            CurrentView = ViewCollection.LastOrDefault();
        }

        public object GetLastView()
        {
            return ViewCollection.LastOrDefault();
        }
    }

}

