using Substation_Builder.Helpers;
using Substation_Builder.Model;
using Substation_Builder.View;
using Substation_Builder.Services;
using System.Windows;
using System.Windows.Controls;
using Substation_Builder.Resources.Monster;
using System;

namespace Substation_Builder.ViewModel
{
    class OnelineViewModel : ObservableObject
    {
        private Substation _project;
        public Substation Project
        {
            get { return _project; }
            set
            {
                _project = value;
                NotifyPropertyChanged("Project");
            }
        }
        private readonly SystemIO FileIO = new SystemIO();
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand LoadCommand { get; set; }
        public RelayCommand TemplateCommand { get; set; }
        public RelayCommand NewCommand { get; set; }
        public RelayCommand RemoveItemCommand { get; set; }
        public RelayCommand AddCTCommand { get; set; }
        public RelayCommand AddItemCommand { get; set; }
        public RelayCommand ResetZoom { get; set; }
        public RelayCommand OpenBreakerCommand { get; set; }
        public RelayCommand CloseBreakerCommand { get; set; }
        public OnelineView OLView = new OnelineView();
        public OnelineViewModel(Substation refproject)
          {
            Project = refproject;
            LoadCommand = new RelayCommand(LoadFile);
            SaveCommand = new RelayCommand(SaveFile);
            TemplateCommand = new RelayCommand(LoadTemplate);
            NewCommand = new RelayCommand(NewProject);
            AddItemCommand = new RelayCommand(AddItem, Can_Add);
            RemoveItemCommand = new RelayCommand(RemoveItem, Can_Remove);
            AddCTCommand = new RelayCommand(AddCT, Can_AddCT);
            ResetZoom = new RelayCommand(ResetZ);
            OpenBreakerCommand = new RelayCommand(OpenBreaker, Can_Open);
            CloseBreakerCommand = new RelayCommand(CloseBreaker, Can_Close);

            OLView.DataContext = this;
            OLView.Show();
        }
        #region File Context Menu Section
        //read a .xaml file and load into the Datamodel classes
        private void LoadFile(object sender)
        {
            FileIO.FileOpen(Project);
            OLView.TreeviewExpander.Content = Project.SubData;
            OLView.TreeviewExpander.Header = Project.SubData.Name;
        }
        //Create Blank Project
        private void NewProject(object sender)
        {
            Substation substation = new Substation
            {
                SubData = new SubstationData
                {
                    Name = "New Project",
                    Monster = Monster.MonsterName()
                },
                OnelinePref = new OnelinePreferences()
            };
            Project.Replace(substation);
            OLView.TreeviewExpander.Content = Project.SubData;
            OLView.TreeviewExpander.Header = Project.SubData.Name;
        }
        //Serialize the DataModel and save
        public void SaveFile(object sender)
        {
            FileIO.FileSave(Project);
        }
        //Load Template
        public void LoadTemplate(object sender)
        {
            FileIO.LoadTemplate(Project);
            OLView.TreeviewExpander.Content = Project.SubData;
            OLView.TreeviewExpander.Header = Project.SubData.Name;
        }
        public int CreateID()
        {
            Random rand = new Random();
            int NewID = rand.Next();
            bool NewNumber = false;
            while (NewNumber == false)
            {
                if (Project.IDs.Contains(NewID))
                {
                    //ID already in use - make another
                    NewID = rand.Next();
                }
                else
                {
                    Project.IDs.Add(NewID);
                    NewNumber = true;
                }
            }
            return NewID;
        }
        //Add Item in Treeview
        public void AddItem(object sender)
        {
            if (sender.ToString() == "Project")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Save Curent Project?", "Create New Project", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.No)
                {
                    Project = new Substation
                    {
                        SubData = new SubstationData
                        {
                            Name = "New Project",
                            Monster = Monster.MonsterName()
                        }
                    };
                }
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    SaveFile(sender);
                    Project = new Substation
                    {
                        SubData = new SubstationData
                        {
                            Name = "New Project",
                            Monster = Monster.MonsterName()
                        }
                    };
                }
            }
            if (sender.ToString() == "Thevenin")
            {
                Thevenin thevenin = new Thevenin {ID = CreateID(), Name = "New Thevenin " + (Project.Thevenins.Count + 1).ToString(), Visibility = "Hidden" };
                Project.Thevenins.Add(thevenin);
            }
            if (sender.ToString() == "Breaker")
            {
                Breaker breaker = new Breaker { ID = CreateID(), Name = "New Breaker " + (Project.Breakers.Count + 1).ToString(), Visibility = "Hidden" };
                Project.Breakers.Add(breaker);
            }
            if (sender.ToString() == "Relay")
            {
                Relay relay = new Relay { ID = CreateID(), Name = "New Relay " + (Project.Relays.Count + 1).ToString(), Visibility = "Hidden" };
                Project.Relays.Add(relay);
            }
            if (sender.ToString() == "Transformer")
            {
                Transformer transformer = new Transformer { ID = CreateID(), Name = "New Transformer " + (Project.Transformers.Count + 1).ToString(), Visibility = "Hidden" };
                Project.Transformers.Add(transformer);
            }
        }
        //Add a CT
        public void AddCT(object sender)
        {
            if (sender != null)
            {
                //CT for Breaker
                if (sender.GetType() == typeof(Breaker))
                {
                    int index = Project.Breakers.IndexOf(sender as Breaker);
                    Breaker breaker = Project.Breakers[index];
                    CT ct = new CT
                    {
                        ID = CreateID()
                    };
                    CT[] cTs = new CT[6];
                    breaker.CTs.CopyTo(cTs, 0);
                    string[] cTPositions = new string[6];
                    for (int i = 0; i < 6; i++)
                    {
                        if (cTs[i] != null)
                        {
                            if (cTs[i].CT_Position == CTPosition.CT1)
                                cTPositions[0] = CTPosition.CT1.ToString();
                            if (cTs[i].CT_Position == CTPosition.CT2)
                                cTPositions[1] = CTPosition.CT2.ToString();
                            if (cTs[i].CT_Position == CTPosition.CT3)
                                cTPositions[2] = CTPosition.CT3.ToString();
                            if (cTs[i].CT_Position == CTPosition.CT4)
                                cTPositions[3] = CTPosition.CT4.ToString();
                            if (cTs[i].CT_Position == CTPosition.CT5)
                                cTPositions[4] = CTPosition.CT5.ToString();
                            if (cTs[i].CT_Position == CTPosition.CT6)
                                cTPositions[5] = CTPosition.CT6.ToString();
                        }
                    }
                    for (int j = 0; j < 6; j++)
                    {
                        if (cTPositions[j] == null)
                        {
                            ct.CT_Position = (CTPosition)j;
                            j = 5;
                        }
                    }
                    ct.Visibility = "Visible";
                    Project.Breakers[index].CTs.Add(ct);
                }
                //CT for Transformer
                else if (sender.GetType() == typeof(Transformer))
                {
                    //Find which xfmr to add CT to
                    int index = Project.Transformers.IndexOf(sender as Transformer);
                    Transformer transformer = Project.Transformers[index];
                    //Create the new CT to add to list
                    CT ct = new CT
                    {
                        ID = CreateID()
                    };
                    //Create new array to hold all the current CTs to find out what is used
                    CT[] cTs = new CT[6];
                    //Copy existing CTs into the new array to manipulate
                    transformer.CTs.CopyTo(cTs, 0);
                    //New string array to put in order of CTs open
                    string[] cTPositions = new string[6];
                    //loop through the array and assign in correct order
                    for (int i = 0; i < 6; i++)
                    {
                        if (cTs[i] != null)
                        {
                            if (cTs[i].CT_Position == CTPosition.CT1)
                                cTPositions[0] = CTPosition.CT1.ToString();
                            if (cTs[i].CT_Position == CTPosition.CT2)
                                cTPositions[1] = CTPosition.CT2.ToString();
                            if (cTs[i].CT_Position == CTPosition.CT3)
                                cTPositions[2] = CTPosition.CT3.ToString();
                            if (cTs[i].CT_Position == CTPosition.CT4)
                                cTPositions[3] = CTPosition.CT4.ToString();
                            if (cTs[i].CT_Position == CTPosition.CT5)
                                cTPositions[4] = CTPosition.CT5.ToString();
                            if (cTs[i].CT_Position == CTPosition.CT6)
                                cTPositions[5] = CTPosition.CT6.ToString();
                        }
                    }
                    for (int j = 0; j < 6; j++)
                    {
                        if (cTPositions[j] == null)
                        {
                            ct.CT_Position = (CTPosition)j;
                            j = 5;
                        }
                    }
                    ct.Visibility = "Visible";
                    Project.Transformers[index].CTs.Add(ct);
                }
            }
        }
        public bool Can_AddCT(object sender)
        {
            if (sender == null)
                return false;

            if (sender.GetType() == typeof(Breaker))
            {
                Breaker breaker = (Breaker)sender;
                if (breaker.CTs.Count > 5)
                    return false;
            }
            else if (sender.GetType() == typeof(Transformer))
            {
                Transformer transformer = (Transformer)sender;
                if (transformer.CTs.Count > 5)
                    return false;
            }
            return true;
        }
        //Select Item to remove
        public void RemoveItem(object sender)
        {
            if (sender.GetType() == typeof(Thevenin))
            {
                Thevenin thevenin = sender as Thevenin;
                Project.Thevenins.Remove(thevenin);
                Project.IDs.Remove(thevenin.ID);
            }
            else if (sender.GetType() == typeof(Breaker))
            {
                Breaker breaker = sender as Breaker;
                Project.Breakers.Remove(breaker);
                Project.IDs.Remove(breaker.ID);
            }
            else if (sender.GetType() == typeof(Relay))
            {
                Relay relay = sender as Relay;
                Project.Relays.Remove(relay);
                Project.IDs.Remove(relay.ID);
            }
            else if (sender.GetType() == typeof(Transformer))
            {
                Transformer transformer = sender as Transformer;
                Project.Transformers.Remove(transformer);
                Project.IDs.Remove(transformer.ID);
            }
            else if (sender.GetType() == typeof(CT))
            {
                CT CTtoRemove = (CT)sender;
                foreach (Transformer Tranformer in Project.Transformers)
                {
                    if (Tranformer.CTs.Contains(CTtoRemove))
                    {
                        Tranformer.CTs.Remove(CTtoRemove);
                        Project.IDs.Remove(CTtoRemove.ID);
                    }
                }
                foreach (Breaker Breaker in Project.Breakers)
                {
                    if (Breaker.CTs.Contains(CTtoRemove))
                    {
                        Breaker.CTs.Remove(CTtoRemove);
                        Project.IDs.Remove(CTtoRemove.ID);
                    }
                }
            }
        }
        //Controls abiliity to add/remove items
        public bool Can_Add(object sender)
        {
            if (sender == null)
                return false;
            bool canexecute = false;

            /*

            if (OLView.OnelineTreeview.SelectedItem != null)
            {
                if (OLView.OnelineTreeview.SelectedItem.GetType() == typeof(TreeViewItem))
                {
                    TreeViewItem TVI = (TreeViewItem) OLView.OnelineTreeview.SelectedItem;
                    if (TVI.Header.ToString() == "Thevenins")
                    {
                        canexecute = true;
                    }
                    else if (TVI.Header.ToString() == "Transformers")
                    {
                        canexecute = true;
                    }
                    else if (TVI.Header.ToString() == "Breakers")
                    {
                        canexecute = true;
                    }
                    else if (TVI.Header.ToString() == "Relays")
                    {
                        canexecute = true;
                    }
                }
            }
                        */
            return canexecute;


        }
        //Controls abiliity to add/remove items
        public bool Can_Remove(object sender)
        {
            if (sender == null)
                return false;
            bool canexecute = false;

            /*

            if (OLView.OnelineTreeview.SelectedItem != null)
            {
                if (OLView.OnelineTreeview.SelectedValue.GetType() == typeof(Thevenin))
                {
                    canexecute = true;
                }
                else if (OLView.OnelineTreeview.SelectedValue.GetType() == typeof(Transformer))
                {
                    canexecute = true;
                }
                else if (OLView.OnelineTreeview.SelectedValue.GetType() == typeof(Breaker))
                {
                    canexecute = true;
                }
                else if (OLView.OnelineTreeview.SelectedValue.GetType() == typeof(Relay))
                {
                    canexecute = true;
                }
                else if (OLView.OnelineTreeview.SelectedValue.GetType() == typeof(CT))
                {
                    canexecute = true;
                }
            }
            */

            return canexecute;
        }
        #endregion
        #region Breaker Context Menu
        public void OpenBreaker(object sender)
        {
            Breaker breaker = (Breaker)sender;
            breaker.BreakerOpen = true;
        }
        public bool Can_Open(object sender)
        {
            if (sender == null)
                return false;
            Breaker breaker = (Breaker)sender;
            if (breaker.BreakerOpen == false)
                return true;
            else
                return false;
        }
        public void CloseBreaker(object sender)
        {
            Breaker breaker = (Breaker)sender;
            breaker.BreakerOpen = false;
        }
        public bool Can_Close (object sender)
        {
            if (sender == null)
                return false;

            Breaker breaker = (Breaker)sender;
            if (breaker.BreakerOpen == false)
                return false;
            else
                return true;
        }
        #endregion
        #region Scrolling support
        public void ResetZ(object sender)
        {
            Project.OnelinePref.ZoomLevel = 1;
        }
        #endregion
    }
}
