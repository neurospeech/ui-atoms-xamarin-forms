using NeuroSpeech.UIAtoms.DI;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuroSpeech.UIAtoms.Pages
{
    public class CheckBoxListPageViewModel: AtomViewModel, IPageResultViewModel<string>
    {

        public PageResult<string> PageResult { get; set; }

        public CheckBoxListPageViewModel()
        {

        }

    }
}
