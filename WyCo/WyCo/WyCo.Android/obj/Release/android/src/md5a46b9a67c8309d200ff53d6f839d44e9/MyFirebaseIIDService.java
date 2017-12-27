package md5a46b9a67c8309d200ff53d6f839d44e9;


public class MyFirebaseIIDService
	extends com.google.firebase.iid.FirebaseInstanceIdService
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onTokenRefresh:()V:GetOnTokenRefreshHandler\n" +
			"";
		mono.android.Runtime.register ("WyCo.Droid.Notifications.MyFirebaseIIDService, WyCo.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MyFirebaseIIDService.class, __md_methods);
	}


	public MyFirebaseIIDService ()
	{
		super ();
		if (getClass () == MyFirebaseIIDService.class)
			mono.android.TypeManager.Activate ("WyCo.Droid.Notifications.MyFirebaseIIDService, WyCo.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onTokenRefresh ()
	{
		n_onTokenRefresh ();
	}

	private native void n_onTokenRefresh ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
