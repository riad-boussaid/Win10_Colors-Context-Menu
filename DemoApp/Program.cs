namespace Win10_Colors;

class Program : ApplicationContext
{

    public static Mutex mutex = null;
    public static string appname;


    [STAThread]
    static void Main()
    {

        appname = "Win10_Color";
        mutex = new Mutex(true, appname, out bool CreatedNew);

        if (!CreatedNew)
        {
            MessageBox.Show("It`s Already Open", appname, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }


        Icon icon = new (@"..\..\Icons\programe16.ico");
        NotifyIcon notifyIcon1 = new();
		notifyIcon1.Visible = true;
        notifyIcon1.Icon = icon;
        notifyIcon1.Text = "Win10_Colors";
        notifyIcon1.ContextMenuStrip = new ContextMenu().ContextMenuStrip;

        Application.Run();


    }
}
