namespace Win10_Colors;

public class ContextMenu
{

    public ContextMenu()
    {
        rgPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        registry = Registry.CurrentUser.OpenSubKey(rgPath, true);
        Component();
        CheckState();
    }

    public void CheckState()
    {
        if (SystemIsLight())
        {
            SystemLight.Checked = true;
        }
        if (!SystemIsLight())
        {
            SystemLight.Checked = false;
        }
        if (AppIsLight())
        {
            AppLight.Checked = true;
        }
        if (!AppIsLight())
        {
            AppLight.Checked = false;
        }
        if (IsTransparente())
        {
            trans.Checked = true;
        }
        if (!IsTransparente())
        {
            trans.Checked = false;
        }


    }
    public void ToggleAppTheme(object sender, EventArgs e)
    {
        if (AppIsLight())
        {
            registry.SetValue("AppsUseLightTheme", 0);
            AppLight.Checked = false;
        }
        else
        {
            registry.SetValue("AppsUseLightTheme", 1);
            AppLight.Checked = true;

        }

    }
    public void ToggleSystemTheme(object sender, EventArgs e)
    {
        if (SystemIsLight())
        {
            registry.SetValue("SystemUsesLightTheme", 0);
            SystemLight.Checked = false;

        }
        else
        {
            registry.SetValue("SystemUsesLightTheme", 1);
            SystemLight.Checked = true;
        }



    }
    public void ToggleTransparency(object sender, EventArgs e)
    {
        if (IsTransparente())
        {
            registry.SetValue("EnableTransparency", 0);
            trans.Checked = false;
        }
        else
        {
            registry.SetValue("EnableTransparency", 1);
            trans.Checked = true;
        }


    }

    public bool AppIsLight()
    {
        bool AppsUseLightTheme = Convert.ToBoolean(registry.GetValue("AppsUseLightTheme"));
        return AppsUseLightTheme;
    }
    public bool SystemIsLight()
    {
        bool SystemUsesLightTheme = Convert.ToBoolean(registry.GetValue("SystemUsesLightTheme"));
        return SystemUsesLightTheme;
    }
    public bool IsTransparente()
    {
        bool EnableTransparency = Convert.ToBoolean(registry.GetValue("EnableTransparency"));
        return EnableTransparency;
    }



    

    public static string rgPath;
    public RegistryKey registry;
    public ToolStripMenuItem ChangeWallpaper;
    public ToolStripMenuItem AppLight;
    public ToolStripMenuItem SystemLight;
    public ToolStripMenuItem trans;
    public ToolStripMenuItem help;
    public ToolStripMenuItem aboutMe;
    public ToolStripMenuItem exit;
    public ContextMenuStrip ContextMenuStrip;

    public void Component()
    {

        ChangeWallpaper = new("Change Wallpaper");
        AppLight = new("Light Theme (Apps)");
        SystemLight = new("Light Theme (System)");
        trans = new("Transparency");
        help = new("Help");
        aboutMe = new("About me");
        exit = new("Exit");
        ContextMenuStrip = new();

        // ChangeWallpaper
        ChangeWallpaper.Click += new EventHandler(ChangeWall);
        // AppLight  
        AppLight.Click += new EventHandler(ToggleAppTheme);
        // SystemLight  
        SystemLight.Click += new EventHandler(ToggleSystemTheme);   
        // trans                
        trans.Click += new EventHandler(ToggleTransparency);
        // help
        help.DropDownItems.Add(aboutMe);
        help.Image = Image.FromFile(@"..\..\Icons\help.ico");
        // aboutMe         
        aboutMe.Image = Image.FromFile(@"..\..\Icons\info.ico");
        aboutMe.ShortcutKeys = Keys.Control | Keys.A;
        aboutMe.Click += new EventHandler(new About().About_Load);
        // exit
        exit.Image = Image.FromFile(@"..\..\Icons\exit.ico");
        exit.Click += new EventHandler((s, e) => Application.Exit());
        // ContextMenuStrip          
        ContextMenuStrip.Items.AddRange(new ToolStripItem[] {
            ChangeWallpaper,
            new ToolStripSeparator(),
            AppLight,
            SystemLight,
            new ToolStripSeparator(),
            trans,
            new ToolStripSeparator(),
            help,
            new ToolStripSeparator(),
            exit
        });
    }

    #region change_wallpaper

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int SystemParametersInfo(uint action, uint uParam, string vParam, uint winIni);
    public void ChangeWall(object sender, EventArgs e)
    {
        OpenFileDialog ofd1 = new()
        {
            InitialDirectory = @"D:\Files\WALLPAPERS",
            Filter = "All|*.*"
        };

        if (ofd1.ShowDialog() == DialogResult.OK)
        {
            SystemParametersInfo(0x14, 0, ofd1.FileName, 0x01 | 0x02);

        }
    }

    #endregion


}
