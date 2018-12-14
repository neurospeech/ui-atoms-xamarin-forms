using System;
using System.Linq;
using System.Collections.Generic;
using NeuroSpeech.UIAtoms.DI;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.DI
{

    public class NavigationQueue {

        private Queue<NavigationItem> queue = new Queue<NavigationItem>();

        public NavigationQueue(string uri)
        {
            foreach (var item in uri.Split(new char[] { '/' })) {
                queue.Enqueue(new NavigationItem(item));
            }
        }

        public NavigationItem Top {
            get {
                if (queue.Count == 0)
                    return null;
                return queue.Peek();
            }
        }

        public NavigationItem Take() {
            return queue.Dequeue();
        }

    }

}