package md5167cf0c1fa468a34aa4f067b9e4d8de4;


public class SportActivity
	extends md5167cf0c1fa468a34aa4f067b9e4d8de4.MenuActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onBackPressed:()V:GetOnBackPressedHandler\n" +
			"";
		mono.android.Runtime.register ("MySportsBook.SportActivity, MySportsBook, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", SportActivity.class, __md_methods);
	}


	public SportActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SportActivity.class)
			mono.android.TypeManager.Activate ("MySportsBook.SportActivity, MySportsBook, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onBackPressed ()
	{
		n_onBackPressed ();
	}

	private native void n_onBackPressed ();

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
