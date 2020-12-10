using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WeSplitApp.Utils;
using WeSplitApp.View;
using WeSplitApp.View.Controls;
namespace WeSplitApp.ViewModel
{
    public class BeingTakenTripsListViewModel : TripViewModel
    {
        private static BeingTakenTripsListViewModel instance { get; set; }
        public static BeingTakenTripsListViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BeingTakenTripsListViewModel();
                }
                return instance;
            }
        }
                                                                                               
        private BeingTakenTripsListViewModel()
        {
            IsDone = false;
            this._itemHandler = GetData();
            TripSortMethods();
        }

        public override void search_byTripName()
        {
            string request = HomeScreen.GetHomeScreenInstance().SearchTextBox.Text;
            var requestText = convertUnicode.convertToUnSign(request.Trim().ToLower());
            string typeSearch = HaveTakenTripsListControl.GetInstance().SearchByComboBox.Text;
            //MessageBox.Show(typeSearch);
            List<TRIP> tripList = new List<TRIP>();
            //tripList = (HomeScreen.GetDatabaseEntities().TRIPS.AsEnumerable().Where(t => convertUnicode.convertToUnSign(t.TITTLE.Trim().ToLower()).Contains(request) && t.ISDONE == true).ToList());

            switch (typeSearch)
            {
                case "":
                case "Trip Title":
                    tripList = (HomeScreen.GetDatabaseEntities().TRIPS.AsEnumerable()
                        .Where(t => convertUnicode.convertToUnSign(t.TITTLE.Trim().ToLower()).Contains(requestText) && t.ISDONE == false)
                        .ToList());

                    //MessageBox.Show(b[0].TITTLE);
                    break;
                case "Member Name":
                    // search member theo ten nhap vao 
                    var memlist = (HomeScreen.GetDatabaseEntities().MEMBERS.AsEnumerable()
                        .Where(mem => convertUnicode.convertToUnSign(mem.NAME.Trim().ToLower()).Contains(requestText))).ToList();
                    tripList.Clear();
                    // tim chuyen di theo ten thanh vien
                    foreach (var index in memlist)
                    {
                        //tim chuyen di co thanh vien index
                        var memberTripList = (from t in HomeScreen.GetDatabaseEntities().TRIPS
                                              join mem in HomeScreen.GetDatabaseEntities().TRIP_MEMBER on t.TRIP_ID equals mem.TRIP_ID
                                              where mem.MEMBER_ID == index.MEMBER_ID && t.ISDONE == false
                                              select t).ToList();
                        // kiem tra chuyen di da co trong List chua va them vao
                        foreach (TRIP trip in memberTripList)
                        {
                            if (tripList == null || !tripList.Contains(trip))
                            {
                                tripList.Add(trip);
                            }
                        }

                    }
                    break;
                case "Location Name":
                    MessageBox.Show("By dia diem");
                    break;
                default:
                    MessageBox.Show("liu liu do ngok");
                    break;
            }
            instance.SearchResult = new ObservableCollection<TRIP>(tripList);
            instance.CalculatePagingInfo(instance.Paging.RowsPerPage, instance.SearchResult.Count);
            instance.DisplayObjects_Search();
            //if (request.Length <= 0)
            //{
            //    var all = (HomeScreen.GetDatabaseEntities().TRIPS.AsEnumerable().Where(t => t.ISDONE == true)).ToList();
            //    this.ToShowItems = new ObservableCollection<TRIP>(all);
            //}
            //else
            //{
            //    //search by TITLE
            //    var requestText = convertUnicode.convertToUnSign(request.Trim().ToLower());
            //    var tripList = (HomeScreen.GetDatabaseEntities().TRIPS.AsEnumerable().Where(t => convertUnicode.convertToUnSign(t.TITTLE.Trim().ToLower()).Contains(requestText) && t.ISDONE == true));

            //    //MessageBox.Show(b[0].TITTLE);
            //    this.ToShowItems = new ObservableCollection<TRIP>(tripList);
            //}
        }

    }
}
