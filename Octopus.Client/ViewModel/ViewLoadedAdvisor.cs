using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows;

namespace Octopus.Client.ViewModel
{
    public static class ViewLoadedAdvisor
    {
        internal static IViewModel GetViewModel(DependencyObject obj)
        {
            return (IViewModel)obj.GetValue(ViewModelProperty);
        }

        internal static void SetViewModel(DependencyObject obj, IViewModel value)
        {
            obj.SetValue(ViewModelProperty, value);
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.RegisterAttached("ViewModel",
            typeof(IViewModel), 
            typeof(ViewLoadedAdvisor), 
            new PropertyMetadata(null, OnViewModelChanged));

        private static void OnViewModelChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            IViewModel oldVM = args.OldValue as IViewModel;
            if (oldVM != null)
            {
                oldVM.NotifyUnloaded();
            }

            IViewModel newVM = args.NewValue as IViewModel;
            if (newVM != null)
            {
                newVM.NotifyLoaded();
            }
        }
    }
}
