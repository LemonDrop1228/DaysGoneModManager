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
        void InitializeViews(IEnumerable<IBaseView> views);
        void SetView(IBaseView view);
        void CloseViews();
        IEnumerable<IBaseView> GetViews();
        void StartAtHome();
    }

    [AddINotifyPropertyChangedInterface]
    public class ControllerService : IControllerService
    {
        public IBaseView CurrentView { get; private set; }
        ObservableCollection<IBaseView> ViewCollection { get; set; }

        public void InitializeViews(IEnumerable<IBaseView> views)
        {
            ViewCollection = new ObservableCollection<IBaseView>();
            ViewCollection.AddRange(views.OrderBy(v => v.ViewMenuData.ViewIndex).ToArray());
        }

        public void SetView(IBaseView view) => CurrentView = view;

        public void CloseViews() { foreach (var view in ViewCollection) view.CloseView(); }

        public IEnumerable<IBaseView> GetViews() => ViewCollection;
        public void StartAtHome() => CurrentView = ViewCollection.FirstOrDefault();
    }

}

