using System;
using System.Collections.Generic;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;

namespace ListBoxExample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Random rnd = new Random();

            var listBox = this.FindControl<ListBox>("mListBox");
            listBox.SelectionMode = SelectionMode.AlwaysSelected;
            listBox.ItemContainerGenerator.Materialized += ItemContainerGenerator_PrepareContainer;
            listBox.ItemContainerGenerator.Recycled += ItemContainerGenerator_PrepareContainer;

            listBox.DataTemplates.Add(new FuncDataTemplate<Repository>((x, n) =>
            {
                Border container = new Border();
                container.Background = Brushes.Pink;
                container.Background = Brushes.Transparent;
                container.MinHeight = 40;

                DockPanel panel = new DockPanel();
                panel.LastChildFill = false;

                TextBlock textBlock = new TextBlock();
                textBlock.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
                textBlock[!TextBlock.TextProperty] = new Binding(nameof(x.Name));

                StackPanel actionsPanel = new StackPanel();
                actionsPanel.Spacing = 2;
                actionsPanel.Orientation = Avalonia.Layout.Orientation.Horizontal;
                actionsPanel.Children.Add(new Panel() { Background = Brushes.Gray, Height = 20, Width = 20 });
                actionsPanel.Children.Add(new Panel() { Background = Brushes.Gray, Height = 20, Width = 20 });
                actionsPanel.Children.Add(new Panel() { Background = Brushes.Gray, Height = 20, Width = 20 });
                actionsPanel.Children.Add(new Panel() { Background = Brushes.Gray, Height = 20, Width = 20 });
                actionsPanel.IsVisible = false;

                DockPanel.SetDock(textBlock, Dock.Left);
                DockPanel.SetDock(actionsPanel, Dock.Right);

                panel.Children.Add(textBlock);
                panel.Children.Add(actionsPanel);

                container.PointerEnter += (s, e) =>
                {
                    actionsPanel.IsVisible = true;
                };

                container.PointerLeave += (s, e) =>
                {
                    actionsPanel.IsVisible = false;
                };

                container.Child = panel;

                return container;
            }, true));

            listBox.DataTemplates.Add(new FuncDataTemplate<Workspace>((x, n) =>
            {
                Border container = new Border();
                container.MinHeight = 32;

                TextBlock textBlock = new TextBlock();
                textBlock.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
                textBlock[!TextBlock.TextProperty] = new Binding(nameof(x.Name));

                container.Child = textBlock;

                return container;
            }, true));

            List<object> items = new List<object>();

            for (int i = 0; i < 10000; i++)
            {
                items.Add(new Repository(i));

                int total = rnd.Next(0, 4);
                for (int j = 0; j < total; j++)
                {
                    items.Add(new Workspace(j, i));
                }
            }

            listBox.Items = items;
        }

        private void ItemContainerGenerator_PrepareContainer(object? sender, Avalonia.Controls.Generators.ItemContainerEventArgs e)
        {
            if (e.Containers[0].Item is Repository)
            {
                ((ListBoxItem)e.Containers[0].ContainerControl).Classes.Replace(new List<string>(){ "repository" });
            }
            if (e.Containers[0].Item is Workspace)
            {
                ((ListBoxItem)e.Containers[0].ContainerControl).Classes.Replace(new List<string>(){ "workspace" });
            }
        }

        public class Repository
        {
            public string Name
            {
                get { return this.ToString(); }
            }
            
            public Repository(int index)
            {
                mIndex = index;
            }

            public override string ToString()
            {
                return "Repository: " + mIndex.ToString();
            }

            int mIndex;
        }

        public class Workspace
        {
            public string Name
            {
                get { return this.ToString(); }
            }

            public Workspace(int index, int repoIndex)
            {
                mIndex = index;
                mRepoIndex = repoIndex;
            }

            public override string ToString()
            {
                return "Workspace: " + mIndex.ToString() + " of repo " + mRepoIndex;
            }

            int mIndex;
            int mRepoIndex;
        }
    }
}