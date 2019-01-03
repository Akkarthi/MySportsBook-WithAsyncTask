package md5cd696db7dbef1e47e5beb4afb6d3b926;


public class AysncTaskClass
	extends android.os.AsyncTask
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPreExecute:()V:GetOnPreExecuteHandler\n" +
			"n_doInBackground:([Ljava/lang/Object;)Ljava/lang/Object;:GetDoInBackground_arrayLjava_lang_Object_Handler\n" +
			"n_onPostExecute:(Ljava/lang/Object;)V:GetOnPostExecute_Ljava_lang_Object_Handler\n" +
			"";
		mono.android.Runtime.register ("MySportsBook.AysncTaskClass, MySportsBook", AysncTaskClass.class, __md_methods);
	}


	public AysncTaskClass ()
	{
		super ();
		if (getClass () == AysncTaskClass.class)
			mono.android.TypeManager.Activate ("MySportsBook.AysncTaskClass, MySportsBook", "", this, new java.lang.Object[] {  });
	}

	public AysncTaskClass (java.lang.String p0, java.lang.String p1, md5cd696db7dbef1e47e5beb4afb6d3b926.LoginActivity p2, android.widget.LinearLayout p3, java.lang.String p4)
	{
		super ();
		if (getClass () == AysncTaskClass.class)
			mono.android.TypeManager.Activate ("MySportsBook.AysncTaskClass, MySportsBook", "System.String, mscorlib:System.String, mscorlib:MySportsBook.LoginActivity, MySportsBook:Android.Widget.LinearLayout, Mono.Android:System.String, mscorlib", this, new java.lang.Object[] { p0, p1, p2, p3, p4 });
	}


	public void onPreExecute ()
	{
		n_onPreExecute ();
	}

	private native void n_onPreExecute ();


	public java.lang.Object doInBackground (java.lang.Object[] p0)
	{
		return n_doInBackground (p0);
	}

	private native java.lang.Object n_doInBackground (java.lang.Object[] p0);


	public void onPostExecute (java.lang.Object p0)
	{
		n_onPostExecute (p0);
	}

	private native void n_onPostExecute (java.lang.Object p0);

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
