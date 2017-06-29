using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.DI
{


    public class PageResult<T>
    {

        public bool Modal { get; set; }

        public bool Animated { get; set; } = true;

        public PageResult()
        {
            nav = DependencyService.Get<IAppNavigator>(DependencyFetchTarget.GlobalInstance);
        }

        protected TaskCompletionSource<T> source = new TaskCompletionSource<T>();
        protected readonly IAppNavigator nav;

        public virtual Task<T> Task => source.Task;


        public virtual async Task FinishAsync(T result) {

            ThrowIfInvalid();
            source.SetResult(result);
            if (Modal)
            {
                await nav.PopModalAsync(Animated);
            }
            else
            {
                await nav.PopAsync(Animated);
            }
        }

        public virtual async Task CancelAsync(bool popStack = true)
        {
            if (source.Task.IsCompleted)
                return;
            ThrowIfInvalid();
            if (popStack)
            {
                if (Modal)
                {
                    await nav.PopModalAsync(Animated);
                }
                else
                {
                    await nav.PopAsync(Animated);
                }
            }
            source.SetCanceled();
        }

        private void ThrowIfInvalid()
        {
            if (source.Task.IsCompleted)
            {
                throw new InvalidOperationException("PageResult is already completed");
            }
            if (source.Task.IsCanceled)
            {
                throw new InvalidOperationException("PageResult is already cancelled");
            }
        }
    }


    public interface IPageResultViewModel<T> {

        PageResult<T> PageResult { get; set; }

    }

    
}
