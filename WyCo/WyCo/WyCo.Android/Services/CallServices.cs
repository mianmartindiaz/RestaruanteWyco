using System;
using Android.Content;
using WyCo.Services;
using Xamarin.Forms;
using WyCo.Droid.Services;
using WyCo.Droid;

[assembly: Dependency(typeof(CallServices))]
namespace WyCo.Droid
{
    class CallServices : ICallServices
    {
        public static void Init()
        {

        }

        public void MakeCall()
        {
            var phone = "927208038";
            if (System.Text.RegularExpressions.Regex.IsMatch(phone, "^(\\(?\\+?[0-9]*\\)?)?[0-9_\\- \\(\\)]*$"))
            {
                var uri = Android.Net.Uri.Parse(String.Format("tel:{0}", phone));
                var intent = new Intent(Intent.ActionView, uri);
                Forms.Context.StartActivity(intent);
            }
            else
            {

            }
        }
    }
}