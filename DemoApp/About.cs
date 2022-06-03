namespace Win10_Colors;

public partial class About : Form
{

    public static int WM_NCLBUTTONDOWN = 0xA1;
    public static int Ht_CAPTION = 0x2;


    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();


    [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    public static extern IntPtr CreateRoundRectRgn(
        int nLeftRect,
        int nTopRect,
        int nRightRect,
        int nBottomRect,
        int nWidthEllipse,
        int nHeightEllipse
        );

    public About()
    {
        InitializeComponent();

    }
    public void label1_Click(object sender, EventArgs e)
    {
        this.Hide();
    }
    public void About_FormClosing(object sender, FormClosingEventArgs e)
    {
        Hide();
        if (e.CloseReason == CloseReason.UserClosing)
        {
            e.Cancel = true;

        }
    }
    public void About_Load(object sender, EventArgs e)
    {
        Rectangle workingArea = Screen.GetWorkingArea(this);
        Point point = new Point(workingArea.Right - Size.Width - 15, workingArea.Bottom - Size.Height - 15);

        this.Location = point;
        this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 16, 16));
        pictureBox1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, pictureBox1.Width, pictureBox1.Height, 16, 16));
        panel1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel1.Width, panel1.Height, 16, 16));

        if (Application.OpenForms["About"] == null || this.Visible == false)
        {        
            this.Show();
        }

    }
    public void panel1_MouseDown_1(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, Ht_CAPTION, 0);
        }
    }
}
