using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Model;

namespace TaskApp.Extensions
{
    public class TaskDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CompleteTemplete { get; set; }
        public DataTemplate NotCompleteTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
             var task = (TaskListItemDto) item;

            if (task != null && task.IsComplete)
            {
                return CompleteTemplete;

            }
            return NotCompleteTemplate;
        }
    }
}
