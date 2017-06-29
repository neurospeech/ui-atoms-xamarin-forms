using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace NeuroSpeech.UIAtoms
{
    public static class UIViewHelper
    {


        public static UIView FindParent<T>(this UIView view)
            where T:class
        {
            if(view == null || typeof(T).IsAssignableFrom(view.GetType()))
                return view;
            return FindParent<T>(view.Superview);
        }


        public static UIView RootView(this UIView view) {
            while (view != null)
            {
                UIView parent = view.Superview;
                if (parent == null)
                    return view;
                view = view.Superview;
            }
            return null;
        }

        public static List<UIView> AllChildren(this UIView view) {
            List<UIView> views = new List<UIView>();
            FillChildren(view, views);
            return views;
        }

        private static void FillChildren(UIView view, List<UIView> views)
        {
            UIView[] children = view.Subviews;
            if (children == null || children.Length==0)
                return;
            //for (var i=children.Length-1;i>=0;i--)
            //{
            //    var v = children[i];
            //    if (views.Contains(v)) {
            //        throw new InvalidOperationException();
            //    }
            //    views.Add(v);
            //}

            //for(var i=children.Length-1;i>=0;i--) {
            //    FillChildren(children[i], views);
            //}

            foreach (var child in children) {
                views.Add(child);
            }

            foreach (var child in children) {
                FillChildren(child, views);
            }

        }
    }
}
