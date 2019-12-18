using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using NeuroSpeech.UIAtoms.Controls;
using Xamarin.Forms.Platform.Android;
using Android.Views.InputMethods;
using static Android.Widget.TextView;

namespace NeuroSpeech.UIAtoms.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class AtomFieldRenderer
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="convertView"></param>
        /// <param name="parent"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IDisposable BindKeyboardActions(Xamarin.Forms.View content)
        {
            if ( content != null && !(content is Editor))
            {

                var textField = Platform.GetRenderer(content)?.GetPropertyValue("Control") as EditText;
                if (textField != null)
                {

                    var next = AtomForm.GetFocusNext(content);


                    textField.ImeOptions = next is AtomSubmitButton ? ImeAction.Go : ImeAction.Next;

                    EventHandler<EditorActionEventArgs> editorAction = (s, e) => {


                        if (e.ActionId == ImeAction.Next
                        ||
                        e.ActionId == ImeAction.Go) {


                            var nextField = Platform.GetRenderer(next)?.GetPropertyValue("Control") as Android.Views.View;

                            var button = nextField as Android.Widget.Button;
                            if (button != null)
                            {
                                button.CallOnClick();

                                // hide keyboard...

                                HideKeyboard(textField);

                                return;
                            }

                            var tf = nextField as EditText;
                            if (tf != null) {
                                tf.RequestFocus();
                                return;
                            }

                            HideKeyboard(textField);


                        }
                    };

                    textField.EditorAction += editorAction;

                    return new AtomDisposableAction(() => {
                        textField.EditorAction -= editorAction;
                    });

                }

            }

            return new AtomDisposableAction(() => {
            
            });

        }

        private static void HideKeyboard(EditText textField)
        {
            InputMethodManager imm = (InputMethodManager)textField.Context.GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(textField.WindowToken, 0);
        }
    }
}