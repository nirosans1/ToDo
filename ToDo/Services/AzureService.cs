﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using ToDo.Models;
using System.Diagnostics;

namespace ToDo.Services
{
    public class AzureService:IAzureService
    {
        public MobileServiceClient MobileService { get; set; }
        IMobileServiceSyncTable<ToDoItem> todoTable;

        bool isInitialized;
        public async Task Initialize()
        {
            if (isInitialized)
                return;

            //TODO 1: Create our client
            //Create our client
            MobileService = new MobileServiceClient(Utility.Constant.AzureServiceUrl, null)
            {
                SerializerSettings = new MobileServiceJsonSerializerSettings()
                {
                    CamelCasePropertyNames = true
                }
            };

            //TODO 2: Create our database store & define a table.
            var store = new MobileServiceSQLiteStore("todo.db");
            store.DefineTable<ToDoItem>();

            //MobileServiceSyncHandler - Handles table operation errors and push completion results.
            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            //Get our sync table that will call out to azure
            todoTable = MobileService.GetSyncTable<ToDoItem>();

            isInitialized = true;
        }

        public async Task SyncToDo()
        {
            //TODO 3: Add connectivity check. 
            var connected = await Plugin.Connectivity.CrossConnectivity.Current.IsReachable(Utility.Constant.AzureServiceUrl);
            if (connected == false)
                return;

            try
            {
                //TODO 4: Push and Pull our data
                await MobileService.SyncContext.PushAsync();
                await todoTable.PullAsync("allTodoItems", todoTable.CreateQuery());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync items, that is alright as we have offline capabilities: " + ex);
            }
        }

        public async Task<IEnumerable<ToDoItem>> GetToDo()
        {
            await Initialize();
            await SyncToDo();
            return await todoTable.ToEnumerableAsync();
        }

        public async Task<ToDoItem> AddToDo(string text, bool complete)
        {
            await Initialize();
            var item = new ToDoItem
            {
                Text = text,
                Complete = complete
            };

            //TODO 5: Insert item into todoTable
            await todoTable.InsertAsync(item);
            //Synchronize todos
            await SyncToDo();
            return item;
        }

        public async Task<ToDoItem> UpdateItem(ToDoItem item)
        {
            await Initialize();

            //TODO 6: Update item
            await todoTable.UpdateAsync(item);

            //Synchronize todos
            await SyncToDo();
            return item;
        }

        public async Task<bool> DeleteItem(ToDoItem item)
        {
            await Initialize();
            try
            {
                //TODO 7: Delete item and Sync
                await todoTable.DeleteAsync(item);
                await SyncToDo();

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
