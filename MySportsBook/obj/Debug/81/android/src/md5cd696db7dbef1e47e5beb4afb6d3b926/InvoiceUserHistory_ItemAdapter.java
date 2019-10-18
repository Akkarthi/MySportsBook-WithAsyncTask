package md5cd696db7dbef1e47e5beb4afb6d3b926;


public class InvoiceUserHistory_ItemAdapter
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("MySportsBook.InvoiceUserHistory_ItemAdapter, MySportsBook", InvoiceUserHistory_ItemAdapter.class, __md_methods);
	}


	public InvoiceUserHistory_ItemAdapter ()
	{
		super ();
		if (getClass () == InvoiceUserHistory_ItemAdapter.class)
			mono.android.TypeManager.Activate ("MySportsBook.InvoiceUserHistory_ItemAdapter, MySportsBook", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
