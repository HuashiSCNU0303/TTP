using System;
using System.Collections.Generic;
using System.Text;

namespace TTP.Services
{
    public interface IToastService
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
