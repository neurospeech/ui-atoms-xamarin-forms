using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class AtomSubmitButton: Button
    {


        /// <summary>
        /// 
        /// </summary>
        public AtomSubmitButton()
        {
            this.Command = new AtomCommand(OnSubmitCommand);
        }

        internal Task OnSubmitCommand()
        {
            AtomForm form = this.GetParentOfType<AtomForm>();

            var af = form?.ValidateCommand;

            af?.Execute(this.CommandParameter);
            return Task.CompletedTask;
        }
    }
}
