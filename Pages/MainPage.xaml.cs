
using CommunityToolkit.Maui.Alerts;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TaskApp.Helpers;
using TaskApp.Model;

namespace TaskApp.Pages;

public partial class MainPage : ContentPage
{
	private readonly TaskHelper _taskHelper;
	public MainPage(TaskHelper taskHelper)
	{
		InitializeComponent();

		_taskHelper = taskHelper;

        FillData();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
      
    }

    ObservableCollection<TaskListItemDto> TaskList { get; set; }
    private async Task FillData()
    {
        TaskList= await _taskHelper.GetAllTasks(); 
       

        // process 
		myList.ItemsSource = TaskList;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        
       await AppShell.Current.GoToAsync("ManageTask");

        if(AppShell.Current.CurrentPage is ManageTask manageTask)
        {
            manageTask.ClosedAndReturn += TaskCreated;

        }

        
        
		//
    }

    private void TaskCreated(object? e , TaskInputDto task)
    {
        TaskListItemDto item = new TaskListItemDto() { CreatedDate = DateTime.Now, Description = task.Description, Id = task.Id?? Guid.NewGuid(), IsComplete = task.IsComplete, Title = task.Title };
        TaskList.Add(item);
    }
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        var  task =   ((ImageButton)sender).CommandParameter as  TaskListItemDto;
        if (task != null)
        {

            bool confirmDelete = await DisplayAlert("", $"Are you sure you want to delete task '{task.Title}'? ", "Ok", "Cancel");

            if (confirmDelete)
            {
              bool result=  await _taskHelper.DeleteTask(task.Id);

                if (result)
                {
                    TaskList.Remove(task);

                    var toast = Toast.Make("Task delete successfully!", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);
                    await toast.Show();


                }
            }
        }


    }
}