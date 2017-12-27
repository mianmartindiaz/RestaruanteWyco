using System;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Util;
using Firebase.Messaging;
using Android.Graphics;

namespace WyCo.Droid.Notifications
{
    [Service] [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
  public  class MyFirebaseMessagingService: FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From); Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
            SendNotification(message);
        }
        void SendNotification(RemoteMessage messageBody)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);
            var notificationBuilder = new Notification.Builder(this).SetSmallIcon(Resource.Drawable.icono).SetContentTitle(messageBody.GetNotification().Title).SetContentText(messageBody.GetNotification().Body).SetAutoCancel(true).SetContentIntent(pendingIntent).SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification)).SetVibrate(new long[] { 1000, 1000, 1000, 1000, 1000 }).SetLights(Color.Red, 3000, 3000);
            var notificationManager = NotificationManager.FromContext(this);
            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}