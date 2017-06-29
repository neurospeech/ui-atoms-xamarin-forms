using NeuroSpeech.UIAtoms.DI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSpeech.UIAtoms.Pages
{
    public class ItemSelectorPageViewModel : AtomViewModel, IPageResultViewModel<object>
    {
        public PageResult<object> PageResult { get; set; }
        public AtomCommand CancelCommand { get; private set; }
        public AtomCommand SelectCommand { get; private set; }

        public ItemSelectorPageViewModel()
        {
            this.CancelCommand = new AtomCommand(async () => await OnCancelCommandAsync());
            this.SelectCommand = new AtomCommand(async () => await OnSelectCommandAsync());
        }

        #region Property SelectedItem

        private object _SelectedItem = null;

        public object SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                SetProperty(ref _SelectedItem, value);
            }
        }
        #endregion

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

        

        private async Task OnSelectCommandAsync()
        {
            await PageResult.FinishAsync(SelectedItem);
        }

        private async Task OnCancelCommandAsync()
        {
            await PageResult.CancelAsync();
        }

        public override Task InitAsync()
        {
         

            return base.InitAsync();
        }


    }
}
