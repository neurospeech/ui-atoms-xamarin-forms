using System;
using System.Collections.Generic;
using System.Text;

namespace NeuroSpeech.UIAtoms.Pages
{
    public class ListViewModel: AtomViewModel
    {

        #region Property Items

        private System.Collections.IEnumerable _Items = null;

        public System.Collections.IEnumerable Items
        {
            get
            {
                return _Items;
            }
            set
            {
                SetProperty(ref _Items, value);
            }
        }
        #endregion

        #region Property ItemTemplate

        private object _ItemTemplate = null;

        public object ItemTemplate
        {
            get
            {
                return _ItemTemplate;
            }
            set
            {
                SetProperty(ref _ItemTemplate, value);
            }
        }
        #endregion

        #region Property IsGroupingEnabled

        private bool _IsGroupingEnabled = false;

        public bool IsGroupingEnabled
        {
            get
            {
                return _IsGroupingEnabled;
            }
            set
            {
                SetProperty(ref _IsGroupingEnabled, value);
            }
        }
        #endregion

        #region Property GroupDisplayBinding

        private object _GroupDisplayBinding = null;

        public object GroupDisplayBinding
        {
            get
            {
                return _GroupDisplayBinding;
            }
            set
            {
                SetProperty(ref _GroupDisplayBinding, value);
            }
        }
        #endregion

        #region Property GroupHeaderTemplate

        private object _GroupHeaderTemplate = null;

        public object GroupHeaderTemplate
        {
            get
            {
                return _GroupHeaderTemplate;
            }
            set
            {
                SetProperty(ref _GroupHeaderTemplate, value);
            }
        }
        #endregion

        #region Property GroupShortNameBinding

        private object _GroupShortNameBinding = null;

        public object GroupShortNameBinding
        {
            get
            {
                return _GroupShortNameBinding;
            }
            set
            {
                SetProperty(ref _GroupShortNameBinding, value);
            }
        }
        #endregion

        #region Property SearchText

        private string _SearchText = null;

        public string SearchText
        {
            get
            {
                return _SearchText;
            }
            set
            {
                SetProperty(ref _SearchText, value, onChanged: OnSearchTextChanged);
            }
        }

        #endregion

        #region Property AllowMultipleSelection

        private bool _AllowMultipleSelection = false;

        public bool AllowMultipleSelection
        {
            get
            {
                return _AllowMultipleSelection;
            }
            set
            {
                SetProperty(ref _AllowMultipleSelection, value);
            }
        }
        #endregion



        protected virtual void OnSearchTextChanged()
        {
            
        }


    }
}
