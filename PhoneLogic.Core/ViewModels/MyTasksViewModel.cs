﻿using Microsoft.Lync.Model;
using PhoneLogic.Core.MVVM_Base_Types;
using PhoneLogic.Core.Services;
using PhoneLogic.Model;
using System.Collections.ObjectModel;
using System.Net;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace PhoneLogic.Core.ViewModels
{
    public class MyTasksViewModel : CollectionViewModelBase
    {

        public MyTasksViewModel()
        {
            StartAutoRefresh(ApiRefreshFrequency.UserDB);
            GetMyTasks();
        }

        public async void GetMyTasks()
        {

            LoadDate = DateTime.Now;
            try
            {
                var mcb = await PhoneLogicTaskSvc.GetMyTasks(LyncClient.GetClient().Self.Contact.Uri);
                PhoneLogicTasks.Clear();
                if (mcb.Count > 0)
                {
                    foreach (var p in mcb)
                        PhoneLogicTasks.Add(p);
                    ShowGridData = true;
                }
                else
                    ShowGridData = false;
                LoadedOk = true;
                // MessageBox.Show("mcbcnt" + mcb.Count() + "Show Grid Data" + ShowGridData);

            }
            catch (Exception e)
            {
//                MessageBox.Show(e.ToString());
//                MessageBox.Show("mcbcnt" + mcb.Count() + "Show Grid Data" + ShowGridData);
                LoadFailed(e);

            }
        }

        protected override void RefreshAll(object sender, EventArgs e)
        {
            GetMyTasks();
        }

        #region MyTasks

        
        private ObservableCollection<PhoneLogicTask> _phoneLogicTask = new ObservableCollection<PhoneLogicTask>();

        public ObservableCollection<PhoneLogicTask> PhoneLogicTasks
        {
            get { return _phoneLogicTask; }
            set
            {
                _phoneLogicTask = value;
                NotifyPropertyChanged();
            }
        }
        #endregion


    }
}
