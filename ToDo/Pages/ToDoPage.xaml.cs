using System;
using System.Collections.Generic;
using ToDo.ViewModels;
using Xamarin.Forms;

namespace ToDo.Pages
{
    public partial class ToDoPage : ContentPage
    {
        ToDoViewModel viewModel;
        public ToDoPage()
        {
            InitializeComponent();
            viewModel = new ToDoViewModel();
            BindingContext = viewModel;
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }
    }
}

