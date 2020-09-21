package crc6429bbe2c8730bf8ef;


public class tela_inicial
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
		mono.android.Runtime.register ("pedraPapelTesoura.Resources.tela_inicial, pedraPapelTesoura", tela_inicial.class, __md_methods);
	}


	public tela_inicial ()
	{
		super ();
		if (getClass () == tela_inicial.class)
			mono.android.TypeManager.Activate ("pedraPapelTesoura.Resources.tela_inicial, pedraPapelTesoura", "", this, new java.lang.Object[] {  });
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
