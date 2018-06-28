package md52761d89aca4f27a7a5ffd378a6c80057;


public class MainActivity_MyFirebaseIIDService
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
		mono.android.Runtime.register ("App12.MainActivity+MyFirebaseIIDService, App12", MainActivity_MyFirebaseIIDService.class, __md_methods);
	}


	public MainActivity_MyFirebaseIIDService ()
	{
		super ();
		if (getClass () == MainActivity_MyFirebaseIIDService.class)
			mono.android.TypeManager.Activate ("App12.MainActivity+MyFirebaseIIDService, App12", "", this, new java.lang.Object[] {  });
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
