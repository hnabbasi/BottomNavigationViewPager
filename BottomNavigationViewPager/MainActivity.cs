using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.Design.Internal;
using Android.Support.V4.View;
using BottomNavigationViewPager.Adapters;
using System.Collections.Generic;
using Android.Support.V4.App;
using BottomNavigationViewPager.Fragments;

namespace BottomNavigationViewPager
{
    [Android.App.Activity(Label = "Bottom Tabs", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : FragmentActivity
    {
        ViewPager _viewPager;
        BottomNavigationView _navigationView;
        Fragment[] _fragments;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            InitializeTabs();

            _viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            _viewPager.PageSelected += ViewPager_PageSelected;
            _viewPager.Adapter = new ViewPagerAdapter(SupportFragmentManager, _fragments);
			
            _navigationView = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            RemoveShiftMode(_navigationView);
            _navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
        }

        void InitializeTabs() {
            _fragments = new Fragment[] {
                TheFragment.NewInstance("Genres", "tab_genres"),
                TheFragment.NewInstance("Titles", "tab_titles"),
                TheFragment.NewInstance("Stream", "tab_stream"),
                TheFragment.NewInstance("Showtimes", "tab_showtimes")
            };
        }

        private void ViewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            var item = _navigationView.Menu.GetItem(e.Position);
            _navigationView.SelectedItemId = item.ItemId;
        }

        void NavigationView_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            _viewPager.SetCurrentItem(e.Item.Order, true);
        }

        void RemoveShiftMode(BottomNavigationView view) {

            var menuView = (BottomNavigationMenuView) view.GetChildAt(0);

            try
            {
                var shiftingMode = menuView.Class.GetDeclaredField("mShiftingMode");
				shiftingMode.Accessible = true;
				shiftingMode.SetBoolean(menuView, false);
				shiftingMode.Accessible = false;

				for (int i = 0; i < menuView.ChildCount; i++)
				{
					var item = (BottomNavigationItemView)menuView.GetChildAt(i);
					item.SetShiftingMode(false);
					// set once again checked value, so view will be updated
					item.SetChecked(item.ItemData.IsChecked);
				}
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine((ex.InnerException??ex).Message);
            }
        }

        protected override void OnDestroy()
        {
			_viewPager.PageSelected -= ViewPager_PageSelected;
            _navigationView.NavigationItemSelected -= NavigationView_NavigationItemSelected;
            base.OnDestroy();
        }
    }
}

