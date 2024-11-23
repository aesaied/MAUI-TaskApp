using System.Text;
using TaskApp.Helpers;
using TaskApp.Model;

namespace TaskApp.Pages;

public partial class ManageTask : ContentPage
{
	private readonly TaskHelper _taskHelper;
	public ManageTask(TaskHelper taskHelper)
	{
		_taskHelper = taskHelper;
		InitializeComponent();
	}


	public TaskInputDto CurrentTask { get; set; }

    private async void btnSave_Clicked(object sender, EventArgs e)
    {
		btnSave.IsEnabled = false;
		if (await IsValid())
		{
			var task = new TaskInputDto { Description = txtDescription.Text, IsComplete = chkIsComplete.IsChecked, Title = txtTitle.Text };

        bool result=	await _taskHelper.AddTask(task);
			if (result)
			{

		     
				CurrentTask= task;
				ClosedAndReturn?.Invoke(this, CurrentTask);
	
                await AppShell.Current.GoToAsync("..");
                // await	App..PopAsync();

                //
            }
		}


		btnSave.IsEnabled = true;

    }
	public EventHandler<TaskInputDto> ClosedAndReturn;
	private async Task<bool> IsValid()
	{
		bool valid = true;

		StringBuilder errors= new StringBuilder();

		if (string.IsNullOrWhiteSpace(txtTitle.Text))
		{
			valid = false;
			errors.AppendLine("Title is required");
		}
        if (string.IsNullOrWhiteSpace(txtDescription.Text))
        {
            valid = false;
            errors.AppendLine("Description is required");
        }


		if (!valid)
		{
			await DisplayAlert("", errors.ToString(), "Ok");
		}
        return valid;
	}
}

