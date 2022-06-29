using System;
using System.Threading.Tasks;
using T1098897.ViewModels;

namespace T1098897.Services {
    public interface INavigationService {
        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;

        Task GoBackAsync();
    }
}
