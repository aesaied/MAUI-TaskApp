using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Model;

namespace TaskApp.Helpers
{
    public class TaskHelper
    {
        private readonly ApiHelper _apiHelper;
        public TaskHelper(ApiHelper apiHelper)
        {
            _apiHelper= apiHelper;
        }


        public async Task<ObservableCollection<TaskListItemDto>?> GetAllTasks()
        {
            var httpClient = await _apiHelper.GetHttpClient();

            var result = await httpClient.GetAsync("/api/tasks");

            if (result.IsSuccessStatusCode)
            {
              var  jsonData= await result.Content.ReadAsStringAsync();

                var tasks =  JsonConvert.DeserializeObject<ObservableCollection<TaskListItemDto>>(jsonData);

                return tasks;
            }

            return null;

        }


        public async Task<bool> AddTask(TaskInputDto task)
        {
            var cansToken = new CancellationToken();

            var httpClient = await _apiHelper.GetHttpClient();
            var result = await httpClient.PostAsJsonAsync("/api/Tasks", task, cansToken);

            if (result.IsSuccessStatusCode)
            {
                var json = await result.Content.ReadAsStringAsync();
                var taskResult = JsonConvert.DeserializeObject<TaskInputDto>(json);

                task.Id = taskResult.Id;
               

                return true;
            }

            else
            {
                await result!.ViewErrors();
               
            }

            return false;


        }

        public async Task<bool> DeleteTask(Guid id)
        {
           

            HttpClient httpClient= await _apiHelper.GetHttpClient();

           var result=  await httpClient.DeleteAsync($"api/tasks/{id}");

            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            else
            {
                await result!.ViewErrors();

            }

            return false;

        }
    }
}
