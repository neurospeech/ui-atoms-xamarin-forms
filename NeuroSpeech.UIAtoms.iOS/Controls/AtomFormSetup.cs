using NeuroSpeech.UIAtoms.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency(typeof(AtomFormSetup))]

namespace NeuroSpeech.UIAtoms.Controls
{
    internal class AtomFormSetup : BaseAtomFormSetup
    {
        internal override void SetupNext(View current, View next)
        {

            UITextField cn = GetNative(current) as UITextField;
            if (cn == null)
                return;


            UIView nn = GetNative(next);
            if (nn == null) {
                cn.ReturnKeyType = UIReturnKeyType.Next;
                cn.ShouldReturn = (tf) => {
                    tf.ResignFirstResponder();
                    return false;
                };
                return;
            }



            var tableCell = cn.FindParent<UITableViewCell>();



            cn.ReturnKeyType = UIReturnKeyType.Next;
            cn.ShouldReturn = (tf) => {

                tableCell?.BecomeFirstResponder();
                nn?.BecomeFirstResponder();

                return false;
            };

            cn.ReloadInputViews();

            

        }

        private UIView GetNative(View current)
        {
            var nv = Platform.GetRenderer(current)?.NativeView;
            if (nv == null)
                return null;
            return nv.GetPropertyValue("Control") as UIView;
        }
    }
}
