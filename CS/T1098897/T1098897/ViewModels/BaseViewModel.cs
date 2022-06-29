﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using T1098897.Models;
using T1098897.Services;
using Xamarin.Forms;

namespace T1098897.ViewModels {
    public class BaseViewModel : INotifyPropertyChanged {
        bool isBusy = false;
        string title = string.Empty;

        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        public INavigationService Navigation => DependencyService.Get<INavigationService>();

        public bool IsBusy {
            get { return this.isBusy; }
            set { SetProperty(ref this.isBusy, value); }
        }

        public string Title {
            get { return this.title; }
            set { SetProperty(ref this.title, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual Task InitializeAsync(object parameter) {
            return Task.CompletedTask;
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null) {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
