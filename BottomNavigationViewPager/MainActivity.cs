using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.Design.Internal;
using Android.Support.V4.View;
using Android.App;
using BottomNavigationViewPager.Adapters;

namespace BottomNavigationViewPager
{
    [Activity(Label = "Bottom Tabs", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Android.Support.V4.App.FragmentActivity
    {
        ViewPager _viewPager;
        BottomNavigationView _navigationView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            _viewPager.PageSelected += ViewPager_PageSelected;
            _viewPager.Adapter = new ViewPagerAdapter(SupportFragmentManager, 4);
			
            _navigationView = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            RemoveShiftMode(_navigationView);
            _navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
        }

        private void ViewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            int resId;

            switch (e.Position)
            {
                case 0:
                    resId = Resource.Id.menu_genres;
                    break;
                case 1:
                    resId = Resource.Id.menu_titles;
					break;
				case 2:
					resId = Resource.Id.menu_stream;
					break;
				case 3:
					resId = Resource.Id.menu_showtimes;
					break;
                default:
                    resId = 0;
                    break;
            }

            _navigationView.SelectedItemId = resId;
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

