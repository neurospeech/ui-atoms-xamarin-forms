using NeuroSpeech.UIAtoms.DI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSpeech.UIAtoms
{
    public interface IAtomViewModel
    {

        bool IsBusy { get; }

       

    }

    public interface IAtomViewLifeCycleModel {

        void OnAppearing();

        void OnDisappearing();

        void OnRemoved();

    }

    public interface IAtomNavigationAware
    {

        Task OnNavigatedAsync(NavigationItem current, NavigationQueue queue);
    }
}
