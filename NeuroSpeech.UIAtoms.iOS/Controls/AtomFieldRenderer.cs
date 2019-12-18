﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform;
using NeuroSpeech.UIAtoms.Controls;
using UIKit;


namespace NeuroSpeech.UIAtoms.Controls
{
    public class AtomFieldRenderer
    {


        public static IDisposable BindKeyboardActions(Xamarin.Forms.View content)
        {

            if (content != null && !(content is Editor)) {

                var textField = Platform.GetRenderer(content)?.NativeView?.GetPropertyValue("Control") as UITextField;
                if (textField != null) {

                    var next = AtomForm.GetFocusNext(content);

                    var submitButton = next as AtomSubmitButton;
                    
                    textField.ReturnKeyType = submitButton != null ? UIReturnKeyType.Go : UIReturnKeyType.Next;


                    textField.ShouldReturn = (tf) => {

                        
                        var nextField = Platform.GetRenderer(next)?.NativeView?.GetPropertyValue("Control") as UIView;
                        if (nextField == null) {
                            tf.ResignFirstResponder();
                            return false;
                        }

                        var cell = nextField.FindParent<UITableViewCell>();

                        cell?.BecomeFirstResponder();

                        //var button = nextField as UIButton;
                        if (submitButton != null) {
                            tf.ResignFirstResponder();
                            //button.SendActionForControlEvents(UIControlEvent.TouchUpInside);
                            submitButton.OnSubmitCommand();
                            return false;
                        }

                        var txt = nextField as UITextField;
                        if (txt != null) {
                            tf.ResignFirstResponder();
                            txt.BecomeFirstResponder();
                        }

                        return false;
                    };

                    new AtomDisposableAction(() => {
                        textField.ShouldReturn = null;
                    });
                }

            }

            return new AtomDisposableAction();
        }

    }
}
