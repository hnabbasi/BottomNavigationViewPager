using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace BottomNavigationViewPager.Fragments
{
    public class TheFragment : Fragment
    {
        string _title;
        string _icon;

        public static TheFragment NewInstance(string title, string icon) {
            var fragment = new TheFragment();
            fragment.Arguments = new Bundle();
            fragment.Arguments.PutString("title", title);
            fragment.Arguments.PutString("icon", icon);
            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Arguments != null)
            {
                if(Arguments.ContainsKey("title"))
                    _title = (string)Arguments.Get("title");

                if (Arguments.ContainsKey("icon"))
                    _icon = (string)Arguments.Get("icon");
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.TheFragmentLayout, container, false);

            var imageView = view.FindViewById<ImageView>(Resource.Id.imageView);

            int resID = (int)typeof(Resource.Drawable).GetField(_icon).GetValue(null);
            imageView.SetImageResource(resID);

            var textView = view.FindViewById<TextView>(Resource.Id.title);
            textView.SetText(_title, null);

            return view;
        }
    }
}
