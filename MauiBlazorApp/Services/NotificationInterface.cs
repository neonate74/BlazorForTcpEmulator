using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorApp.Services
{
    public interface INotification
    {
        public void Subscribe(EventHandler evt);

        public void Unsubscribe(EventHandler evt);

        public void Publish();
    }
}
