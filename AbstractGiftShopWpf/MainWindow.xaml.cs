﻿using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using AbstractGiftShopServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Unity;

namespace AbstractGiftShopWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly IMainService service;

        public MainWindow(IMainService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void LoadData()
        {
            try
            {
                List<SOrderViewModel> list = service.GetList();
                if (list != null)
                {
                    dataGridViewMain.ItemsSource = list;
                    dataGridViewMain.Columns[0].Visibility = Visibility.Hidden;
                    dataGridViewMain.Columns[1].Visibility = Visibility.Hidden;
                    dataGridViewMain.Columns[3].Visibility = Visibility.Hidden;
                    dataGridViewMain.Columns[5].Visibility = Visibility.Hidden;
                    dataGridViewMain.Columns[1].Width = DataGridLength.Auto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<WindowClients>();
            form.ShowDialog();
        }

        private void материалыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<WindowMaterialss>();
            form.ShowDialog();
        }

        private void подаркиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<WindowGifts>();
            form.ShowDialog();
        }


        private void buttonCreateSOrder_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<WindowCreateSOrder>();
            form.ShowDialog();
            LoadData();
        }

        private void buttonTakeSOrderInWork_Click(object sender, EventArgs e)
        {
            if (dataGridViewMain.SelectedItem != null)
            {
                int id = ((SOrderViewModel)dataGridViewMain.SelectedItem).Id;
                try
                {
                    service.TakeOrderInWork(new SOrderBindingModel { Id = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void buttonSOrderReady_Click(object sender, EventArgs e)
        {
            if (dataGridViewMain.SelectedItem != null)
            {
                int id = ((SOrderViewModel)dataGridViewMain.SelectedItem).Id;
                try
                {
                    service.FinishOrder(new SOrderBindingModel { Id = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void buttonPaySOrder_Click(object sender, EventArgs e)
        {
            if (dataGridViewMain.SelectedItem != null)
            {
                int id = ((SOrderViewModel)dataGridViewMain.SelectedItem).Id;
                try
                {
                    service.PayOrder(new SOrderBindingModel { Id = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}