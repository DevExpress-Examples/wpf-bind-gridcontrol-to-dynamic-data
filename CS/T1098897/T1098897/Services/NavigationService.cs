using DevExpress.XamarinForms.Navigation;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using T1098897.ViewModels;
using T1098897.Views;
using Xamarin.Forms;

namespace T1098897.Services {
    public class NavigationService : INavigationService {

        public async Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel {
            await InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public async Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel {
            await InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public async Task GoBackAsync() {
            INavigation navigation = GetActiveNavigation();
            await navigation.PopAsync();
        }

        async Task InternalNavigateToAsync(Type viewModelType, object parameter) {
            Page page = CreatePage(viewModelType, parameter);
            INavigation navigation = GetActiveNavigation();
            if (navigation != null)
                await navigation.PushAsync(page);
            else
                Application.Current.MainPage = page;

            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }

        Type GetPageTypeForViewModel(Type viewModelType) {
            var viewName = viewModelType.FullName.Replace("ViewModels", "Views").Replace("ViewModel", "Page");
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(
                        CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        Page CreatePage(Type viewModelType, object parameter) {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null) {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            return page;
        }

        INavigation GetActiveNavigation() {
            var navigationPage = Application.Current.MainPage as MainPage;
            var selectedPage = navigationPage?.SelectedItem as TabPageItem;
            var navigator = selectedPage?.Content as NavigationPage;
            return navigator?.Navigation;
        }
    }
}
