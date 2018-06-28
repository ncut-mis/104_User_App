using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.OS;
using Android.Support.V7.App;
using Firebase.Messaging;
using Android.Util;
using System.Collections.Generic;
using Firebase.Iid;
using Android.Widget;
using Android.Gms.Common;
using Android.Webkit;

namespace App12
{
    [Activity(Label = "@string/app_name", MainLauncher = true, LaunchMode = Android.Content.PM.LaunchMode.SingleTop, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        const string TAG = "MainActivity";
        WebView webviewMain;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //Set your main view here
            SetContentView(Resource.Layout.main);

            webviewMain = FindViewById<WebView>(Resource.Id.webviewMain);
            //啟用Javascript Enable
            webviewMain.Settings.JavaScriptEnabled = true;
            //載入網址
            webviewMain.LoadUrl("http://140.128.80.192/posts");
            // 請注意這行，如果不加入巢狀Class 會必成呼叫系統讓系統來裁決開啟http 的方式
            webviewMain.SetWebViewClient(new CustWebViewClient());

        
            Log.Debug(TAG, "InstanceID token: " + FirebaseInstanceId.Instance.Token);
            FirebaseMessaging.Instance.SubscribeToTopic("news");
        }
        private class CustWebViewClient : WebViewClient
        {
            public override bool ShouldOverrideUrlLoading(WebView view, string url)
            {
                view.LoadUrl(url);
                return true;
            }

        }
        public override bool OnKeyDown(Android.Views.Keycode keyCode, Android.Views.KeyEvent e)
        {
            if (keyCode == Keycode.Back && webviewMain.CanGoBack())
            {
                webviewMain.GoBack();
                return true;
            }
            return base.OnKeyDown(keyCode, e);
        }
        [Service]
        [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
        public class MyFirebaseIIDService : FirebaseInstanceIdService
        {
            const string TAG = "MyFirebaseIIDService";
            public override void OnTokenRefresh()
            {
                var refreshedToken = FirebaseInstanceId.Instance.Token;
                Log.Debug(TAG, "Refreshed token: " + refreshedToken);
                SendRegistrationToServer(refreshedToken);
            }
            void SendRegistrationToServer(string token)
            {
                // Add custom implementation, as needed.
            }
        }
        [Service]
        [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
        public class MyFirebaseMessagingService : FirebaseMessagingService
        {
            const string TAG = "MyFirebaseMsgService";
            public override void OnMessageReceived(RemoteMessage message)
            {
                Log.Debug(TAG, "From: " + message.From);
                Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
                SendNotification(message.GetNotification().Body);
            }
            void SendNotification(string messageBody)
            {
                var intent = new Intent(this, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ClearTop);
                var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

                var notificationBuilder = new Notification.Builder(this)
                    .SetSmallIcon(Resource.Drawable.Icon)
                    .SetContentTitle("雙魚商圈優惠通知")
                    .SetContentText(messageBody)
                    .SetAutoCancel(true)
                    .SetContentIntent(pendingIntent);
                var notificationManager = NotificationManager.FromContext(this);
                notificationManager.Notify(0, notificationBuilder.Build());
            }
        }
    }
}


