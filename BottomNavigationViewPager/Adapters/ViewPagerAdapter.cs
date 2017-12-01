using Android.Support.V4.App;
using BottomNavigationViewPager.Fragments;

namespace BottomNavigationViewPager.Adapters
{
    public class ViewPagerAdapter : FragmentPagerAdapter
    {
        int tabsCount;

        public ViewPagerAdapter(FragmentManager fm, int count) : base(fm)
        {
            tabsCount = count;
        }

        public override int Count => tabsCount;

        public override Fragment GetItem(int position)
        {
			Fragment fragment = null;

			switch (position)
			{
				case 0:
					fragment = TheFragment.NewInstance("Genres", "tab_genres");
					break;
				case 1:
                    fragment = TheFragment.NewInstance("Titles", "tab_titles");
					break;
				case 2:
                    fragment = TheFragment.NewInstance("Stream", "tab_stream");
					break;
				case 3:
                    fragment = TheFragment.NewInstance("Showtimes", "tab_showtimes");
					break;
			}
			return fragment;
        }
    }
}
