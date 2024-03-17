using _2048.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048.WPF.Utilities
{
    public static class Methods
    {
        public static async Task WaitAndRemoveErrorMessage(IErrorViewModel errorViewModel)
        {
            await Task.Delay(3000);
            errorViewModel.ErrorMessage = null;
        }
    }
}
